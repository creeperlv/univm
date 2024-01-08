using LibCLCC.NET.Operations;

namespace univmc.core.Errors
{
    public class ConstantDefinitionNotFound : Error
    {
        public string ConstantName;

        public ConstantDefinitionNotFound(string constantName)
        {
            ConstantName = constantName;
        }
        public override string ToString()
        {
            return $"Constant \"{ConstantName}\" is not defined!";
        }
    }

}
