using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class loginEntity
    {
        public String Role { get; set; }
        public Int32 Sw_admin_id { get; set; }
        public String Sw_admin_name { get; set; }
        public String Sw_admin_email { get; set; }

        public Int32 Sw_entity_id { get; set; }
        public String Sw_entity_name { get; set; }


        public Int32 Sw_branch_id { get; set; }
        public String Sw_branch_code { get; set; }
        public String Sw_branch_name { get; set; }
        public String Sw_tel { get; set; }

        public String Sw_username { get; set; }
        public String Sw_password { get; set; }
        public DateTime Create_date { get; set; }
        public Int32 Create_by { get; set; }

        public string type { get; set; }
    }
}
