using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BaseResult<T>
    {
        public bool IS_SUCCESS { get; set; }
        public string MESSAGE { get; set; }
        public List<T> RESULT { get; set; }

        public BaseResult()
        {
            this.RESULT = new List<T>();
        }
    }

}
