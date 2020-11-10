using System.Collections.Generic;
namespace Entity
{
    public class param_create_role_menu
    {
        public int role_menu_id { get; set; } // role_menu_id (Primary key)
        public int role_id { get; set; } // role_id
        public int menu_id { get; set; } // menu_id
        public bool? is_display { get; set; } // is_display  
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

