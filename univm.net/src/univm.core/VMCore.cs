using System;

namespace univm.core
{
    public sealed class VMCore : IDisposable
    {
        public CoreData coreData = new CoreData();
        public MachineData machineData = new MachineData();
        public VM HostMachine;

        public VMCore(VM hostMachine)
        {
            HostMachine = hostMachine;
            machineData = hostMachine.machineData;
            coreData.mdata = machineData;
        }

        public void Dispose()
        {
        }
        public unsafe void Execute(Inst inst)
        {
            switch (inst.Op_Code)
            {
                case InstOPCodes.BASE_ADD:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) + coreData.GetDataFromRegister<int>(inst.Data2));
                    break;
                case InstOPCodes.BASE_SUB:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) - coreData.GetDataFromRegister<int>(inst.Data2));
                    break;
                case InstOPCodes.BASE_MUL:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) * coreData.GetDataFromRegister<int>(inst.Data2));
                    break;
                case InstOPCodes.BASE_DIV:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) / coreData.GetDataFromRegister<int>(inst.Data2));
                    break;
                case InstOPCodes.BASE_ADD_U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<uint>(inst.Data1) + coreData.GetDataFromRegister<uint>(inst.Data2));
                    break;
                case InstOPCodes.BASE_SUB_U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<uint>(inst.Data1) - coreData.GetDataFromRegister<uint>(inst.Data2));
                    break;
                case InstOPCodes.BASE_MUL_U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<uint>(inst.Data1) * coreData.GetDataFromRegister<uint>(inst.Data2));
                    break;
                case InstOPCodes.BASE_DIV_U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<uint>(inst.Data1) / coreData.GetDataFromRegister<uint>(inst.Data2));
                    break;
                case InstOPCodes.BASE_ADD_I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) + ((int*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_SUB_I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) - ((int*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_MUL_I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) * ((int*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_DIV_I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) / ((int*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_ADD_IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) + inst.Data2);
                    break;
                case InstOPCodes.BASE_SUB_IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) - inst.Data2);
                    break;
                case InstOPCodes.BASE_MUL_IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) * inst.Data2);
                    break;
                case InstOPCodes.BASE_DIV_IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) / inst.Data2);
                    break;
                case InstOPCodes.BASE_ADD_64:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<long>(inst.Data1) + coreData.GetDataFromRegister<long>(inst.Data2));
                    break;
                case InstOPCodes.BASE_SUB_64:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<long>(inst.Data1) - coreData.GetDataFromRegister<long>(inst.Data2));
                    break;
                case InstOPCodes.BASE_MUL_64:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<long>(inst.Data1) * coreData.GetDataFromRegister<long>(inst.Data2));
                    break;
                case InstOPCodes.BASE_DIV_64:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<long>(inst.Data1) / coreData.GetDataFromRegister<long>(inst.Data2));
                    break;
                case InstOPCodes.BASE_ADD_64I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<long>(inst.Data1) + ((int*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_SUB_64I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<long>(inst.Data1) - ((int*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_MUL_64I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<long>(inst.Data1) * ((int*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_DIV_64I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<long>(inst.Data1) / ((int*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_ADD_64IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ulong>(inst.Data1) + inst.Data2);
                    break;
                case InstOPCodes.BASE_SUB_64IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ulong>(inst.Data1) - inst.Data2);
                    break;
                case InstOPCodes.BASE_MUL_64IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ulong>(inst.Data1) * inst.Data2);
                    break;
                case InstOPCodes.BASE_DIV_64IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ulong>(inst.Data1) / inst.Data2);
                    break;
                case InstOPCodes.BASE_ADD_64U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ulong>(inst.Data1) + coreData.GetDataFromRegister<ulong>(inst.Data2));
                    break;
                case InstOPCodes.BASE_SUB_64U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ulong>(inst.Data1) - coreData.GetDataFromRegister<ulong>(inst.Data2));
                    break;
                case InstOPCodes.BASE_MUL_64U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ulong>(inst.Data1) * coreData.GetDataFromRegister<ulong>(inst.Data2));
                    break;
                case InstOPCodes.BASE_DIV_64U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ulong>(inst.Data1) / coreData.GetDataFromRegister<ulong>(inst.Data2));
                    break;
                case InstOPCodes.BASE_ADD_16:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<short>(inst.Data1) + coreData.GetDataFromRegister<short>(inst.Data2));
                    break;
                case InstOPCodes.BASE_SUB_16:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<short>(inst.Data1) - coreData.GetDataFromRegister<short>(inst.Data2));
                    break;
                case InstOPCodes.BASE_MUL_16:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<short>(inst.Data1) * coreData.GetDataFromRegister<short>(inst.Data2));
                    break;
                case InstOPCodes.BASE_DIV_16:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<short>(inst.Data1) / coreData.GetDataFromRegister<short>(inst.Data2));
                    break;
                case InstOPCodes.BASE_ADD_16U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ushort>(inst.Data1) + coreData.GetDataFromRegister<ushort>(inst.Data2));
                    break;
                case InstOPCodes.BASE_SUB_16U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ushort>(inst.Data1) - coreData.GetDataFromRegister<ushort>(inst.Data2));
                    break;
                case InstOPCodes.BASE_MUL_16U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ushort>(inst.Data1) * coreData.GetDataFromRegister<ushort>(inst.Data2));
                    break;
                case InstOPCodes.BASE_DIV_16U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ushort>(inst.Data1) / coreData.GetDataFromRegister<ushort>(inst.Data2));
                    break;
                case InstOPCodes.BASE_ADD_16I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<short>(inst.Data1) + ((short*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_SUB_16I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<short>(inst.Data1) - ((short*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_MUL_16I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<short>(inst.Data1) * ((short*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_DIV_16I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<short>(inst.Data1) / ((short*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_ADD_16IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ushort>(inst.Data1) + ((ushort*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_SUB_16IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ushort>(inst.Data1) - ((ushort*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_MUL_16IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ushort>(inst.Data1) * ((ushort*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_DIV_16IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ushort>(inst.Data1) / ((ushort*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_SET32:
                    coreData.SetDataToRegister(inst.Data0, inst.Data1);
                    break;
                case InstOPCodes.BASE_ABS:
                    coreData.SetDataToRegister(inst.Data0, Math.Abs(coreData.GetDataFromRegister<int>(inst.Data1)));
                    break;
                case InstOPCodes.BASE_ABS_B:
                    coreData.SetDataToRegister(inst.Data0, Math.Abs(coreData.GetDataFromRegister<sbyte>(inst.Data1)));
                    break;
                case InstOPCodes.BASE_ABS_D:
                    coreData.SetDataToRegister(inst.Data0, Math.Abs(coreData.GetDataFromRegister<double>(inst.Data1)));
                    break;
                case InstOPCodes.BASE_ABS_S:
                    coreData.SetDataToRegister(inst.Data0, Math.Abs(coreData.GetDataFromRegister<float>(inst.Data1)));
                    break;
                case InstOPCodes.BASE_ABS_16:
                    coreData.SetDataToRegister(inst.Data0, Math.Abs(coreData.GetDataFromRegister<short>(inst.Data1)));
                    break;
                case InstOPCodes.BASE_ABS_64:
                    coreData.SetDataToRegister(inst.Data0, Math.Abs(coreData.GetDataFromRegister<long>(inst.Data1)));
                    break;
                case InstOPCodes.BASE_SET16:
                    coreData.SetDataToRegister(inst.Data0, inst.Data1, sizeof(short));
                    break;
                case InstOPCodes.BASE_SET8:
                    coreData.SetDataToRegister(inst.Data0, inst.Data1, sizeof(byte));
                    break;
                case InstOPCodes.BASE_SETU32:
                    coreData.SetDataToRegister(inst.Data0, inst.Data1);
                    break;
                case InstOPCodes.BASE_SETU16:
                    coreData.SetDataToRegister(inst.Data0, inst.Data1, sizeof(short));
                    break;
                case InstOPCodes.BASE_SETS8:
                    coreData.SetDataToRegister(inst.Data0, inst.Data1, sizeof(sbyte));
                    break;
                case InstOPCodes.BASE_SET64:
                    coreData.SetDataToRegister64(inst.Data0, inst.Data1, inst.Data2);
                    break;
                case InstOPCodes.BASE_SETU64:
                    coreData.SetDataToRegister64(inst.Data0, inst.Data1, inst.Data2);
                    break;
                case InstOPCodes.HL_ALLOC:
                    {
                        uint size = coreData.GetDataFromRegister<uint>(inst.Data1);
                        var id = coreData.Alloc(size);
                        MemPtr ptr = new MemPtr(id, 0);
                        if (machineData.MemBlocks[(int)id].Size == 0 && size != 0)
                        {
                            ptr = new MemPtr(uint.MaxValue, uint.MaxValue);
                        }
                        coreData.SetDataToRegister(inst.Data0, ptr);
                    }
                    break;
                case InstOPCodes.HL_FREE:
                    {
                        var PTR = coreData.GetDataFromRegister<MemPtr>(inst.Data0);
                        if (PTR.IsNotNull())
                            machineData.Free(PTR.MemID);
                    }
                    break;
                case InstOPCodes.HL_MEASURE:
                    coreData.SetDataToRegister(inst.Data1, machineData.GetMemBlockSize(coreData.GetDataFromRegister<uint>(inst.Data0), coreData));
                    break;
                case InstOPCodes.BASE_J:
                    {
                        var frame = coreData.CallStack[^1];
                        frame.PCInAssembly = (uint)(frame.PCInAssembly + ((int*)&inst.Data0)[0]);
                        coreData.CallStack[^1] = (frame);
                    }
                    break;
                case InstOPCodes.BASE_JR:
                    {
                        var frame = coreData.CallStack[^1];
                        frame.PCInAssembly = (uint)(frame.PCInAssembly + coreData.GetDataFromRegister<int>(inst.Data0));
                        coreData.CallStack[^1] = (frame);
                    }
                    break;
                case InstOPCodes.BASE_SYSCALL:
                    machineData.SysCall(inst.Data0, inst.Data1, coreData);
                    break;
                case InstOPCodes.BASE_SYSCALLR:
                    machineData.SysCall(coreData.GetDataFromRegister<uint>(inst.Data0), coreData.GetDataFromRegister<uint>(inst.Data0), coreData);
                    break;
                case InstOPCodes.BASE_SYSCALL_TEST:
                    coreData.SetDataToRegister(inst.Data2, machineData.IsSysCallExist(inst.Data0, inst.Data1) ? 1 : 0);
                    break;
                case InstOPCodes.BASE_SYSCALL_TESTR:
                    coreData.SetDataToRegister(inst.Data2, machineData.IsSysCallExist(coreData.GetDataFromRegister<uint>(inst.Data0), coreData.GetDataFromRegister<uint>(inst.Data0)) ? 1 : 0);
                    break;
                default:
                    break;
            }
        }
        public void Call(CallStackItem frame)
        {
            coreData.CallStack.Add(frame);
            Run();
        }
        public void Run()
        {
            while (true)
            {

                var frame = coreData.CallStack[coreData.CallStack.Count - 1];
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var inst = machineData.assemblies[(int)frame.AssemblyID].Instructions[frame.PCInAssembly];
                frame.PCInAssembly++;
                coreData.CallStack[coreData.CallStack.Count - 1] = (frame);
                Execute(inst);
                frame = coreData.CallStack[coreData.CallStack.Count - 1];
                if (machineData.assemblies[(int)frame.AssemblyID].Instructions.Length <= frame.PCInAssembly)
                {
                    return;
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
        }
    }
}