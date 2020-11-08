using System.Collections.Generic;
namespace Entity
{
    public class specification
    {
        public int specification_id { get; set; } // specification_id (Primary key)
        public string specification_name { get; set; } // specification_name (length: 300)
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted

        public virtual ICollection<product> product { get; set; } // product.fk_product_specification_product_specification_id
        public virtual ICollection<sub_specification> sub_specification { get; set; } // sub_specification.fk_sub_specification_specification_specification_id
        public specification()
        {
            this.product = new List<product>();
            this.sub_specification = new List<sub_specification>();
        }
    }
}

