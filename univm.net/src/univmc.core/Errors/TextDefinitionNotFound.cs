using LibCLCC.NET.Operations;

namespace univmc.core.Errors
{
    public class TextDefinitionNotFound : Error
    {
        public string ConstantName;

        public TextDefinitionNotFound(string constantName)
        {
            ConstantName = constantName;
        }
        public override string ToString()
        {
            return $"Text \"{ConstantName}\" is not defined!";
        }
    }

}
