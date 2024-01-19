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
        VM vm = new(VMConfiguration.NoDispatcherConfiguration);
        UniVMAssembly asm;
        {
            using var fs = File.OpenRead(args[0]);
            asm = UniVMAssembly.Read(fs);
        }
        vm.RedirectSTDIO();
        BSDStyleSyscalls.SetupSysCall(vm);
        try
        {
            vm.RunAsm(asm);
        }
        catch (Exception)
        {
            var fs=File.Open("coredump.log", FileMode.Create, FileAccess.Write);
            fs.SetLength(0);
            var sw=new StreamWriter(fs);
            vm.DumpText(sw);
            throw;
        }
        return vm.ExitCode;
    }
}
