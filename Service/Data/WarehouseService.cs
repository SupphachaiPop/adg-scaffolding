using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity;

namespace Service.Backend
{
    public partial class DataService
    {
        public List<warehouse> GetWarehouseList()
        {
            return dataDao.GetWarehouseList();
        }

        public result_info_warehouse GetWarehouseInfo(long id)
        {
            return dataDao.GetWarehouseInfo(id);
        }

        public List<result_search_warehouse> SearchWarehouseList(param_search_warehouse param)
        {
            return dataDao.SearchWarehouseList(param);
        }


        public int InsertWarehouse(param_create_warehouse entity)
        {
            return dataDao.InsertWarehouse(entity);
        }

        public int UpdateWarehouse(param_create_warehouse entity)
        {
            return dataDao.UpdateWarehouse(entity);
        }

        public int UpdateStatusWarehouse(warehouse entity)
        {
            return dataDao.UpdateStatusWarehouse(entity);
        }

        public int UpdateIsRefferedWarehouse(warehouse entity)
        {
            return dataDao.UpdateRefferedWarehouse(entity);
        }

        public int DeleteWarehouse(warehouse entity)
        {
            return dataDao.DeleteWarehouse(entity);
        }
    }
}
