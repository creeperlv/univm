using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using univm.core.Utilities;

namespace univm.core
{
    public static class Constants
    {
        public const int StackBlockSize = 4 * 16;
    }
    public sealed class SharedCoreData
    {
        public Dictionary<uint, Dictionary<uint, SysCall>>? SysCallDefintion;
        public List<IDisposable?> Resources = new List<IDisposable?>();
    }
    public sealed class RuntimeData : IDisposable
    {
        public static int MAX_REGISTER_COUNT = 64;
        public SharedCoreData SharedCoreData = new SharedCoreData();
        public List<UniVMAssembly> assemblies = new List<UniVMAssembly>();
        public byte[] RegisterData;
        public List<MemBlock> MemBlocks = new List<MemBlock>();
        public List<CallStackItem> CallStack = new List<CallStackItem>();
        public RuntimeData()
        {
            RegisterData = new byte[MAX_REGISTER_COUNT * 8];
            var memID = Alloc(Constants.StackBlockSize);
            MemPtr memPtr = new MemPtr(memID, 0);
            SetDataToRegister(RegisterDefinition.SP, memPtr);
        }
        public void DumpText(TextWriter writer, int Width = 80)
        {
            writer.WriteLine("Registers");
            writer.WriteLine(RegisterData.Length);
            int _w = 0;
            for (int i = 0; i < RegisterData.Length; i++)
            {
                writer.Write(RegisterData[i].ToHEX());
                _w += 2;
                if (_w >= Width)
                {
                    writer.WriteLine();
                    _w = 0;
                }
            }
            writer.WriteLine();
            writer.WriteLine("Memory");
            writer.WriteLine(MemBlocks.Count);
            unsafe
            {

                foreach (var item in MemBlocks)
                {
                    _w = 0;
                    writer.WriteLine(item.Size);
                    for (int i = 0; i < item.Size; i++)
                    {
                        writer.Write((item.Data[i]).ToHEX());
                        _w += 2;
                        if (_w >= Width)
                        {
                            writer.WriteLine();
                            _w = 0;
                        }
                    }
                    writer.WriteLine();
                }
            }
        }
        public void DumpBinary(Stream output)
        {
            output.WriteData(RegisterData.Length);
            output.Write(RegisterData);
            output.WriteData(MemBlocks.Count);
            unsafe
            {
                foreach (var item in MemBlocks)
                {
                    output.WriteData(item.Size);
                    for (int i = 0; i < item.Size; i++)
                    {
                        output.WriteByte(item.Data[i]);
                    }
                }
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
        public uint GetMemBlockSize(uint ID)
        {
            if (MemBlocks.Count <= ID)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
                return 0;
            }
            return MemBlocks[(int)ID].Size;
        }
        public unsafe T GetDataFromMemPtr<T>(MemPtr ptr) where T : unmanaged
        {
            T t;
#if BOUNDARY_CHECK
            int size = sizeof(T);
            if (ptr.Offset + size > MemBlocks[(int)ptr.MemID].Size)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
                return default;
            }
#endif
#if NOT_MEMID_CHECK
#else
            if (MemBlocks.Count <= ptr.MemID)
            {
                return default;
            }
#endif
            byte* _b = MemBlocks[(int)ptr.MemID].Data + ptr.Offset;
            T* _t = &t;
            _t[0] = ((T*)_b)[0];
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
        public unsafe void SetDataToMemPtr<T>(MemPtr ptr, T Value) where T : unmanaged
        {
#if BOUNDARY_CHECK
            int size = sizeof(T);
            if (ptr.Offset + size > MemBlocks[(int)ptr.MemID].Size)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
                return;
            }
#endif
#if NOT_MEMID_CHECK
#else
            if (MemBlocks.Count <= ptr.MemID)
            {
                return;
            }
#endif
            byte* b = MemBlocks[(int)ptr.MemID].Data;
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
            if (MemBlocks.Count <= Src.MemID || MemBlocks.Count <= Dest.MemID)
            {
                return;
            }
#endif
            byte* SrcPtr = MemBlocks[(int)Src.MemID].Data + Src.Offset;
            byte* DestPtr = MemBlocks[(int)Dest.MemID].Data + Dest.Offset;
            try
            {
                Buffer.MemoryCopy(SrcPtr, DestPtr, MemBlocks[(int)Dest.MemID].Size - Dest.Offset, Length);

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
            if (MemBlocks.Count <= MemID)
            {
                return;
            }
#endif
            try
            {
                var d = MemBlocks[MemID];
                d.Data = (byte*)Marshal.ReAllocHGlobal((IntPtr)MemBlocks[MemID].Data, (IntPtr)Length);
                d.Size = (uint)Length;
                MemBlocks[MemID] = d;
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
            for (int i = 0; i < MemBlocks.Count; i++)
            {
                if (MemBlocks[i].Data == null)
                {
                    MemBlocks[i] = block;
                    return (uint)i;
                }
            }
            MemBlocks.Add(block);
            return (uint)MemBlocks.Count - 1;
        }
        public unsafe void Free(uint ID)
        {
#if NOT_MEMID_CHECK
#else
            if (MemBlocks.Count >= ID)
            {
                return;
            }
#endif
            Marshal.FreeHGlobal((IntPtr)MemBlocks[(int)ID].Data);
            MemBlock block = new MemBlock(0, null);
            MemBlocks[(int)ID] = block;
        }
        public bool IsSysCallExist(uint Namespace,uint ID)
        {
            if (SharedCoreData.SysCallDefintion is null)
            {
                return false;
            }
            if (!SharedCoreData.SysCallDefintion.ContainsKey(Namespace))
            {
                return false;
            }
            if (!SharedCoreData.SysCallDefintion[Namespace].ContainsKey(ID))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void SysCall(uint Namespace,uint ID)
        {
            if (SharedCoreData.SysCallDefintion is null)
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.SysCallNotExist);
                return;
            }
            if (!SharedCoreData.SysCallDefintion.ContainsKey(Namespace))
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.SysCallNotExist);
                return;
            }
            if (!SharedCoreData.SysCallDefintion[Namespace].ContainsKey(ID))
            {
                SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.SysCallNotExist);
                return;
            }
            else
            {
                SharedCoreData.SysCallDefintion[Namespace][ID](this);
            }
        }
        public void SetSysCall(uint Namespace, uint ID, SysCall call)
        {
            if (SharedCoreData.SysCallDefintion is null)
            {
                SharedCoreData.SysCallDefintion = new Dictionary<uint, Dictionary<uint, SysCall>>();
            }
            if (!SharedCoreData.SysCallDefintion.ContainsKey(Namespace))
            {
                SharedCoreData.SysCallDefintion.Add(Namespace, new Dictionary<uint, SysCall>());
            }
            if (!SharedCoreData.SysCallDefintion[Namespace].ContainsKey(ID))
            {
                SharedCoreData.SysCallDefintion[Namespace].Add(ID, call);
            }
            else
            {
                SharedCoreData.SysCallDefintion[Namespace][ID] = call;
            }
        }

        public unsafe void Dispose()
        {
            for (int i = 0; i < MemBlocks.Count; i++)
            {
                if (MemBlocks[i].Data != null)
                {
                    Free((uint)i);
                }
            }
        }
    }
}