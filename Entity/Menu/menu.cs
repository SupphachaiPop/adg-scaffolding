using System.Collections.Generic;
namespace Entity
{
    public class menu
    {
        public int menu_id { get; set; } // menu_id (Primary key)
        public int? parent_menu_id { get; set; } // parent_menu_id
        public int? seq { get; set; } // seq
        public string menu_name { get; set; } // menu_name (length: 300)
        public string menu_url { get; set; } // menu_url (length: 300)
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted
        public virtual ICollection<role_menu> role_menu { get; set; } // role_menu.fk_role_menu_menu_menu_id
        public menu()
        {
            this.role_menu = new List<role_menu>();
        }
    }
}

