using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity;

namespace Service.Backend
{
    public partial class DataService
    {

        public List<location> GetLocationList()
        {
            return dataDao.GetLocationList();
        }

        public result_info_location GetLocationInfo(long id)
        {
            return dataDao.GetLocationInfo(id);
        }

        public List<result_search_location> SearchLocationList(param_search_location param)
        {
            return dataDao.SearchLocationList(param);
        }


        public int InsertLocation(param_create_location entity)
        {
            return dataDao.InsertLocation(entity);
        }

        public int UpdateLocation(param_create_location entity)
        {
            return dataDao.UpdateLocation(entity);
        }

        public int UpdateStatusLocation(location entity)
        {
            return dataDao.UpdateStatusLocation(entity);
        }

        public int DeleteLocation(location entity)
        {
            return dataDao.DeleteLocation(entity);
        }

        public int UpdateReferredLocation(location entity)
        {
            return dataDao.UpdateReferredLocation(entity);
        }
    }
}
