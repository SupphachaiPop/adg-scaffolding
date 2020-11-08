using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity;
using Entity.Backend;

namespace Service.Backend
{
    public partial class dataService
    {
        public List<role> GetDataAllRole()
        {
            return dataDao.GetDataAll();
        }

        public result_info_role GetDataByID(long id)
        {
            return dataDao.GetDataByID(id);
        }

        public List<result_search_role> GetDataByCondition(param_search_role param)
        {
            return dataDao.GetDataByCondition(param);
        }


        public int InsertData(param_create_role entity)
        {
            return dataDao.InsertData(entity);
        }

        public int UpdateData(param_create_role entity)
        {
            return dataDao.UpdateData(entity);
        }

        public int UpdateDataStatus(role entity)
        {
            return dataDao.UpdateDataStatus(entity);
        }

        public int DeleteData(role entity)
        {
            return dataDao.DeleteData(entity);
        }

        public bool CheckIsReferred(role entity)
        {
            return dataDao.CheckIsReferred(entity);
        }
    }
}
