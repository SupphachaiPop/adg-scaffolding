using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Backend
{
    public class RoleEntity
    {
        public Int32 total_record { get; set; }
        public Int32 role_id { get; set; }
        public Int32 company_id { get; set; }
        public String role_code { get; set; }
        public String role_name { get; set; }
        public String comment { get; set; }
        public bool is_active { get; set; }
        public bool is_referred { get; set; }
        public int created_by { get; set; }
        public System.DateTime created_date { get; set; }
        public int modified_by { get; set; }
        public System.DateTime modified_date { get; set; }
        public String role_encrypt { get; set; }
        public bool user_manage { get; set; }
        public List<RoleMenuEntity> roleMenuEntities { get; set; }
    }
}
