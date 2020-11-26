using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity;

namespace Service.Backend
{
    public partial class DataService
    {
        public List<delivery> GetDeliveryList()
        {
            return dataDao.GetDeliveryList();
        }

        public result_info_delivery GetDeliveryInfo(long id)
        {
            return dataDao.GetDeliveryInfo(id);
        }

        public List<result_search_delivery> SearchDeliveryList(param_search_delivery param)
        {
            return dataDao.SearchDeliveryList(param);
        }


        public int InsertDelivery(param_create_delivery entity)
        {
            return dataDao.InsertDelivery(entity);
        }

        public int UpdateDelivery(param_create_delivery entity)
        {
            return dataDao.UpdateDelivery(entity);
        }

        public int UpdateStatusDelivery(delivery entity)
        {
            return dataDao.UpdateStatusDelivery(entity);
        }

        public int UpdateRefferedDelivery(delivery entity)
        {
            return dataDao.UpdateRefferedDelivery(entity);
        }

        public int DeleteDelivery(delivery entity)
        {
            return dataDao.DeleteDelivery(entity);
        }
    }
}
