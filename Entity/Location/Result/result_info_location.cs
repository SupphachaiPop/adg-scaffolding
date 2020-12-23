using System.Collections.Generic;
namespace Entity
{
    public class result_info_location
    {
        public int location_id { get; set; } // location_id (Primary key)
        public int warehouse_id { get; set; } // warehouse_id
        public string location_code { get; set; } // location_code (length: 300)
        public string location_name { get; set; } // location_name (length: 300)
        public string comment { get; set; } // comment (length: 4000)
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public List<result_info_zone> zone { get; set; }
        public List<result_info_status> status { get; set; }
        public result_info_location()
        {
            this.zone = new List<result_info_zone>();
            this.status = new List<result_info_status>();
        }
    }
}

