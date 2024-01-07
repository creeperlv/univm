using univm.core;
using univm.syscalls;

namespace univm;

class Program
{
    static void Main(string[] args)
    {
        VM vm = new VM();
        //MachineData machine = vm.machineData;
        //VMCore vMCore = vm.CreateCore();
        //CoreData coredata = vMCore.coreData;
        //DefaultSysCalls.SetupSysCall(vm);
        //coredata.SetDataToRegister(0, 123);
        //Console.WriteLine(coredata.GetDataFromRegister<int>(0));
        //coredata.SetDataToRegister(1, 123);
        //Console.WriteLine(coredata.GetDataFromRegister<int>(0));
        //coredata.SetDataToRegister(0, 1.025f);
        //Console.WriteLine(coredata.GetDataFromRegister<float>(0));
        //var ID = machine.Alloc(4, coredata);
        //var ID2 = machine.Alloc(4, coredata);
        //MemPtr L = new MemPtr() { MemID = ID, Offset = 0 };
        //MemPtr R = new MemPtr() { MemID = ID2, Offset = 0 };
        
        UniVMAssembly asm = new UniVMAssembly()
        {
            Instructions = [
                new Inst() { Op_Code = InstOPCodes.BASE_SET32, Data0 = RegisterDefinition.RegisterOffset_01, Data1 = 0x1000, Data2 = 0 },
                new Inst() { Op_Code = InstOPCodes.BASE_SET32, Data0 = RegisterDefinition.RegisterOffset_02, Data1 = 0x1234, Data2 = 0 },
                new Inst() { Op_Code = InstOPCodes.BASE_ADD, Data0 = RegisterDefinition.RegisterOffset_00, 
                Data1 = RegisterDefinition.RegisterOffset_01, Data2 = RegisterDefinition.RegisterOffset_02},
                ],
        };
        //coredata.SetDataToRegister(RegisterDefinition.RegisterOffset_00, L);
        //coredata.SetDataToRegister(RegisterDefinition.RegisterOffset_01, R);
        //machine.SetDataToMemPtr(coredata.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_00), 1.234f, coredata);
        //machine.MemCpy(coredata.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_00),
        //                   coredata.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_01),
        //                   4, coredata);
        //Console.WriteLine(machine.GetDataFromMemPtr<float>(coredata.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_00), coredata));
        //Console.WriteLine(machine.GetDataFromMemPtr<float>(coredata.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_01), coredata));
        //machine.Realloc((int)ID, 8, coredata);
        //Console.WriteLine(machine.GetDataFromMemPtr<float>(L, coredata));
        //L.Offset += 4;
        //machine.SetDataToMemPtr(L, 117, coredata);
        //Console.WriteLine(machine.GetDataFromMemPtr<int>(L, coredata));
        vm.RunAsm(asm);
        using FileStream stream = File.OpenWrite("coredump");
        vm.DumpBinary(stream);
        stream.Flush();
        vm.DumpText(Console.Out);
    }
}
