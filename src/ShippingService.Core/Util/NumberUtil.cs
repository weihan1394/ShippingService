using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ShippingService.Core.Util
{
    class NumberUtil
    {
        // creating object of CultureInfo 
        static CultureInfo cultures = new CultureInfo("en-US");

        public static int convertStringToInt(string input)
        {
            if (checkInt(input))
            {
                string newValue = string.IsNullOrEmpty(input) ? "0" : input;
                return int.Parse(newValue, cultures);
            }
            else
            {
                return 0;
            }
        }

        private static bool checkInt(string value)
        {
            int num;
            return int.TryParse(value, out num);
        }

        public static double convertStringToDouble(string input)
        {
            if (checkDouble(input))
            {
                string newValue = string.IsNullOrEmpty(input) ? "0.0" : input;
                return double.Parse(newValue, cultures);
            }
            else
            {
                return 0.0f;
            }
        }

        private static bool checkDouble(string value)
        {
            double num;
            return double.TryParse(value, out num);
        }
    }
}
