using System.Collections.Generic;
namespace Entity
{
    public class result_info_store
    {
        public int store_id { get; set; } // store_id (Primary key)
        public int warehouse_id { get; set; } // warehouse_id
        public int product_store_id { get; set; } // product_store_id
        public int qty { get; set; } // qty
        public string comment { get; set; } // comment (length: 4000)   
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public List<result_info_store_history> store_history { get; set; }

        public result_info_store()
        {
            this.store_history = new List<result_info_store_history>();
        }
    }
}

