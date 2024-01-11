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
        processor.AllowMultipleMainParameters = true;
        processor.UnknownParametersAsMainParameters = true;
        processor.ParameterOptions = new List<Parameter>()
        {
            new Parameter(){ MainName="L", Aliases=new List<string>(), RequireValues=true, AcceptValues =true, Description="Library" },
            new Parameter(){ MainName="O", Aliases=new List<string>(){"output"}, RequireValues=true, AcceptValues =true, Description="Output" },
        };
        var options = CLIOptions.ParseFromStringArray(args, processor);
        CompileOptions compileOptions = new CompileOptions();
        //.prep: i \"../kernel/k.inc\" i <io.inc> l ker \"kernel.dll\" 
        var sample_code = ".text: " +
            "t0 \"Hello, World!\\n\" .code: " +
            "qtext $a2 @t0 " +
            "set32 $a1 1 " +
            "set32 $a3 ?t0 " +
            "syscall 0 64";
        compileOptions.SourceFiles.Add(new SourceFile() { Data = sample_code, DataIsNotFile = true });
        CoreCompiler coreCompiler = new CoreCompiler(compileOptions);

        var result = coreCompiler.Compile();
        //Console.WriteLine("Error?" + result.HasError());
        //Console.WriteLine("IC:" + result.Result.IntermediateUniAssembly.intermediateInstructions.Count);

        if (result.Result.Artifact != null)
        {
            using var opt = compileOptions.output ?? new Output("a.out");
            UniVMAssembly.Write(opt.stream, result.Result.Artifact);
        }
    }

}
