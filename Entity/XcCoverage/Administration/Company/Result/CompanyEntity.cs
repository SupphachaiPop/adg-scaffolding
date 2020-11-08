using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Backend
{
    public class CompanyEntity
    {
        public Int32 total_record { get; set; }
        public Int32 company_id { get; set; }
        public String company_code { get; set; }
        public String company_name { get; set; }
        public int company_type { get; set; }
        public String company_type_name { get; set; }
        public String tax_no { get; set; }
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
        public String company_encrypt { get; set; }
        public bool user_manage { get; set; }
        public List<CompanyMenuEntity> companyMenuEntities { get; set; }
    }
}
