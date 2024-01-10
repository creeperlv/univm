using LibCLCC.NET.Operations;

namespace univmc.core.Errors
{
    public class AssemblyDefinitionNotFound : Error
    {
        public string FileName;

        public AssemblyDefinitionNotFound(string fileName)
        {
            FileName = fileName;
        }
        public override string ToString()
        {
            return $"Assembly Definition file not found \"{FileName}\".";
        }
    }
}
