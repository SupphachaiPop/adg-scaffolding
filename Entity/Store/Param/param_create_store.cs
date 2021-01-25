using System.Collections.Generic;
namespace Entity
{
    public class param_create_store
    {
        public int store_id { get; set; } // store_id (Primary key)
        public int warehouse_id { get; set; } // warehouse_id
        public int product_store_id { get; set; } // product_store_id
        public int qty { get; set; } // qty
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted

        public virtual product_store product_store { get; set; } // FK_Table_1_product_store_product_store_id
        public virtual warehouse warehouse { get; set; } // FK_store_warehouse
    }
}

