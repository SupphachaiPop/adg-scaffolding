using System.Collections.Generic;
namespace Entity
{
    public class uom
    {
        public int uom_id { get; set; } // uom_id (Primary key)
        public string uom_code { get; set; } // uom_code (length: 300)
        public string uom_name { get; set; } // uom_name (length: 300)
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public bool? is_referred { get; set; } // is_referred
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted
    }
}

