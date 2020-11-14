using System.Collections.Generic;
namespace Entity
{
    public class result_info_product
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
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public List<result_info_product_specification> product_specifications { get; set; }

        public result_info_product()
        {
            this.product_specifications = new List<result_info_product_specification>();
        }
    }
}

