using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity;

namespace Service.Backend
{
    public partial class DataService
    {

        public List<specification> GetSpecificationList()
        {
            return dataDao.GetSpecificationList();
        }

        public result_info_specification GetSpecificationInfo(long id)
        {
            return dataDao.GetSpecificationInfo(id);
        }

        public List<result_search_specification> SearchSpecificationList(param_search_specification param)
        {
            return dataDao.SearchSpecificationList(param);
        }


        public int InsertSpecification(param_create_specification entity)
        {
            return dataDao.InsertSpecification(entity);
        }

        public int UpdateSpecification(param_create_specification entity)
        {
            return dataDao.UpdateSpecification(entity);
        }

        public int UpdateStatusSpecification(specification entity)
        {
            return dataDao.UpdateStatusSpecification(entity);
        }

        public int DeleteSpecification(specification entity)
        {
            return dataDao.DeleteSpecification(entity);
        }

        public int UpdateReferredSpecification(specification entity)
        {
            return dataDao.UpdateReferredSpecification(entity);
        }
    }
}
