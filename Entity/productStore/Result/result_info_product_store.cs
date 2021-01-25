using System.Collections.Generic;
namespace Entity
{
    public class result_info_product_store
    {
        public int product_store_id { get; set; } // product_store_id (Primary key)
        public string product_store_code { get; set; } // product_store_code (length: 300)
        public string product_store_name { get; set; } // product_store_name (length: 300)
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

