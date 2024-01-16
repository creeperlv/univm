using univm.core;
using univm.syscalls;
using System;
using System.IO;
namespace univm;

class Program
{
    static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("univm: no input file.");
            return -1;
        }
        VM vm = new();
        UniVMAssembly asm;
        {
            using var fs = File.OpenRead(args[0]);
            asm = UniVMAssembly.Read(fs);
        }
        vm.RedirectSTDIO();
        BSDStyleSyscalls.SetupSysCall(vm);
        vm.RunAsm(asm);
        return vm.ExitCode;
    }
}
