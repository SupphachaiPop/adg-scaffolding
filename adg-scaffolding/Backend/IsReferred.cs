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
        public void SetIsReferredRole(int  id)
        {
            DataService dataService = new DataService();
            var role = dataService.GetRoleList().Where(i => i.role_id == id).FirstOrDefault(); 
            if (role != null && role.is_referred != true) 
            {
                dataService.UpdateReferredRole(role);
            }
        }

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
    }
}