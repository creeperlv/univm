using System;

namespace univm.core
{
    public sealed class VMCore : IDisposable
    {
        public RuntimeData runtimeData = new RuntimeData();

        public void Dispose()
        {
            runtimeData.Dispose();
        }
        public void Execute(Inst inst)
        {
            switch (inst.Op_Code)
            {
                case InstOPCodes.BASE_ADD:
                    runtimeData.SetDataToRegister(inst.Data0, runtimeData.GetDataFromRegister<int>(inst.Data1) + runtimeData.GetDataFromRegister<int>(inst.Data2));
                    break;
                case InstOPCodes.BASE_SUB:
                    runtimeData.SetDataToRegister(inst.Data0, runtimeData.GetDataFromRegister<int>(inst.Data1) - runtimeData.GetDataFromRegister<int>(inst.Data2));
                    break;
                case InstOPCodes.BASE_MUL:
                    runtimeData.SetDataToRegister(inst.Data0, runtimeData.GetDataFromRegister<int>(inst.Data1) * runtimeData.GetDataFromRegister<int>(inst.Data2));
                    break;
                case InstOPCodes.BASE_DIV:
                    runtimeData.SetDataToRegister(inst.Data0, runtimeData.GetDataFromRegister<int>(inst.Data1) / runtimeData.GetDataFromRegister<int>(inst.Data2));
                    break;
                case InstOPCodes.BASE_SET32:
                    runtimeData.SetDataToRegister(inst.Data0, inst.Data1);
                    break;
                case InstOPCodes.BASE_SET16:
                    runtimeData.SetDataToRegister(inst.Data0, inst.Data1, sizeof(short));
                    break;
                case InstOPCodes.BASE_SET8:
                    runtimeData.SetDataToRegister(inst.Data0, inst.Data1, sizeof(byte));
                    break;
                case InstOPCodes.BASE_SET64:
                    runtimeData.SetDataToRegister64(inst.Data0, inst.Data1, inst.Data2);
                    break;
                case InstOPCodes.HL_ALLOC:
                    {
                        var id = runtimeData.Alloc(runtimeData.GetDataFromRegister<uint>(inst.Data1));
                        MemPtr ptr = new MemPtr(id, 0);
                        runtimeData.SetDataToRegister(inst.Data0, ptr);
                    }
                    break;
                case InstOPCodes.HL_FREE:
                    {
                        runtimeData.Free(runtimeData.GetDataFromRegister<uint>(inst.Data0));
                    }
                    break;
                case InstOPCodes.HL_MEASURE:
                    runtimeData.SetDataToRegister(inst.Data1, runtimeData.GetMemBlockSize(runtimeData.GetDataFromRegister<uint>(inst.Data0)));
                    break;
                case InstOPCodes.BASE_SYSCALL:
                    runtimeData.SysCall(inst.Data0, inst.Data1);
                    break;
                case InstOPCodes.BASE_SYSCALLR:
                    runtimeData.SysCall(runtimeData.GetDataFromRegister<uint>(inst.Data0), runtimeData.GetDataFromRegister<uint>(inst.Data0));
                    break;
                case InstOPCodes.BASE_SYSCALL_TEST:
                    runtimeData.SetDataToRegister(inst.Data2, runtimeData.IsSysCallExist(inst.Data0, inst.Data1) ? 1 : 0);
                    break;
                case InstOPCodes.BASE_SYSCALL_TESTR:
                    runtimeData.SetDataToRegister(inst.Data2, runtimeData.IsSysCallExist(runtimeData.GetDataFromRegister<uint>(inst.Data0), runtimeData.GetDataFromRegister<uint>(inst.Data0)) ? 1 : 0);
                    break;
                default:
                    break;
            }
        }
        public void Call(CallStackItem frame)
        {
            runtimeData.CallStack.Add(frame);
            Run();
        }
        public void Run()
        {
            while (true)
            {

                var frame = runtimeData.CallStack[runtimeData.CallStack.Count - 1];
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var inst = runtimeData.assemblies[(int)frame.AssemblyID].Instructions[frame.PCInAssembly];
                frame.PCInAssembly++;
                runtimeData.CallStack[runtimeData.CallStack.Count - 1] = (frame);
                Execute(inst);
                frame = runtimeData.CallStack[runtimeData.CallStack.Count - 1];
                if (runtimeData.assemblies[(int)frame.AssemblyID].Instructions.Length >= frame.PCInAssembly)
                {
                    return;
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
        }
    }
}