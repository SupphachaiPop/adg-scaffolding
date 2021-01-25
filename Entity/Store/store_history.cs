using System.Collections.Generic;
namespace Entity
{
    public class store_history
    {
        public int store_history_id { get; set; } // store_history_id (Primary key)
        public int product_store_id { get; set; } // product_store_id
        public int qty { get; set; } // qty
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted
    }
}

