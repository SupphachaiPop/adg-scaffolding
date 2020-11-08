using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BaseEntity
    {
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
    }
}
