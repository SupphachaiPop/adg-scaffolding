using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Backend
{
    public class RoleMenuMasterEntity
    {
        public RoleMenuMasterEntity()
        {
            this.child_menu = new List<RoleMenuMasterEntity>();
        }
        public int menu_id { get; set; }
        public int? parent_menu_id { get; set; }
        public int list_no { get; set; }
        public string menu_code { get; set; }
        public string menu_name { get; set; }
        public string menu_name_eng { get; set; }
        public int menu_type { get; set; }
        public string url { get; set; }
        public string comment { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int modified_by { get; set; }
        public DateTime modified_date { get; set; }
        public bool is_referred { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
        public string parent_menu_name { get; set; }
        public string parent_menu_name_eng { get; set; }
        public string parent_url { get; set; }
        public int role_item_id { get; set; }
        public int role_id { get; set; }
        public bool is_display { get; set; }

        public List<RoleMenuMasterEntity> child_menu { get; set; }
    }
}
