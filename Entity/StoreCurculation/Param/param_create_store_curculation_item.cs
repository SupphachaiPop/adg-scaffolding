using System.Collections.Generic;
namespace Entity
{
    public class param_create_store_curculation_item
    {
        public int store_curculation_item_id { get; set; } // store_curculation_item_id (Primary key)
        public int store_curculation_id { get; set; } // store_curculation_id
        public int store_id { get; set; } // store_id
        public string product_store_name { get; set; } // product_store_id
        public int qty_loaner { get; set; } // qty_loaner
        public int qty_return { get; set; } // qty_return
        public bool is_return { get; set; } // is_return
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

