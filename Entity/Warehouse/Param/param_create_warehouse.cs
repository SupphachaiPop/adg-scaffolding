using System.Collections.Generic;
namespace Entity
{
    public class param_create_warehouse
    {
        public int warehouse_id { get; set; } // warehouse_id (Primary key)
        public string warehouse_code { get; set; } // warehouse_code (length: 300)
        public string warehouse_name { get; set; } // warehouse_name (length: 300)
        public string address { get; set; } // address (length: 4000)
        public string phone { get; set; } // phone (length: 10)
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted
    }
}

