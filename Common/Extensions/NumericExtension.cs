using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemnantOfTheAncientsMod.Common.Extensions
{
    public static class NumericExtension
    {
        public static bool Between(this int value, int min, int max, bool inclusive = false)
        {
            if(inclusive) return value <= max && value >= min;
            return value < max && value > min;
        }
        public static bool Between(this float value, float min, float max, bool inclusive = false)
        {
            if (inclusive) return value <= max && value >= min;
            return value < max && value > min;
        }
    }
}
