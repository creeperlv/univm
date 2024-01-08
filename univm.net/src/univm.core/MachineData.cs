using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace univm.core
{
    public sealed class MachineData : IDisposable
    {
        public List<UniVMAssembly> assemblies = new List<UniVMAssembly>();
        public Dictionary<uint, Dictionary<uint, SysCall>>? SysCallDefintion;
        public List<IDisposable?> Resources = new List<IDisposable?>();
        public List<MemBlock> MemBlocks = new List<MemBlock>();
        public int AddAssembly(UniVMAssembly asm,CoreData callingCore)
        {
            assemblies.Add(asm);
            asm.GlobalMemPtr=Alloc(asm.GlobalMemSize, callingCore);
            return assemblies.Count-1;
        }
        public uint GetMemBlockSize(uint ID,CoreData data)
        {
            if (MemBlocks.Count <= ID)
            {
                data.SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
                return 0;
            }
            return MemBlocks[(int)ID].Size;
        }
        public unsafe T GetDataFromMemPtr<T>(MemPtr ptr, CoreData data) where T : unmanaged
        {
            T t;
#if BOUNDARY_CHECK
            int size = sizeof(T);
            if (ptr.Offset + size > MemBlocks[(int)ptr.MemID].Size)
            {
                data.SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
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
        public unsafe void SetDataToMemPtr<T>(MemPtr ptr, T Value, CoreData data) where T : unmanaged
        {
#if BOUNDARY_CHECK
            int size = sizeof(T);
            if (ptr.Offset + size > MemBlocks[(int)ptr.MemID].Size)
            {
                data.SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
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
        public unsafe void MemCpy(MemPtr Src, MemPtr Dest, uint Length, CoreData data)
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
                data.SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
            }
            catch (Exception)
            {
                data.SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.Unknown);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Realloc(int MemID, int Length, CoreData data)
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
                data.SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfMemory);
            }
            catch (Exception)
            {
                data.SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.Unknown);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe uint Alloc(uint Size, CoreData data)
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
                data.SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfMemory);
            }
            catch (Exception)
            {
                data.SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.Unknown);
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
        public bool IsSysCallExist(uint Namespace, uint ID)
        {
            if (SysCallDefintion is null)
            {
                return false;
            }
            if (!SysCallDefintion.ContainsKey(Namespace))
            {
                return false;
            }
            if (!SysCallDefintion[Namespace].ContainsKey(ID))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void SysCall(uint Namespace, uint ID, CoreData data)
        {
            if (SysCallDefintion is null)
            {
                data.SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.SysCallNotExist);
                return;
            }
            if (!SysCallDefintion.ContainsKey(Namespace))
            {
                data.SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.SysCallNotExist);
                return;
            }
            if (!SysCallDefintion[Namespace].ContainsKey(ID))
            {
                data.SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.SysCallNotExist);
                return;
            }
            else
            {
                SysCallDefintion[Namespace][ID](data);
            }
        }
        public void SetSysCall(uint Namespace, uint ID, SysCall call)
        {
            if (SysCallDefintion is null)
            {
                SysCallDefintion = new Dictionary<uint, Dictionary<uint, SysCall>>();
            }
            if (!SysCallDefintion.ContainsKey(Namespace))
            {
                SysCallDefintion.Add(Namespace, new Dictionary<uint, SysCall>());
            }
            if (!SysCallDefintion[Namespace].ContainsKey(ID))
            {
                SysCallDefintion[Namespace].Add(ID, call);
            }
            else
            {
                SysCallDefintion[Namespace][ID] = call;
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