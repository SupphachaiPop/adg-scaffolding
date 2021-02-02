using System.Collections.Generic;
namespace Entity
{ 
    public class result_search_store_curculation
    {
        public int total_record { get; set; }
        public string id { get; set; } // specification_id (Primary key)
        public int store_curculation_id { get; set; } // store_curculation_id (Primary key)
        public string curculation_no { get; set; } // curculation_no
        public System.DateTime loaner_date { get; set; } // loaner_date
        public string loaner_name { get; set; } // loaner_name
        public System.DateTime? return_date { get; set; } // return_date
        public string return_name { get; set; } // return_name
        public string curculation_status { get; set; } // curculation_status
        public string comment { get; set; } // comment (length: 4000)
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted
    }
}

