using LibCLCC.NET.Operations;

namespace univmc.core.Errors
{
    public class LabelNotFound : Error
    {
        public string ConstantName;

        public LabelNotFound(string constantName)
        {
            ConstantName = constantName;
        }
        public override string ToString()
        {
            return $"Label \"{ConstantName}\" is not defined!";
        }
    }

}
