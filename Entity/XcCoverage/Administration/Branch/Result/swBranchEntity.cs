using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Backend
{
    public class swBranchEntity
    {
        public Int32 total_record { get; set; }
        public Int32 branch_id { get; set; }
        public Int32 company_id { get; set; }
        public String company_name { get; set; }
        public String branch_code { get; set; }
        public String branch_no { get; set; }
        public String branch_name { get; set; }
        public int branch_type { get; set; }
        public String billing_address { get; set; }
        public String shipping_address { get; set; }
        public String contact_name { get; set; }
        public String phone { get; set; }
        public String email { get; set; }
        public String comment { get; set; }
        public bool is_active { get; set; }
        public bool is_referred { get; set; }
        public int created_by { get; set; }
        public System.DateTime created_date { get; set; }
        public int modified_by { get; set; }
        public System.DateTime modified_date { get; set; }

        public String branch_encrypt { get; set; }
    }
}
