using System.Collections.Generic;
namespace Entity
{ 
    public class result_info_delivery
    {
        public int delivery_id { get; set; } // delivery_id (Primary key)

        public string delivery_code { get; set; } // delivery_name (length: 300)
        public string delivery_name { get; set; } // delivery_name (length: 300)
        public string tax_no { get; set; } // tax_no (length: 20)
        public string address { get; set; } // address (length: 300)
        public string email { get; set; } // email (length: 300)
        public string phone { get; set; } // phone (length: 10)
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
