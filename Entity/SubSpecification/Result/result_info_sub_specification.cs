using System.Collections.Generic;
namespace Entity
{
    public class result_info_sub_specification
    {
        public int seq { get; set; } // seq
        public int sub_specification_id { get; set; } // sub_specification_id (Primary key)
        public string sub_specification_name { get; set; } // sub_specification_name (length: 300)
        public string comment { get; set; } // comment (length: 4000)
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
    }
}

