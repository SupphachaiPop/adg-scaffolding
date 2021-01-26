using System.Collections.Generic;
namespace Entity
{ 
    public class result_search_store_history
    {
        public int total_record { get; set; }
        public string id { get; set; } // specification_id (Primary key)
        public int store_history_id { get; set; } // store_id (Primary key)
        public string location_name { get; set; } // warehouse_id
        public string zone_name { get; set; } // warehouse_id
        public string product_store_name { get; set; } // product_store_name
        public int qty { get; set; } // qty
        public bool is_inbound { get; set; } // is_inbound
        public string comment { get; set; } // comment (length: 4000)
        public System.DateTime created_date { get; set; } // created_date
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted
    }
}

