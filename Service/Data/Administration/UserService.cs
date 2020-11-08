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
        DataDao dataDao = new DataDao();

        public List<user> GetDataAll()
        {
            return dataDao.GetDataAll();
        }

        public result_info_user GetDataByID(long id)
        {
            return dataDao.GetDataByID(id);
        }

        public List<result_search_user> GetDataByCondition(param_search_user param)
        {
            return dataDao.GetDataByCondition(param);
        }


        public int InsertData(param_create_user entity)
        {
            return dataDao.InsertData(entity);
        }

        public int UpdateData(param_create_user entity)
        {
            return dataDao.UpdateData(entity);
        }

        public int UpdateDataStatus(user entity)
        {
            return dataDao.UpdateDataStatus(entity);
        }

        public int DeleteData(user entity)
        {
            return dataDao.DeleteData(entity);
        }

        public bool CheckIsReferred(user entity)
        {
            return dataDao.CheckIsReferred(entity);
        }
    }
}
