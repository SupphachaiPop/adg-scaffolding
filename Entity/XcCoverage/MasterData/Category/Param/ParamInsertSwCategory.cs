using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Backend
{
    public class ParamInsertSwCategory
    {
        public int category_id { get; set; }
        public string category_code { get; set; }
        public string category_name { get; set; }
        public int company_id { get; set; }
        public string comment { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public bool is_active { get; set; }
    }
}
