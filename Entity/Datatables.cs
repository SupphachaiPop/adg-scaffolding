using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class DataTables<T>
    {
        public DataTables()
        {
            this.data = new List<T>();
        }

        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<T> data { get; set; }

    }

    public class JQDT_Order
    {
        public string column { get; set; }
        public string dir { get; set; }
    }
}
