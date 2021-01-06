using System.Collections.Generic;
namespace Entity
{
    public partial class _BaseConst
    {

        public const string job_complete = "complete";
        public enum location
        {
            Cleaning = 1,
            Repair = 2,
            Delivery = 3
        }

        public enum status_job
        {
            cleaning_processing = 1,
            cleaning_complete = 4,
            repair_processing = 5,
            repair_complete = 6,
            repair_await_part = 7,
            delivery_processing = 8,
            delivery_complete = 9,
        }

        public enum status_job_cleaning
        {
            cleaning_processing = 1,

        }

        public enum status_job_repair
        {
            repair_processing = 5,
            repair_await_part = 7,
        }

        public enum status_job_delivery
        {
            delivery_processing = 8,
        }

        //public static Dictionary<string, string> LOANER_STATUS = new Dictionary<string, string>() {
        //    { "Open", "O"},
        //    { "Loaner", "L"},
        //    { "Return", "R"}
        //};
    }
}

