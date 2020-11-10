using System.Collections.Generic;
namespace Entity
{
    public class result_info_role_menu
    {
        public int role_menu_id { get; set; } // role_menu_id (Primary key)
        public int menu_id { get; set; } // menu_id
        public string menu_name_render { get; set; } //menu_name_render   
        public int menu_seq_render { get; set; } //menu_seq_render 
        public bool? is_display { get; set; } // is_display  

    }
}

