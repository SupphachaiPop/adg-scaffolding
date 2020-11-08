using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Backend
{
    public class CompanyTypeEntity
    {
        public Int32 total_record { get; set; }
        public Int32 company_type_id { get; set; }
        public String company_type_name { get; set; }
        public String comment { get; set; }
        public bool is_active { get; set; }
        public bool is_referred { get; set; }
    }
}
