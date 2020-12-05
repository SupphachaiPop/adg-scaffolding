using System.Collections.Generic;

namespace Entity
{
    public class job
    {
        public int job_id { get; set; } // job_id (Primary key)
        public int zone_id { get; set; } // zone_id
        public int product_id { get; set; } // product_id
        public int amount { get; set; } // amount
        public int status_id { get; set; } // status_id
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted
        public virtual ICollection<job_delivery> job_delivery { get; set; } // job_delivery.fk_job_delivery_job_job_id
        public virtual product product { get; set; } // fk_job_product_product_id
        public virtual status status { get; set; } // fk_job_status_status_id
        public virtual zone zone { get; set; } // fk_job_zone_zone_id

        public job()
        {
            this.job_delivery = new List<job_delivery>();
        }
    }
}

