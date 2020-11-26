using System.Collections.Generic;
namespace Entity
{
    public class result_search_customer
    {
        public int total_record { get; set; }
        public string id { get; set; } // user_id (Primary key)
        public int customer_id { get; set; } // customer_id (Primary key)
        public string customer_code { get; set; } // customer_code (length: 300)
        public string customer_name { get; set; } // customer_name (length: 300)
        public string tax_no { get; set; } // tax_no (length: 20)
        public string address { get; set; } // address (length: 300)
        public string email { get; set; } // email (length: 300)
        public string phone { get; set; } // phone (length: 10)
        public string comment { get; set; } // comment (length: 4000)      
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
    }
}

