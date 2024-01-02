using univm.cli.core;

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

    }

}
