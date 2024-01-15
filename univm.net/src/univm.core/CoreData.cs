using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace univm.core
{
    public static class Constants
    {
        public const int StackBlockSize = 4 * 16;
    }
    public sealed class CoreData
    {
        public static int MAX_REGISTER_COUNT = 64;
        public MachineData mdata = new MachineData();
        public byte[] RegisterData;
        public List<CallStackItem> CallStack = new List<CallStackItem>();
        public uint CurrentStackSize = 0;
        public VMCore core;
        public CoreData(VMCore core)
        {
            RegisterData = new byte[MAX_REGISTER_COUNT * 8];
            var memID = Alloc(Constants.StackBlockSize);
            MemPtr memPtr = new MemPtr(memID, 0);
            SetDataToRegister(RegisterDefinition.SP, memPtr);
            this.core = core;
        }
        public void TryCloseResourceByID(int ID)
        {
            if (mdata.Resources == null) return;
            if (mdata.Resources.Count <= ID) return;
            var res = mdata.Resources[ID];
            if (res == null) return;
            res.Dispose();
            mdata.Resources[ID] = null;
        }
        public bool TryQueryResourceByID(int ID, bool WillTriggerErrno, out IDisposable? disposable)
        {
            if (mdata.Resources.Count <= ID)
            {
                disposable = null;
                if (WillTriggerErrno)
                    SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.ResourceNotFound);
                return false;
            }
            else
            {
                if (mdata.Resources[ID] == null)
                {
                    disposable = null;
                    if (WillTriggerErrno)
                        SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.ResourceNotFound);
                    return false;
                }
                disposable = mdata.Resources[ID];
                return true;
            }
        }
        public uint GetMemBlockSize(uint ID)
        {
            if (mdata.MemBlocks.Count <= ID)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
                return 0;
            }
            return mdata.MemBlocks[(int)ID].Size;
        }
        public uint GetMemBlockSize(MemPtr ID)
        {
            if (mdata.MemBlocks.Count <= ID.MemID)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
                return 0;
            }
            return mdata.MemBlocks[(int)ID.MemID].Size - ID.Offset;
        }
        public unsafe bool TryGetMemBlockFromPtr(MemPtr ptr, bool SetErrnoOnException, out MemBlock blk)
        {
            if (mdata.MemBlocks.Count <= ptr.MemID)
            {
                if (SetErrnoOnException)
                    SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
                blk = default;
                return false;
            }
            var d = mdata.MemBlocks[(int)ptr.MemID];
            d.Data += ptr.Offset;
            d.Size -= ptr.Offset;
            blk = d;
            return true;
        }
        public unsafe void GetDataFromMemPtr(byte* buf, MemPtr ptr, uint TargetBufferSize, uint SizeToGet)
        {
            int id = (int)ptr.MemID;
            if (id >= mdata.MemBlocks.Count)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.MemIDNotExist);
                return;
            }
            if (SizeToGet > mdata.MemBlocks[id].Size)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
                return;
            }
            var blk = mdata.MemBlocks[id];
            var src = blk.Data + ptr.Offset;
            Buffer.MemoryCopy(src, buf, TargetBufferSize, SizeToGet);
        }
        public unsafe void GetDataFromMemPtr(byte* buf, MemPtr ptr, uint TargetBufferSize)
        {
            int id = (int)ptr.MemID;
            if (id >= mdata.MemBlocks.Count)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.MemIDNotExist);
                return;
            }
            var blk = mdata.MemBlocks[id];
            var SizeToGet = blk.Size - ptr.Offset;
            var src = blk.Data + ptr.Offset;
            Buffer.MemoryCopy(src, buf, TargetBufferSize, SizeToGet);
        }
        public unsafe T GetDataFromMemPtr<T>(MemPtr ptr) where T : unmanaged
        {
            T t;
#if BOUNDARY_CHECK
            int size = sizeof(T);
            if (ptr.Offset + size > mdata.MemBlocks[(int)ptr.MemID].Size)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
                return default;
            }
#endif
#if NOT_MEMID_CHECK
#else
            if (mdata.MemBlocks.Count <= ptr.MemID)
            {
                return default;
            }
#endif
            byte* _b = mdata.MemBlocks[(int)ptr.MemID].Data + ptr.Offset;
            T* _t = &t;
            _t[0] = ((T*)_b)[0];
            return t;
        }
        public unsafe void SetDataToMemPtr<T>(MemPtr ptr, T Value) where T : unmanaged
        {
#if BOUNDARY_CHECK
            int size = sizeof(T);
            if (ptr.Offset + size > mdata.MemBlocks[(int)ptr.MemID].Size)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
                return;
            }
#endif
#if NOT_MEMID_CHECK
#else
            if (mdata.MemBlocks.Count <= ptr.MemID)
            {
                return;
            }
