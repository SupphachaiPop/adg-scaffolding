using System.Collections.Generic;
namespace Entity
{
    public class result_info_zone
    {
        public int seq { get; set; } // seq
        public int zone_id { get; set; } // zone_id (Primary key)
        public string zone_code { get; set; } // zone_code (length: 300)
        public string zone_name { get; set; } // zone_name (length: 300)
        public string comment { get; set; } // comment (length: 4000)
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
    }
}

