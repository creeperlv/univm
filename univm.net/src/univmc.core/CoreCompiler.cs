using LibCLCC.NET.Operations;
using System.Collections.Generic;
using System.IO;

namespace univmc.core
{
    public class CompileOptions
    {
        public List<string>? SourceFiles;
        public List<string>? IncludeDirectories;
        public List<string>? Libraries;
        public bool IsStatic = false;
        public string output = "a.out";
    }
    public class CoreCompiler
    {
        CompileOptions options;
        public CoreCompiler(CompileOptions options)
        {
            this.options = options;
        }
        public void FinalizeData(CompileTimeData data)
        {
            data.Artifact = new univm.core.UniVMAssembly();
            if (data.IntermediateUniAssembly != null)
            {
                for (int i = 0; i < data.IntermediateUniAssembly.intermediateInstructions.Count; i++)
                {
                    if (options.IsStatic)
                    {

                    }

                }
            }
        }
        public OperationResult<CompileTimeData> Compile()
        {
            CompileTimeData data = new CompileTimeData();
            OperationResult<CompileTimeData> result = new OperationResult<CompileTimeData>(data);
            AssemblyScanner scanner = new AssemblyScanner();
            if (options.SourceFiles != null)
            {
                foreach (var item in options.SourceFiles)
                {
                    var file_info = new FileInfo(item);
                    var parent = file_info.Directory;
                    var HEAD = scanner.Scan(File.ReadAllText(item), false, null);
                    Parser parser = new Parser();
                    var _result = parser.Parse(data, parent.FullName, HEAD);
                    if (result.CheckAndInheritErrorAndWarnings(result))
                    {
                        return result;
                    }
                    if (!_result.Result)
                    {
                        return result;
                    }
                }
            }

            FinalizeData(data);
            return result;
        }
    }
}
