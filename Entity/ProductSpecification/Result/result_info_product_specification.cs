using System.Collections.Generic;
namespace Entity
{
    public class result_info_product_specification
    {
        public int seq { get; set; } // seq
        public int product_specification_id { get; set; } // product_specification_id
        public int sub_specification_id { get; set; } // sub_specification_id
        public string product_specification_name { get; set; } // product_specification_name (length: 300)
        public string product_specification_value { get; set; } // product_specification_value (length: 300)
        public string comment { get; set; } // comment (length: 4000)
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
    }
}

