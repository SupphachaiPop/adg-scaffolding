using System.Collections.Generic;
namespace Entity
{ 
    public class result_search_specification
    {
        public int total_record { get; set; }
        public string id { get; set; } // specification_id (Primary key)
        public int specification_id { get; set; } // specification_id (Primary key)
        public string specification_code { get; set; } // specification_code (length: 300)
        public string specification_name { get; set; } // specification_name (length: 300)
        public string comment { get; set; } // comment (length: 4000)
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
    }
}

