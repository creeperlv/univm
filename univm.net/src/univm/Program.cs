using univm.core;
using univm.syscalls;

namespace univm;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("univm: no input file.");
            return;
        }
        VM vm = new VM();
        UniVMAssembly asm;
        {
            using var fs = File.OpenRead(args[0]);
            asm= UniVMAssembly.Read(fs);
        }
        vm.RedirectSTDIO();
        BSDStyleSyscalls.SetupSysCall(vm);
        //UniVMAssembly asm = new UniVMAssembly()
        //{
        //    Instructions = [
        //        new Inst() { Op_Code = InstOPCodes.BASE_SET32, Data0 = RegisterDefinition.RegisterOffset_01, Data1 = 0x1000, Data2 = 0 },
        //        new Inst() { Op_Code = InstOPCodes.BASE_SET32, Data0 = RegisterDefinition.RegisterOffset_02, Data1 = 0x1234, Data2 = 0 },
        //        new Inst() { Op_Code = InstOPCodes.BASE_ADD, Data0 = RegisterDefinition.RegisterOffset_00, 
        //        Data1 = RegisterDefinition.RegisterOffset_01, Data2 = RegisterDefinition.RegisterOffset_02},
        //        ],
        //};
        vm.RunAsm(asm);
        //using FileStream stream = File.OpenWrite("coredump");
        //vm.DumpBinary(stream);
        //stream.Flush();
        //vm.DumpText(Console.Out);
    }
}
