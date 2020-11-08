using System.Collections.Generic;
namespace Entity
{ 
    public class user
    {
        public int user_id { get; set; } // user_id (Primary key)
        public int role_id { get; set; } // role_id
        public string username { get; set; } // username (length: 300)
        public string password { get; set; } // password (length: 300)
        public string first_name { get; set; } // first_name (length: 300)
        public string last_name { get; set; } // last_name (length: 300)
        public string nick_name { get; set; } // nick_name (length: 300)
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
        public virtual role role { get; set; } // fk_user_role_role_id

    }
}

