using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity;

namespace Service.Backend
{
    public partial class DataService
    {

        public List<role> GetRoleList()
        {
            return dataDao.GetRoleList();
        }

        public result_info_role GetRoleInfo(long id)
        {
            return dataDao.GetRoleInfo(id);
        }

        public List<result_search_role> SearchRoleList(param_search_role param)
        {
            return dataDao.SearchRoleList(param);
        }


        public int InsertRole(param_create_role entity)
        {
            return dataDao.InsertRole(entity);
        }

        public int UpdateRole(param_create_role entity)
        {
            return dataDao.UpdateRole(entity);
        }

        public int UpdateStatusRole(role entity)
        {
            return dataDao.UpdateStatusRole(entity);
        }

        public int DeleteRole(role entity)
        {
            return dataDao.DeleteRole(entity);
        }

        public int UpdateReferredRole(role entity)
        {
            return dataDao.UpdateReferredRole(entity);
        }
    }
}
