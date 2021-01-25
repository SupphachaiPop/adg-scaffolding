using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity;

namespace Service.Backend
{
    public partial class DataService
    {
        public List<vendor> GetVendorList()
        {
            return dataDao.GetVendorList();
        }

        public result_info_vendor GetVendorInfo(long id)
        {
            return dataDao.GetVendorInfo(id);
        }

        public List<result_search_vendor> SearchVendorList(param_search_vendor param)
        {
            return dataDao.SearchVendorList(param);
        }


        public int InsertVendor(param_create_vendor entity)
        {
            return dataDao.InsertVendor(entity);
        }

        public int UpdateVendor(param_create_vendor entity)
        {
            return dataDao.UpdateVendor(entity);
        }

        public int UpdateStatusVendor(vendor entity)
        {
            return dataDao.UpdateStatusVendor(entity);
        }

        public int UpdateRefferedVendor(vendor entity)
        {
            return dataDao.UpdateRefferedVendor(entity);
        }

        public int DeleteVendor(vendor entity)
        {
            return dataDao.DeleteVendor(entity);
        }
    }
}
