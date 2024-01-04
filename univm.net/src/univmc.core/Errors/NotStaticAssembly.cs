using LibCLCC.NET.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace univmc.core.Errors
{
    public class NotStaticAssembly : Error
    {
        public string Name;
        public NotStaticAssembly(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return $"\"{Name}\" is not static assembly.";
        }
    }
    public class LabelHaveNoRealLabelString : Error
    {
        public override string ToString()
        {
            return "A Label have no label string! How do you get this error?";
        }
    }
}
