using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity;

namespace Service.Backend
{
    public partial class DataService
    {

        public List<product> GetProductList()
        {
            return dataDao.GetProductList();
        }

        public result_info_product GetProductInfo(long id)
        {
            return dataDao.GetProductInfo(id);
        }

        public List<result_search_product> SearchProductList(param_search_product param)
        {
            return dataDao.SearchProductList(param);
        }


        public int InsertProduct(param_create_product entity)
        {
            return dataDao.InsertProduct(entity);
        }

        public int UpdateProduct(param_create_product entity)
        {
            return dataDao.UpdateProduct(entity);
        }

        public int UpdateStatusProduct(product entity)
        {
            return dataDao.UpdateStatusProduct(entity);
        }

        public int DeleteProduct(product entity)
        {
            return dataDao.DeleteProduct(entity);
        }

        public int UpdateReferredProduct(product entity)
        {
            return dataDao.UpdateReferredProduct(entity);
        }
    }
}
