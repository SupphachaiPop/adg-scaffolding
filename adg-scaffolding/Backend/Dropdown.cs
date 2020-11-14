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
            var roleList = dataService.GetRoleList();
            if (roleList.Count() > 0)
            {
                dropdown.DataSource = roleList;
                roleList.Insert(0, new role
                {
                    role_id = 0,
                    role_name = "-- Select --",
                    is_active = true
                });

                dropdown.DataValueField = "role_id";
                dropdown.DataTextField = "role_name";
                dropdown.DataBind();
                foreach (ListItem item in dropdown.Items)
                {
                    if (!roleList.Where(i => i.role_id.ToString() == item.Value).Select(s => s.is_active.Value).FirstOrDefault())
                    {
                        item.Attributes.Add("style", "color:gray;");
                        item.Attributes.Add("disabled", "disabled");
                    }
                };

            }

            return dropdown;
        }
        #endregion

        #region Role 
        public DropDownList GetDropdownSpecification(DropDownList dropdown)
        {
            DataService dataService = new DataService();
            var specificationList = dataService.GetSpecificationList();
            if (specificationList.Count() > 0)
            {
                dropdown.DataSource = specificationList;
                specificationList.Insert(0, new specification
                {
                    specification_id = 0,
                    specification_name = "-- Select --"
                });

                dropdown.DataValueField = "specification_id";
                dropdown.DataTextField = "specification_name";

                foreach (ListItem item in dropdown.Items)
                {
                    if (!specificationList.Where(i => i.specification_id.ToString() == item.Value).Select(s => s.is_active.Value).FirstOrDefault())
                    {
                        item.Attributes.Add("style", "color:gray;");
                        item.Attributes.Add("disabled", "disabled");
                    }
                };
                dropdown.DataBind();
            }

            return dropdown;
        }
        #endregion
    }
}