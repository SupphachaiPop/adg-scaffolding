using System.Collections.Generic;
namespace Entity
{ 
    public class result_info_store_curculation_item
    {
        public int seq { get; set; }
        public int store_curculation_item_id { get; set; } // store_curculation_item_id (Primary key)
        public int store_curculation_id { get; set; } // store_curculation_id
        public int store_id { get; set; } // store_id
        public string product_store_name { get; set; } // product_store_name
        public int qty_loaner { get; set; } // qty_loaner
        public int qty_return { get; set; } // qty_return
        public bool is_return { get; set; } // is_return
        public string comment { get; set; } // comment (length: 4000)
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted
    }
}

