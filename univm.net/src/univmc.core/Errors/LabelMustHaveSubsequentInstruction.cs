using LibCLCC.NET.Operations;

namespace univmc.core.Errors
{
    public class LabelMustHaveSubsequentInstruction : Error
    {
        public string Label;

        public LabelMustHaveSubsequentInstruction(string label)
        {
            Label = label;
        }
        public override string ToString()
        {
            return $"{Label} requres at least one subsequent instruction.";
        }
    }
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
