using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity;

namespace Service.Backend
{
    public partial class DataService
    {
        #region store
        public List<store> GetStoreList()
        {
            return dataDao.GetStoreList();
        }
        public result_info_store GetStoreInfo(long id)
        {
            return dataDao.GetStoreInfo(id);
        }
        public List<result_search_store> SearchStoreList(param_search_store param)
        {
            return dataDao.SearchStoreList(param);
        }

        public int InsertStore(param_create_store entity)
        {
            return dataDao.InsertStore(entity);
        }
        public int UpdateStore(param_create_store entity)
        {
            return dataDao.UpdateStore(entity);
        }
        public int UpdateStatusStore(store entity)
        {
            return dataDao.UpdateStatusStore(entity);
        }
        public int UpdateRefferedStore(store entity)
        {
            return dataDao.UpdateRefferedStore(entity);
        }
        public int DeleteStore(store entity)
        {
            return dataDao.DeleteStore(entity);
        }
        #endregion

        #region ProductStore
        public List<product_store> GetProductStoreList()
        {
            return dataDao.GetProductStoreList();
        }
        public result_info_product_store GetProductStoreInfo(long id)
        {
            return dataDao.GetProductStoreInfo(id);
        }
        public List<result_search_product_store> SearchProductStoreList(param_search_product_store param)
        {
            return dataDao.SearchProductStoreList(param);
        }
        public int InsertProductStore(param_create_product_store entity)
        {
            return dataDao.InsertProductStore(entity);
        }
        public int UpdateProductStore(param_create_product_store entity)
        {
            return dataDao.UpdateProductStore(entity);
        }
        public int UpdateStatusProductStore(product_store entity)
        {
            return dataDao.UpdateStatusProductStore(entity);
        }
        public int UpdateRefferedProductStore(product_store entity)
        {
            return dataDao.UpdateRefferedProductStore(entity);
        }
        public int DeleteProductStore(product_store entity)
        {
            return dataDao.DeleteProductStore(entity);
        }
        #endregion

        public List<result_info_store_history> SearchStoreHistoryList(int productStoreId)
        {
            return dataDao.SearchStoreHistoryList(productStoreId);
        }
    }
}
