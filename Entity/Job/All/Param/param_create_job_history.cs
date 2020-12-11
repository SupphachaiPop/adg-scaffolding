using System.Collections.Generic;
namespace Entity
{
    public class param_create_job_history
    {
        public int job_history_id { get; set; } // job_history_id (Primary key)
        public int job_id { get; set; } // job_id 
        public int amount { get; set; } // amount
        public int status_id { get; set; } // status_id
        public int? parent_job_id { get; set; } // parent_job_id
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted
    }
}

