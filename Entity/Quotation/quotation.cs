using System.Collections.Generic;
namespace Entity
{
    public class quotation
    {
        public int quotation_id { get; set; } // quotation_id (Primary key)
        public string quotation_no { get; set; } // quotation_no (length: 20)
        public System.DateTime quotation_date { get; set; } // quotation_date
        public int customer_id { get; set; } // customer_id
        public decimal sub_total { get; set; } // sub_total
        public decimal discount { get; set; } // discount
        public decimal vat { get; set; } // vat
        public decimal security_money { get; set; } // security_money
        public decimal total { get; set; } // total
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public bool? is_referred { get; set; } // is_referred
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted
        public virtual ICollection<quotation_item> quotation_item { get; set; } // quotation_item.FK_quotation_item_quotation
        public virtual customer customer { get; set; } // FK_Table_1_customer_customer_id

        public quotation()
        {
            this.quotation_item = new List<quotation_item>();
        }
    }
}

