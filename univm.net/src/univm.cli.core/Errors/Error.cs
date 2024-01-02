using LibCLCC.NET.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace univm.cli.core.Errors
{
    public class RequireValueError : Error
    {
        string _key;
        public RequireValueError(string Key)
        {
            _key = Key;
        }
        public override string ToString()
        {
            return $"{_key} requries a value";
        }
    }
    public class UnknownParameterError : Error
    {
        string _key;
        public UnknownParameterError(string Key)
        {
            _key = Key;
        }
        public override string ToString()
        {
            return $"Unknown parameter:{_key}";
        }
    }
}
