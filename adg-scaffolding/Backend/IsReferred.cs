using Entity;
using Service.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace adg_scaffolding.Backend
{
    public class IsReferred
    {
        #region Role 

        public void SetIsReferredUser(int id)
        {
            DataService dataService = new DataService();
            var user = dataService.GetUserList().Where(i => i.user_id == id).FirstOrDefault();
            if (user != null && user.is_referred != true)
            {
                dataService.UpdateStatusUser(user);
            }
        }
        #endregion

        #region User
        public void SetIsReferredRole(int id)
        {
            DataService dataService = new DataService();
            var role = dataService.GetRoleList().Where(i => i.role_id == id).FirstOrDefault();
            if (role != null && role.is_referred != true)
            {
                dataService.UpdateReferredRole(role);
            }
        }

        #endregion

        #region Warehouse 
        public void SetIsReferredWarehouse(int id)
        {
            DataService dataService = new DataService();
            var warehouse = dataService.GetWarehouseList().Where(i => i.warehouse_id == id).FirstOrDefault();
            if (warehouse != null && warehouse.is_referred != true)
            {
                dataService.UpdateIsRefferedWarehouse(warehouse);
            }
        }

        #endregion

        #region Location 
        public void SetIsReferredLocation(int id)
        {
            DataService dataService = new DataService();
            var location = dataService.GetLocationList().Where(i => i.warehouse_id == id).FirstOrDefault();
            if (location != null && location.is_referred != true)
            {
                dataService.UpdateReferredLocation(location);
            }
        }

        #endregion

        #region Product 
        public void SetIsReferredProduct(int id)
        {
            DataService dataService = new DataService();
            var product = dataService.GetProductList().Where(i => i.product_id == id).FirstOrDefault();
            if (product != null && product.is_referred != true)
            {
                dataService.UpdateReferredProduct(product);
            }
        }

        #endregion

        #region Specification 
        public void SetIsReferredSpecification(int id)
        {
            DataService dataService = new DataService();
            var specification = dataService.GetSpecificationList().Where(i => i.specification_id == id).FirstOrDefault();
            if (specification != null && specification.is_referred != true)
            {
                dataService.UpdateReferredSpecification(specification);
            }
        }

        #endregion
    }
}