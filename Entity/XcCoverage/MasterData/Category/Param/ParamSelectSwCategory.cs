using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Backend
{
    public class ParamSelectSwCategory
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string order { get; set; }
        public string search { get; set; }
        public bool is_active { get; set; }
        public int company_id { get; set; }
    }
}
