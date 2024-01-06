using System;
using System.Collections.Generic;
using System.Text;

namespace univmc.core.Utilities
{
    public static class NumberParsers
    {
        public static bool TryParse(this string value, out int result)
        {
            if (value.StartsWith("0x") || value.StartsWith("0X"))
            {
                try
                {

                    result = Convert.ToInt32(value[2..], 16);
                    return true;
                }
                catch (Exception)
                {
                }
                result = default;
                return false;
            }
            else
            if (value.StartsWith("0c") || value.StartsWith("0C"))
            {
                try
                {

                    result = Convert.ToInt32(value[2..], 8);
                    return true;
                }
                catch (Exception)
                {
                }
                result = default;
                return false;
            }
            else
            if (value.StartsWith("0b") || value.StartsWith("0B"))
            {
                try
                {

                    result = Convert.ToInt32(value[2..], 2);
                    return true;
                }
                catch (Exception)
                {
                }
                result = default;
                return false;
            }
            else
            {
                return int.TryParse(value, out result);
            }

        }
        public static bool TryParse(this string value, out uint result)
        {
            if (value.StartsWith("0x") || value.StartsWith("0X"))
            {
                try
                {

                    result = Convert.ToUInt32(value[2..], 16);
                    return true;
                }
                catch (Exception)
                {
                }
                result = default;
                return false;
            }
            else
            if (value.StartsWith("0c") || value.StartsWith("0C"))
            {
                try
                {

                    result = Convert.ToUInt32(value[2..], 8);
                    return true;
                }
                catch (Exception)
                {
                }
                result = default;
                return false;
            }
            else
            if (value.StartsWith("0b") || value.StartsWith("0B"))
            {
                try
                {

                    result = Convert.ToUInt32(value[2..], 2);
                    return true;
                }
                catch (Exception)
                {
                }
                result = default;
                return false;
            }
            else
            {
                return uint.TryParse(value, out result);
            }

        }
    }
}
