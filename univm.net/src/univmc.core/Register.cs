﻿namespace univmc.core
{
    public struct Register
    {
        public uint Value;

        internal static bool TryParse(string str, out uint data)
        {
            if (!uint.TryParse(str, out data))
            {
                if (str.StartsWith("$"))
                {
                    var name = str[1..].ToLower();
                    if (Keywords.RegisterNames.ContainsKey(name))
                    {
                        data = Keywords.RegisterNames[name];
                        return true;
                    }

                }
                return false;
            }
            return true;
        }
    }
}
