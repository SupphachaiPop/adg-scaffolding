using System.Collections.Generic;
namespace Entity
{
    public class param_create_specification
    {
        public int specification_id { get; set; } // specification_id (Primary key)
        public string specification_code { get; set; } // specification_code (length: 300)
        public string specification_name { get; set; } // specification_name (length: 300)
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted

        public List<param_create_sub_specification> sub_specifications { get; set; }

        public param_create_specification()
        {
            this.sub_specifications = new List<param_create_sub_specification>();
        }
    }
}

