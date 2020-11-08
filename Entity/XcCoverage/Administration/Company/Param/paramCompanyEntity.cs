using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Backend
{
    public class paramCompanyEntity
    {
        public Int32? pageNumber { get; set; }
        public Int32? pageSize { get; set; }
        public String search { get; set; }
        public bool? is_active { get; set; }
        public Int32? company_id { get; set; }
    }
}
