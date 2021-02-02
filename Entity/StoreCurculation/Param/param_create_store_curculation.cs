using System.Collections.Generic;
namespace Entity
{
    public class param_create_store_curculation
    {
        public int store_curculation_id { get; set; } // store_curculation_id (Primary key)
        public string curculation_no { get; set; } // curculation_no
        public System.DateTime loaner_date { get; set; } // loaner_date
        public string loaner_name { get; set; } // loaner_name
        public System.DateTime? return_date { get; set; } // return_date
        public string return_name { get; set; } // return_name
        public string curculation_status { get; set; } // curculation_status
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted

        public virtual List<param_create_store_curculation_item> store_curculation_item { get; set; }

        public param_create_store_curculation()
        {
            this.store_curculation_item = new List<param_create_store_curculation_item>();
        }
    }
}

