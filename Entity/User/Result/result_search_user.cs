using System.Collections.Generic;
namespace Entity
{ 
    public class result_search_user
    {
        public int total_record { get; set; }
        public string id { get; set; } // user_id (Primary key)
        public int user_id { get; set; } // user_id (Primary key)
        public string username { get; set; } // username (length: 300)
        public string full_name { get; set; } // first_name (length: 300)
        public string email { get; set; } // email (length: 300)
        public string phone { get; set; } // phone (length: 10)
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public string role_name { get; set; } // nick_name (length: 300)
    }
}

