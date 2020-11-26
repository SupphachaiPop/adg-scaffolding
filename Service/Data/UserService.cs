using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity;

namespace Service.Backend
{
    public partial class DataService
    {
        DataDao dataDao = new DataDao();
        public List<user> GetUserList()
        {
            return dataDao.GetUserList();
        }

        public result_info_user GetUserInfo(long id)
        {
            return dataDao.GetUserInfo(id);
        }

        public List<result_search_user> SearchUserList(param_search_user param)
        {
            return dataDao.SearchUserList(param);
        }


        public int InsertUser(param_create_user entity)
        {
            return dataDao.InsertUser(entity);
        }

        public int UpdateUser(param_create_user entity)
        {
            return dataDao.UpdateUser(entity);
        }

        public int UpdateStatusUser(user entity)
        {
            return dataDao.UpdateStatusUser(entity);
        }

        public int UpdateRefferedUser(user entity)
        {
            return dataDao.UpdateRefferedUser(entity);
        }

        public int DeleteUser(user entity)
        {
            return dataDao.DeleteUser(entity);
        }
    }
}
