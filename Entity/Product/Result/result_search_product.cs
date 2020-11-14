using System.Collections.Generic;
namespace Entity
{ 
    public class result_search_product
    {
        public int total_record { get; set; }
        public string id { get; set; } // product_id (Primary key)
        public int product_id { get; set; } // product_id (Primary key)
        public string product_code { get; set; } // product_code (length: 300)
        public string product_name { get; set; } // product_name (length: 300)
        public int stock_qty { get; set; } // stock_qty 
        public string comment { get; set; } // comment (length: 4000)
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
    }
}

