﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationAttributes.Attributes
{
    public class MyRangeAttribute : MyValidationAttribute
    {
        private int minValue;
        private int maxValue;

        public MyRangeAttribute(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public override bool IsValid(object obj)
        {
            // short syntaxis: => (int)obj >= minValue && (int)obj <= maxValue;

            int numberToValdate = (int)obj;

            if (numberToValdate < minValue || numberToValdate > maxValue)
            {
                return false;

            }
            return true;

        }
    }
}
