using System.Collections.Generic;
namespace Entity
{
    public partial class _BaseConst
    {
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
    }
}

