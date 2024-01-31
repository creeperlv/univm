using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using univm.core.Definitions;

namespace univm.core
{
    public sealed class MachineData : IDisposable
    {
        public List<UniVMAssembly> assemblies = new List<UniVMAssembly>();
        public Dictionary<uint, Dictionary<uint, SysCall>>? SysCallDefintion;
        public List<IDisposable?> Resources = new List<IDisposable?>();
        public List<MemBlock> MemBlocks = new List<MemBlock>();
        public List<Interrupt> InterruptTable=new List<Interrupt>();
        public int AddResource(IDisposable resource)
        {
            for (int i = 0; i < Resources.Count; i++)
            {
                if (Resources[i] == null)
                {
                    Resources[i] = resource;
                    return i;
                }
            }
            Resources.Add(resource);
            return Resources.Count - 1;
        }
        public int AddAssembly(UniVMAssembly asm, CoreData callingCore)
        {
            assemblies.Add(asm);
            asm.GlobalMemPtr = callingCore.Alloc(asm.GlobalMemSize);
            return assemblies.Count - 1;
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

    public struct Interrupt
    {
        public bool IsSet;
        public uint AsmID;
        public uint PC;
    }
}