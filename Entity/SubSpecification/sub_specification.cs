using System.Collections.Generic;
namespace Entity
{
    public class sub_specification
    {
        public int sub_specification_id { get; set; } // sub_specification_id (Primary key)
        public int specification_id { get; set; } // specification_id
        public string sub_specification_name { get; set; } // sub_specification_name (length: 300)
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted

        public virtual ICollection<product_specification> product_specification { get; set; } // product_specification.fk_product_specification_sub_specification_sub_specification_id

        public virtual specification specification { get; set; } // fk_sub_specification_specification_specification_id

        public sub_specification()
        {
            this.product_specification = new List<product_specification>();
        }
    }
}

