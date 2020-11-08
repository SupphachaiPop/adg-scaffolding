using System;
using System.Collections.Generic;
using System.Text;

namespace Definitions
{
    public class CalculateWarranty
    {
        public int CalculateWarrantyDays(DateTime startWarrantyDate, int warrantyDay, DateTime currentDate)
        {
            var calWarrantyDay = 0;
            DateTime endWarrantyDate;
            var a = -1;

            endWarrantyDate = startWarrantyDate.AddDays(warrantyDay).AddDays(a);
            var cDate = DateTime.Parse(currentDate.Date.ToShortDateString());
            var eDate = DateTime.Parse(endWarrantyDate.Date.ToShortDateString());

            calWarrantyDay = int.Parse((eDate - cDate).TotalDays.ToString()) + 1;

            return calWarrantyDay;
        }
        
        public static bool IsExpiredWarranty(DateTime startWarrantyDate, int warrantyDay, DateTime claimDate)
        {
            TimeSpan openingTime = new TimeSpan(7, 0, 0);
            bool isExpired = startWarrantyDate.AddDays(warrantyDay).Subtract(claimDate).Days <= 0 ? true : false;

            return isExpired;
        }
    }
}
