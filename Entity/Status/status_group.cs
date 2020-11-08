using System.Collections.Generic;

namespace Entity
{
    public class status_group
    {       
        public int status_group_id { get; set; } // status_group_id (Primary key)
        public string status_group_name { get; set; } // status_group_name (length: 300)
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted

        public virtual ICollection<status> status { get; set; } // status.fk_status_status_group_status_group_id
        public status_group()
        {
            this.status = new List<status>();
        }
    }
}

