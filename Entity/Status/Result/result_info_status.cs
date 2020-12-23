using System.Collections.Generic;
namespace Entity
{
    public class result_info_status
    {
        public int seq { get; set; } // seq
        public int status_id { get; set; } // status_id (Primary key)
        public int location_id { get; set; } // location_id
        public string status_name { get; set; } // status_name (length: 300)
        public string status_name_en { get; set; } // status_name_en (length: 300)
        public string status_description { get; set; } // status_description (length: 300)
        public string comment { get; set; } // comment (length: 4000)
        public bool? is_referred { get; set; } // is_referred
        public bool? is_active { get; set; } // is_active       
       
    }
}

