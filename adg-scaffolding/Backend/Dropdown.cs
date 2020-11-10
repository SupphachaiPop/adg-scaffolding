using Entity;
using Service.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace adg_scaffolding.Backend
{
    public class Dropdown
    {
        #region Role 
        public DropDownList GetDropdownRole(DropDownList dropdown)
        {
            DataService dataService = new DataService();
            var role = dataService.GetRoleList();
            if (role.Count() > 0)
            {
                role = role.Where(i => i.is_active == true).ToList();
                dropdown.DataSource = role;
                role.Insert(0, new role
                {
                    role_id = 0,
                    role_name = "-- Select --"
                });

                dropdown.DataValueField = "role_id";
                dropdown.DataTextField = "role_name";
                dropdown.DataBind();
            }

            return dropdown;
        }
        #endregion
    }
}