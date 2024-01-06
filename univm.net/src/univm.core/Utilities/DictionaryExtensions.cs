using System;
using System.Collections.Generic;
using System.Text;

namespace univm.core.Utilities
{
    public static class DictionaryExtensions
    {
        public static void Set<T, V>(this Dictionary<T, V> _this, T key, V v) where T : notnull
        {
            if(_this.TryAdd(key, v))
            {
                _this[key] = v;

            }
            //if (_this.ContainsKey(key))
            //{
            //    _this[key] = v;
            //}
            //else
            //{
            //    _this.Add(key, v);
            //}
        }
    }
}
