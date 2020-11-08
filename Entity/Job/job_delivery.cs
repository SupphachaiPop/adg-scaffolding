using System.Collections.Generic;
namespace Entity
{
    public class job_delivery
    {
        public int job_delivery_id { get; set; } // job_delivery_id (Primary key)
        public int customer_id { get; set; } // customer_id
        public int delivery_id { get; set; } // delivery_id
        public int job_id { get; set; } // job_id
        public int status_id { get; set; } // status_id
        public string comment { get; set; } // comment (length: 4000)
        public int? created_by { get; set; } // created_by
        public System.DateTime? created_date { get; set; } // created_date
        public int? modified_by { get; set; } // modified_by
        public System.DateTime? modified_date { get; set; } // modified_date
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active
        public bool? is_deleted { get; set; } // is_deleted

        public virtual customer customer { get; set; } // fk_job_delivery_customer_customer_id
        public virtual delivery delivery { get; set; } // fk_job_delivery_delivery_delivery_id
        public virtual job job { get; set; } // fk_job_delivery_job_job_id
        public virtual status status { get; set; } // fk_job_delivery_status_status_id

    }
}

