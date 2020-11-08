using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Backend
{
    public class ParamUpdateSwCategory
    {
        public int category_id { get; set; }
        public string category_code { get; set; }
        public string category_name { get; set; }
        public string comment { get; set; }
        public int modified_by { get; set; }
        public DateTime modified_date { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
    }
}
