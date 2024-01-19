using System.Diagnostics;
using univm.cli.core;
using univm.core;
using univmc.core;

namespace univmc;

class Program
{
    static void Main(string[] args)
    {
        CLIProcessor processor = new CLIProcessor();
        processor.MainParameterPlaceholder = "source_file_0 [source_file_1 [source_file_2 [...]]]";

        processor.AllowMultipleMainParameters = true;
        processor.UnknownParametersAsMainParameters = true;
        processor.ParameterOptions =
        [
            new Parameter("-L",[]) { RequireValues = true, AcceptValues = true, Description = "Library" },
            new Parameter("-o", ["--output"])
            {
                RequireValues = true,
                AcceptValues = true,
                Description = "Output file",
                KeyAsPartOfName = false
            },
        ];
        var options = CLIOptions.ParseFromStringArray(args, processor);
        if (options.Result.ShowHelp)
        {
            processor.PrintParameters(Console.Out, 0);
            return;
        }
        using CompileOptions compileOptions = new(options.Result);
        //.prep: i \"../kernel/k.inc\" i <io.inc> l ker \"kernel.dll\" 
        //var sample_code = ".text: " +
        //    "t0 \"Hello, World!\\n\" .code: " +
        //    "qtext $a2 @t0 " +
        //    "set32 $a1 1 " +
        //    "set32 $a3 ?t0 " +
        //    "syscall 0 64";
        //compileOptions.SourceFiles.Add(new SourceFile() { Data = sample_code, DataIsNotFile = true });
        CoreCompiler coreCompiler = new(compileOptions);

        var result = coreCompiler.Compile();
        //Console.WriteLine("Error?" + result.HasError());
        //Console.WriteLine("IC:" + result.Result.IntermediateUniAssembly.intermediateInstructions.Count);
        if (result.HasError())
        {
            foreach (var item in result.Errors)
            {
                Console.WriteLine(item.ToString());
            }
        }
        if (result.HasWarning())
        {
            foreach (var item in result.Warnings)
            {
                Console.WriteLine(item.ToString());
            }
        }
        if (result.Result.Artifact != null)
        {
            using var opt = compileOptions.output ?? new Output("a.out");
            UniVMAssembly.Write(opt.stream, result.Result.Artifact);
        }
        else
        {
            Console.WriteLine("Weird. No Artifact.");
        }
    }

}
