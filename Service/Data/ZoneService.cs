using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity;

namespace Service.Backend
{
    public partial class DataService
    {
        public List<zone> GetZoneList()
        {
            return dataDao.GetZoneList();
        }    
    }
}
