using System.Collections.Generic;
namespace Entity
{
    public class result_search_job_zone
    {
        public int total_record { get; set; }
        public string id { get; set; } // zone_id (Primary key)
        public int zone_id { get; set; } // zone_id
        public int location_id { get; set; } // location_id
        public string warehouse_name { get; set; } // location_id (Primary key)
        public string location_name { get; set; } // location_name (length: 300)
        public string zone_name { get; set; } // warehouse_name
        public int amount { get; set; } // warehouse_name
        public string comment { get; set; } // comment (length: 4000)
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active

    }
}

