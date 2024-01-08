using LibCLCC.NET.Operations;

namespace univmc.core.Errors
{
    public class LabelHaveNoRealLabelString : Error
    {
        public override string ToString()
        {
            return "A Label have no label string! How do you get this error?";
        }
    }
}
