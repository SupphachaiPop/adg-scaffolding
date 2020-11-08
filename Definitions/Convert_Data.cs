using System;
using System.Collections.Generic;
using System.Text;

namespace Definitions
{
    namespace Definitions
    {
        public class SetDataBeforeConvert
        {
            public static Int32 setIntBeforeConvert(string inputText)
            {
                if (!string.IsNullOrEmpty(inputText))
                {
                    return Convert.ToInt32(inputText);
                }

                return 0;
            }

            public static Decimal setScoreBeforeInsert(string inputText)
            {
                if (!string.IsNullOrEmpty(inputText))
                {
                    return Convert.ToDecimal(inputText.Replace(",", ""));
                }

                return 0;
            }

            public static Decimal setFormatBeforeCalculate(string inputText)
            {
                if (!string.IsNullOrEmpty(inputText))
                {
                    return Convert.ToDecimal(inputText.Replace(",", ""));
                }

                return 0;
            }

            public static bool IsNumber(string inputText)
            {
                if (!string.IsNullOrEmpty(inputText))
                {
                    if (Decimal.TryParse(inputText.Replace(",", ""), out Decimal result))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }

}

