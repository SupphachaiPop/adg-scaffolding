using System.Collections.Generic;
namespace Entity
{
    public class result_info_specification
    {
        public int specification_id { get; set; } // specification_id (Primary key)
        public string specification_code { get; set; } // specification_code (length: 300)
        public string specification_name { get; set; } // specification_name (length: 300)
        public string comment { get; set; } // comment (length: 4000)
        public bool? is_referred { get; set; } // is_active
        public bool? is_active { get; set; } // is_active
        public List<result_info_sub_specification> sub_specifications { get; set; }

        public result_info_specification()
        {
            this.sub_specifications = new List<result_info_sub_specification>();
        }
    }
}

