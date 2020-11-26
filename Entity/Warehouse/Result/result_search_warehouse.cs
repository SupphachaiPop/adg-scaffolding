using System.Collections.Generic;
namespace Entity
{ 
    public class result_search_warehouse
    {
        public int total_record { get; set; }
        public string id { get; set; } // user_id (Primary key)
        public int warehouse_id { get; set; } // warehouse_id (Primary key)
        public string warehouse_code { get; set; } // warehouse_code (length: 300)
        public string warehouse_name { get; set; } // warehouse_name (length: 300)
        public string phone { get; set; } // phone (length: 10)
        public string comment { get; set; } // comment (length: 4000)
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
    }
}

