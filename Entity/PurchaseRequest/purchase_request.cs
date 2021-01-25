using System.Collections.Generic;
namespace Entity
{
    public class purchase_request
    {
        public int purchase_request_id { get; set; } // purchase_request_id (Primary key)
        public string purchase_request_no { get; set; } // purchase_request_no (length: 20)
        public System.DateTime purchase_request_date { get; set; } // purchase_request_date
        public string ref_document_no { get; set; } // ref_document_no (length: 20)
        public int vendor_id { get; set; } // vendor_id
        public decimal sub_total { get; set; } // sub_total
        public decimal vat { get; set; } // vat
        public decimal total { get; set; } // total
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public bool? is_referred { get; set; } // is_referred
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted
        public virtual ICollection<purchase_request_item> purchase_request_item { get; set; } // purchase_request_item.FK_purchase_request_item_purchase_request
        public virtual vendor vendor { get; set; } // FK_purchase_request_vendor

        public purchase_request()
        {
            this.purchase_request_item = new List<purchase_request_item>();
        }
    }
}

