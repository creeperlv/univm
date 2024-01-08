using LibCLCC.NET.Operations;
using System;

namespace univmc.core.Errors
{
    public class TypeParseError : Error
    {
        public Type Target;
        public string Value;

        public TypeParseError(Type target, string value)
        {
            Target = target;
            Value = value;
        }
        public override string ToString()
        {
            return $"Cannot parse string \"{Value}\" to {Target.Name}";
        }
    }

}
