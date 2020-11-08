using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Backend
{
    public class RoleMenuEntity
    {
        public Int32 role_item_id { get; set; }
        public Int32 role_id { get; set; }
        public Int32 menu_id { get; set; }
        public String menu_name { get; set; }
        public bool is_display { get; set; }
        public String comment { get; set; }
        public int created_by { get; set; }
        public System.DateTime created_date { get; set; }
        public int modified_by { get; set; }
        public System.DateTime modified_date { get; set; }
        public bool is_active { get; set; }
        public bool is_referred { get; set; }
        public bool is_deleted { get; set; }
    }
}
