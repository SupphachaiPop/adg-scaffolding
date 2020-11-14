using System.Collections.Generic;
namespace Entity
{
    public class product
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

        public virtual ICollection<job> job { get; set; } // job.fk_job_product_product_id
        public virtual ICollection<product_specification> product_specification { get; set; } // product_specification.fk_product_specification_product_product_id
        public virtual specification specification { get; set; } // fk_product_specification_product_specification_id

        public product()
        {
            this.job = new List<job>();
            this.product_specification = new List<product_specification>();
        }
    }
}

