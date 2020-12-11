using System.Collections.Generic;
namespace Entity
{
    public class result_info_job_zone
    {
        public string location_name { get; set; } // location_name (length: 300)
        public string zone_name { get; set; } // zone_name
        public List<result_info_job_zone_item> items { get; set; }

        public result_info_job_zone()
        {
            this.items = new List<result_info_job_zone_item>();
        }
    }

    public class result_info_job_zone_item
    {
        public int seq { get; set; } // seq
        public int job_id { get; set; } // zone_id
        public int product_id { get; set; } // product_id
        public string product_name { get; set; } // product_name
        public int amount { get; set; } // warehouse_name
        public string comment { get; set; } // comment (length: 4000)
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted
    }
}

