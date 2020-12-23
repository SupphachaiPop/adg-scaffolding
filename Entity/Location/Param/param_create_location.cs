using System.Collections.Generic;
namespace Entity
{
    public class param_create_location
    {
        public int location_id { get; set; } // location_id (Primary key)
        public int warehouse_id { get; set; } // warehouse_id
        public string location_code { get; set; } // location_code (length: 300)
        public string location_name { get; set; } // location_name (length: 300)
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted

        public List<param_create_zone> zone { get; set; }
        public List<param_create_status> status { get; set; }
        public param_create_location()
        {
            this.zone = new List<param_create_zone>();
            this.status = new List<param_create_status>();
        }
    }
}

