using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity;

namespace Service.Backend
{
    public partial class DataService
    {
        public List<status> GetStatusList()
        {
            return dataDao.GetStatusList();
        }    
    }
}
