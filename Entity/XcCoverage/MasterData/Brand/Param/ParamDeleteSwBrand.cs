﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Backend
{
    public class ParamDeleteSwBrand
    {
        public int brand_id { get; set; }
        public int modified_by { get; set; }
        public DateTime modified_date { get; set; }
        public int success_row { get; set; }
    }
}
