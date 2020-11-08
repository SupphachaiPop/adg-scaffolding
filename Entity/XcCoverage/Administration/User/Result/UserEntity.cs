using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Backend
{
    public class UserEntity
    {
        public Int32 total_record { get; set; }
        public Int32 user_id { get; set; }
        public Int32 company_id { get; set; }
        public Int32 branch_id { get; set; }
        public String branch_code { get; set; }
        public String branch_name { get; set; }
        public Int32 role_id { get; set; }
        public String role_name { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        public String fullname { get; set; }
        public String email { get; set; }
        public String comment { get; set; }
        public bool is_active { get; set; }
        public bool is_referred { get; set; }
        public int created_by { get; set; }
        public System.DateTime created_date { get; set; }
        public int modified_by { get; set; }
        public System.DateTime modified_date { get; set; }

        public String user_encrypt { get; set; }
        public bool is_manage { get; set; }
}
}
