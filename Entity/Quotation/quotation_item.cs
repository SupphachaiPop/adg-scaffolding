using System.Collections.Generic;

namespace Entity
{
    public class quotation_item
    {
        public int quotation_item_id { get; set; } // quotation_item_id (Primary key)
        public int quotation_id { get; set; } // quotation_id
        public int? template_id { get; set; } // template_id
        public string description { get; set; } // description (length: 300)
        public decimal qty { get; set; } // qty
        public int uom_id { get; set; } // uom_id
        public int qty_day { get; set; } // qty_day
        public decimal unit_price { get; set; } // unit_price
        public decimal total { get; set; } // total
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public bool? is_referred { get; set; } // is_referred
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted
        public virtual quotation quotation { get; set; } // FK_quotation_item_quotation
        public virtual uom uom { get; set; } // FK_quotation_item_uom

    }
}

