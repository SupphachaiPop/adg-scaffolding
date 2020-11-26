using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity;

namespace Service.Backend
{
    public partial class DataService
    {
        public List<customer> GetCustomerList()
        {
            return dataDao.GetCustomerList();
        }

        public result_info_customer GetCustomerInfo(long id)
        {
            return dataDao.GetCustomerInfo(id);
        }

        public List<result_search_customer> SearchCustomerList(param_search_customer param)
        {
            return dataDao.SearchCustomerList(param);
        }


        public int InsertCustomer(param_create_customer entity)
        {
            return dataDao.InsertCustomer(entity);
        }

        public int UpdateCustomer(param_create_customer entity)
        {
            return dataDao.UpdateCustomer(entity);
        }

        public int UpdateStatusCustomer(customer entity)
        {
            return dataDao.UpdateStatusCustomer(entity);
        }

        public int UpdateRefferedCustomer(customer entity)
        {
            return dataDao.UpdateRefferedCustomer(entity);
        }

        public int DeleteCustomer(customer entity)
        {
            return dataDao.DeleteCustomer(entity);
        }
    }
}
