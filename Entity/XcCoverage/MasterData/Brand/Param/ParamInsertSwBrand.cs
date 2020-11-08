using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Backend
{
    public class ParamInsertSwBrand
    {
        public int brand_id { get; set; }
        public string brand_code { get; set; }
        public string brand_name { get; set; }
        public int company_id { get; set; }
        public string comment { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public bool is_active { get; set; }
    }
}
