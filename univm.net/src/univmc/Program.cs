using univm.cli.core;
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
        var options=CLIOptions.ParseFromStringArray(args, processor);
        CompileOptions compileOptions = new CompileOptions();

        var sample_code = ".code: set32 $1 1 set32 $2 2 add $3 $1 $2";

        CoreCompiler coreCompiler=new CoreCompiler(compileOptions);
        coreCompiler.Compile();
    }

}
