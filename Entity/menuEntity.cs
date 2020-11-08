using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class menuEntity
    {



        public Int32 menu_id { get; set; }
        public Int32 list_no { get; set; }
        public string menu_code { get; set; }
        public string menu_name { get; set; }
        public string menu_name_en { get; set; }
        public Int32 menu_type { get; set; }
        public string comment { get; set; }
        public Int32 created_by { get; set; }
        public DateTime created_date { get; set; }
        public Int32 modified_by { get; set; }
        public DateTime modified_date { get; set; }
        public Boolean  is_active { get; set; }
        public Boolean is_deleted { get; set; }
    }
}
