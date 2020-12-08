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

        #region Specification 
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

        #region Warehouse 
        public DropDownList GetDropdownWarehouse(DropDownList dropdown)
        {
            DataService dataService = new DataService();
            var warehouseList = dataService.GetWarehouseList();
            if (warehouseList.Count() > 0)
            {
                dropdown.DataSource = warehouseList;
                warehouseList.Insert(0, new warehouse
                {
                    warehouse_id = 0,
                    warehouse_name = "-- Select --",
                    is_active = true
                });

                dropdown.DataValueField = "warehouse_id";
                dropdown.DataTextField = "warehouse_name";
                dropdown.DataBind();
                foreach (ListItem item in dropdown.Items)
                {
                    if (!warehouseList.Where(i => i.warehouse_id.ToString() == item.Value).Select(s => s.is_active.Value).FirstOrDefault())
                    {
                        item.Attributes.Add("style", "color:gray;");
                        item.Attributes.Add("disabled", "disabled");
                    }
                };

            }

            return dropdown;
        }
        #endregion

        #region Product 
        public DropDownList GetDropdownProduct(DropDownList dropdown)
        {
            DataService dataService = new DataService();
            var productList = dataService.GetProductList();
            if (productList.Count() > 0)
            {
                productList.ForEach(i =>
                {
                    i.product_name = i.product_code + " : " + i.product_name;
                });
                dropdown.DataSource = productList;
                productList.Insert(0, new product
                {
                    product_id = 0,
                    product_name = "-- Select --",
                    is_active = true
                });

                dropdown.DataValueField = "product_id";
                dropdown.DataTextField = "product_name";
                dropdown.DataBind();
                foreach (ListItem item in dropdown.Items)
                {
                    if (!productList.Where(i => i.product_id.ToString() == item.Value).Select(s => s.is_active.Value).FirstOrDefault())
                    {
                        item.Attributes.Add("style", "color:gray;");
                        item.Attributes.Add("disabled", "disabled");
                    }
                };

            }

            return dropdown;
        }
        #endregion

        #region Location 
        public DropDownList GetDropdownLocation(DropDownList dropdown)
        {
            DataService dataService = new DataService();
            var LocationList = dataService.GetLocationList();
            if (LocationList.Count() > 0)
            {
                LocationList.ForEach(i =>
                {
                    i.location_name = i.location_code + " : " + i.location_name;
                });
                dropdown.DataSource = LocationList;
                LocationList.Insert(0, new location
                {
                    location_id = 0,
                    location_name = "-- Select --",
                    is_active = true
                });

                dropdown.DataValueField = "location_id";
                dropdown.DataTextField = "location_name";
                dropdown.DataBind();
                foreach (ListItem item in dropdown.Items)
                {
                    if (!LocationList.Where(i => i.location_id.ToString() == item.Value).Select(s => s.is_active.Value).FirstOrDefault())
                    {
                        item.Attributes.Add("style", "color:gray;");
                        item.Attributes.Add("disabled", "disabled");
                    }
                };

            }

            return dropdown;
        }
        #endregion

        #region Zone 
        public DropDownList GetDropdownZone(DropDownList dropdown)
        {
            DataService dataService = new DataService();
            var zoneList = dataService.GetZoneList();
            if (zoneList.Count() > 0)
            {
                zoneList.ForEach(i =>
                {
                    i.zone_name = i.zone_code + " : " + i.zone_name;
                });
                dropdown.DataSource = zoneList;
                zoneList.Insert(0, new zone
                {
                    zone_id = 0,
                    zone_name = "-- Select --",
                    is_active = true
                });

                dropdown.DataValueField = "zone_id";
                dropdown.DataTextField = "zone_name";
                dropdown.DataBind();
                foreach (ListItem item in dropdown.Items)
                {
                    if (!zoneList.Where(i => i.zone_id.ToString() == item.Value).Select(s => s.is_active.Value).FirstOrDefault())
                    {
                        item.Attributes.Add("style", "color:gray;");
                        item.Attributes.Add("disabled", "disabled");
                    }
                };

            }

            return dropdown;
        }
        #endregion

        #region Zone 
        public DropDownList GetDropdownJob(DropDownList dropdown, int zoneId)
        {
            DataService dataService = new DataService();
            var jobList = dataService.GetActiveJobList(pZoneId: zoneId);
            if (jobList.Count() > 0)
            {
                dropdown.DataSource = jobList;
                jobList.Insert(0, new job
                {
                    job_id = 0,
                    product_name = "-- Select --",
                    is_active = true
                });

                dropdown.DataValueField = "job_id";
                dropdown.DataTextField = "product_name";
                dropdown.DataBind();
                foreach (ListItem item in dropdown.Items)
                {
                    if (!jobList.Where(i => i.zone_id.ToString() == item.Value).Select(s => s.is_active.Value).FirstOrDefault())
                    {
                        item.Attributes.Add("style", "color:gray;");
                        item.Attributes.Add("disabled", "disabled");
                    }
                };

            }

            return dropdown;
        }
        #endregion
    }
}