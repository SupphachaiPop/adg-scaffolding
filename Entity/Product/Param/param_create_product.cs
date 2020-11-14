using System.Collections.Generic;
namespace Entity
{
    public class param_create_product
    {
        public int product_id { get; set; } // product_id (Primary key)
        public int specification_id { get; set; } // specification_id
        public string product_code { get; set; } // product_code (length: 300)
        public string product_name { get; set; } // product_name (length: 300)
        public string product_description { get; set; } // product_description (length: 4000)
        public int stock_qty { get; set; } // stock
        public decimal product_cost_price { get; set; } // product_cost
        public decimal product_sale_price { get; set; } // product_price
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted

        public List<param_create_product_specification> product_specifications { get; set; }

        public param_create_product()
        {
            this.product_specifications = new List<param_create_product_specification>();
        }
    }
}

