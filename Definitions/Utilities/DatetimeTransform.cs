using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Definitions.Utilities
{
    public class DatetimeTransform
    {
        public DateTime GetDatetimeWithCultureEN(string input)
        {
            return DateTime.ParseExact(input.Trim(), "dd/MM/yyyy", new CultureInfo("en-US"));
        }

        public DateTime GetDatetimeWithCultureENWithTime(string input)
        {
            return DateTime.ParseExact(input.Trim(), "dd/MM/yyyy HH:mm:ss", new CultureInfo("en-US"));
        }

        public DateTime GetDatetimeWithCultureENAnyFormat(string input)
        {
            return Convert.ToDateTime(input.Trim());
        }

        public DateTime GetDateWithCultureENForImport(string input)
        {
            return DateTime.ParseExact(input.Trim(), "d/M/yyyy", new CultureInfo("en-US"));
        }

        public DateTime GetDatetimeWithCultureENOneDigitMonth(string input)
        {
            return DateTime.ParseExact(input.Trim(), "dd/M/yyyy", new CultureInfo("en-US"));
        }

        public string GetDatetimeWithCultureEN(DateTime inputDate)
        {
            string ret = "-";

            if (inputDate == DateTime.MinValue)
            {
                return ret;
            }

            return inputDate.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
        }

        public string GetDatetimeWithTimeCultureEN(DateTime inputDate)
        {
            string ret = "-";

            if (inputDate == DateTime.MinValue)
            {
                return ret;
            }

            return inputDate.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("en-US"));
        }

        public TimeSpan GetTimeWithCulture(string input)
        {
            return TimeSpan.Parse(input.Trim());
        }
    }
}
