using System.Collections.Generic;
namespace Entity
{
    public class result_info_role
    {
        public int role_id { get; set; } // role_id (Primary key)
        public string role_code { get; set; } // role_code (length: 300)
        public string role_name { get; set; } // role_name (length: 300)
        public string comment { get; set; } // comment (length: 4000)
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public List<result_info_role_menu> role_menu { get; set; }

        public result_info_role()
        {
            this.role_menu = new List<result_info_role_menu>();
        }
    }
}