#endif
            byte* b = mdata.MemBlocks[(int)ptr.MemID].Data;
            {
                T* _b = (T*)(b + ptr.Offset);
                _b[0] = Value;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void MemCpy(MemPtr Src, MemPtr Dest, uint Length)
        {
#if NOT_MEMID_CHECK
#else
            if (mdata.MemBlocks.Count <= Src.MemID || mdata.MemBlocks.Count <= Dest.MemID)
            {
                return;
            }
#endif
            byte* SrcPtr = mdata.MemBlocks[(int)Src.MemID].Data + Src.Offset;
            byte* DestPtr = mdata.MemBlocks[(int)Dest.MemID].Data + Dest.Offset;
            try
            {
                Buffer.MemoryCopy(SrcPtr, DestPtr, mdata.MemBlocks[(int)Dest.MemID].Size - Dest.Offset, Length);

            }
            catch (ArgumentOutOfRangeException)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
            }
            catch (Exception)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.Unknown);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Realloc(int MemID, int Length)
        {
#if NOT_MEMID_CHECK
#else
            if (mdata.MemBlocks.Count <= MemID)
            {
                return;
            }
#endif
            if (mdata.MemBlocks[MemID].Size == Length) return;
            try
            {
                var d = mdata.MemBlocks[MemID];
                d.Data = (byte*)Marshal.ReAllocHGlobal((IntPtr)mdata.MemBlocks[MemID].Data, (IntPtr)Length);
                d.Size = (uint)Length;
                mdata.MemBlocks[MemID] = d;
            }
            catch (OutOfMemoryException)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfMemory);
            }
            catch (Exception)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.Unknown);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe uint Alloc(uint Size)
        {
            MemBlock block = new MemBlock
            {
                Data = null,
                Size = 0,
            };
            try
            {
                block.Data = (byte*)Marshal.AllocHGlobal((int)Size);
                block.Size = Size;
            }
            catch (OutOfMemoryException)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfMemory);
            }
            catch (Exception)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.Unknown);
            }
            for (int i = 0; i < mdata.MemBlocks.Count; i++)
            {
                if (mdata.MemBlocks[i].Data == null)
                {
                    mdata.MemBlocks[i] = block;
                    return (uint)i;
                }
            }
            mdata.MemBlocks.Add(block);
            return (uint)mdata.MemBlocks.Count - 1;
        }
        public unsafe void Free(uint ID)
        {
#if NOT_MEMID_CHECK
#else
            if (mdata.MemBlocks.Count >= ID)
            {
                return;
            }
#endif
            Marshal.FreeHGlobal((IntPtr)mdata.MemBlocks[(int)ID].Data);
            MemBlock block = new MemBlock(0, null);
            mdata.MemBlocks[(int)ID] = block;
        }
        public bool IsSysCallExist(uint Namespace, uint ID)
        {
            if (mdata.SysCallDefintion is null)
            {
                return false;
            }
            if (!mdata.SysCallDefintion.ContainsKey(Namespace))
            {
                return false;
            }
            if (!mdata.SysCallDefintion[Namespace].ContainsKey(ID))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void SysCall(uint Namespace, uint ID)
        {
            if (mdata.SysCallDefintion is null)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.SysCallNotExist);
                return;
            }
            if (!mdata.SysCallDefintion.ContainsKey(Namespace))
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.SysCallNotExist);
                return;
            }
            if (!mdata.SysCallDefintion[Namespace].ContainsKey(ID))
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.SysCallNotExist);
                return;
            }
            else
            {
                mdata.SysCallDefintion[Namespace][ID](this);
            }
        }
        public void SetSysCall(uint Namespace, uint ID, SysCall call)
        {
            if (mdata.SysCallDefintion is null)
            {
                mdata.SysCallDefintion = new Dictionary<uint, Dictionary<uint, SysCall>>();
            }
            if (!mdata.SysCallDefintion.ContainsKey(Namespace))
            {
                mdata.SysCallDefintion.Add(Namespace, new Dictionary<uint, SysCall>());
            }
            if (!mdata.SysCallDefintion[Namespace].ContainsKey(ID))
            {
                mdata.SysCallDefintion[Namespace].Add(ID, call);
            }
            else
            {
                mdata.SysCallDefintion[Namespace][ID] = call;
            }
        }

        public unsafe T GetDataFromRegister<T>(uint Offset) where T : unmanaged
        {
#if BOUNDARY_CHECK
            int size = sizeof(T);
            if (Offset + size > RegisterData.Length)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
                return default;
            }
#endif
            T t;
            fixed (byte* b = RegisterData)
            {
                byte* _b = b + Offset;
                T* _t = &t;
                _t[0] = ((T*)_b)[0];
            }
            return t;
        }
        public unsafe void SetDataToRegister(uint Offset, uint Value, uint Size)
        {
#if BOUNDARY_CHECK
            if (Offset + Size> RegisterData.Length)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
                return;
            }
#endif
            byte* b = (byte*)&Value;
            for (int i = 0; i < Size; i++)
            {
                RegisterData[Offset + i] = b[i];
            }
        }
        public unsafe void SetDataToRegister64(uint Offset, uint Value, uint Value2)
        {
#if BOUNDARY_CHECK
            if (Offset + sizeof(long)> RegisterData.Length)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
                return;
            }
#endif
            fixed (byte* b = RegisterData)
            {
                uint* _b = (uint*)(b + Offset);
                _b[0] = Value;
                _b[1] = Value2;
            }
        }
        public unsafe void SetDataToRegister<T>(uint Offset, T Value) where T : unmanaged
        {

#if BOUNDARY_CHECK
            int size = sizeof(T);
            if (Offset + size > RegisterData.Length)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
                return;
            }
#endif
            fixed (byte* b = RegisterData)
            {
                T* _b = (T*)(b + Offset);
                _b[0] = Value;
            }
        }
    }
}