using System.Collections.Generic;

namespace Entity
{
    public class product_specification
    {
        public int product_specification_id { get; set; } // product_specification_id (Primary key)
        public int product_id { get; set; } // product_id
        public int sub_specification_id { get; set; } // sub_specification_id
        public string sub_specification_value { get; set; } // sub_specification_value (length: 300)
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted
        public virtual product product { get; set; } // fk_product_specification_product_product_id
        public virtual sub_specification sub_specification { get; set; } // fk_product_specification_sub_specification_sub_specification_id

    }
}

