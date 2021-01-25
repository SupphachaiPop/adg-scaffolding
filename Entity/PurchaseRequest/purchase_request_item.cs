using System.Collections.Generic;
namespace Entity
{
    public class purchase_request_item
    {
        public int purchase_request_item_id { get; set; } // purchase_request_item_id (Primary key)
        public int purchase_request_id { get; set; } // purchase_request_id
        public int? product_store_id { get; set; } // product_store_id
        public int? product_id { get; set; } // product_id
        public string product_description { get; set; } // product_description (length: 300)
        public int qty { get; set; } // qty
        public int uom_id { get; set; } // uom_id
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
        public virtual product product { get; set; } // FK_purchase_request_item_product_product_id
        public virtual product_store product_store { get; set; } // FK_purchase_request_item_product_store_product_store_id
        public virtual purchase_request purchase_request { get; set; } // FK_purchase_request_item_purchase_request
        public virtual uom uom { get; set; } // FK_purchase_request_item_uom

    }
}

