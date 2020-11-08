using Definitions.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Definitions.Static_Text
{
    public class WarrantyStatus
    {
        public enum WARRANTY_STATUS
        {
            PENDING_WARRANTY = -1,
            OUT_OF_WARRANTY = 0,
            IN_WARRANTY = 1
        }
        public enum BALANCE_DAY_CONDITION
        {
            EQUAL = 0,
            LESS_THAN_OR_EQUAL = 1,
            GREATER_THAN_OR_EQUAL = 2
        }
        public enum RUNNING_NUMBER
        {
            SW = 1,
            CLAIM = 2
        }

        public enum ROW_COLOR
        {
            [Display(Name = "")]
            DEFAULT = 1,
            [Display(Name = " custom-alert-warning")]
            YELLOW = 2,
            [Display(Name = " custom-alert-danger")]
            RED = 3
        }

        public enum WARRANTY_SEARCH_TYPE
        {
            IMEI = 1,
            ID_CARD = 2,
            TEL_NO = 3
        }

        public const string STATUS_IN_WARRANTY = "Coverage";
        public const string STATUS_OUT_OF_WARRANTY = "Expire";

        public string GetStatusName(int statusId)
        {
            switch (statusId)
            {
                case (int)WARRANTY_STATUS.OUT_OF_WARRANTY:

                    return WarrantyStatus.STATUS_OUT_OF_WARRANTY;

                default:

                    return WarrantyStatus.STATUS_IN_WARRANTY;
            }
        }

        public static string GetCSSClassName(int? id)
        {
            string className = string.Empty;

            switch (id)
            {
                case (int)ROW_COLOR.DEFAULT:
                    className = EnumHelper<ROW_COLOR>.GetDisplayValue(ROW_COLOR.DEFAULT);
                    break;

                case (int)ROW_COLOR.YELLOW:
                    className = EnumHelper<ROW_COLOR>.GetDisplayValue(ROW_COLOR.YELLOW);
                    break;

                case (int)ROW_COLOR.RED:
                    className = EnumHelper<ROW_COLOR>.GetDisplayValue(ROW_COLOR.RED);
                    break;
            }

            return className;
        }

    }
}
