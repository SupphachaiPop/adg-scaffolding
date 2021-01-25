using System.Collections.Generic;
namespace Entity
{ 
    public class result_search_store
    {
        public int total_record { get; set; }
        public string id { get; set; } // specification_id (Primary key)
        public int store_id { get; set; } // store_id (Primary key)
        public int warehouse_id { get; set; } // warehouse_id
        public int product_store_id { get; set; } // product_store_id
        public string product_store_name { get; set; } // product_store_name
        public int qty { get; set; } // qty
        public string comment { get; set; } // comment (length: 4000)
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted
    }
}

