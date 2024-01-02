using System.Collections.Generic;

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

        }
        public void Compile()
        {
            CompileTimeData data = new CompileTimeData();

            if (options.SourceFiles != null)
            {
                foreach (var item in options.SourceFiles)
                {

                }
            }
            FinalizeData(data);
        }
    }
}
