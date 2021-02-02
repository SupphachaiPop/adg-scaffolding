using System.Collections.Generic;
namespace Entity
{
    public class result_info_store_curculation
    {
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
        public List<result_info_store_curculation_item> store_curculation_item { get; set; }

        public result_info_store_curculation()
        {
            this.store_curculation_item = new List<result_info_store_curculation_item>();
        }
    }
}

