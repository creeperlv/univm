using LibCLCC.NET.Operations;

namespace univmc.core.Errors
{
    public class GlobalVariableNotFound : Error
    {
        public string ConstantName;

        public GlobalVariableNotFound(string constantName)
        {
            ConstantName = constantName;
        }
        public override string ToString()
        {
            return $"Global Variable \"{ConstantName}\" is not defined!";
        }
    }

}
