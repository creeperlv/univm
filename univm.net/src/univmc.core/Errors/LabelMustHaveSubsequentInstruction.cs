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
}
