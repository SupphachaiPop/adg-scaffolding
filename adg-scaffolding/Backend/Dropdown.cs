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

        public DropDownList GetDropdownLocationDestination(DropDownList dropdown, int locationId)
        {
            DataService dataService = new DataService();
            var LocationList = dataService.GetLocationList().Where(i => i.location_id != locationId).ToList();
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
        public DropDownList GetDropdownZone(DropDownList dropdown, int locationId)
        {
            DataService dataService = new DataService();
            var zoneList = dataService.GetZoneList().Where(i => i.location_id == locationId).ToList();
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

        #region job 
        public DropDownList GetDropdownJobHaveInStock(DropDownList dropdown, int zoneId)
        {
            DataService dataService = new DataService();
            var jobList = dataService.GetActiveJobList(pZoneId: zoneId).Where(i => i.amount > 0).ToList();
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
                    if (!jobList.Where(i => i.job_id.ToString() == item.Value).Select(s => s.is_active.Value).FirstOrDefault())
                    {
                        item.Attributes.Add("style", "color:gray;");
                        item.Attributes.Add("disabled", "disabled");
                    }
                };

            }

            return dropdown;
        }
        #endregion

        #region job 
        public DropDownList GetDropdownStatusJob(DropDownList dropdown, int locationId)
        {
            DataService dataService = new DataService();
            List<status> statuslist = new List<status>();

            statuslist = dataService.GetStatusList().Where(i => i.location_id == locationId).ToList() ;
            //if (locationId == (int)_BaseConst.location.Cleaning)
            //{
            //    statuslist = statuslist.Where(i => i.status_id == (int)_BaseConst.status_job.cleaning_processing).ToList();
            //}
            //else if (locationId == (int)_BaseConst.location.Delivery)
            //{
            //    statuslist = statuslist.Where(i => i.status_id == (int)_BaseConst.status_job.delivery_processing).ToList();
            //}
            //else if (locationId == (int)_BaseConst.location.Repair)
            //{
            //    List<int> statusIds = new List<int>();
            //    statusIds.Add((int)_BaseConst.status_job.repair_processing);
            //    statusIds.Add((int)_BaseConst.status_job.repair_await_part);
            //    statuslist = statuslist.Where(i => statusIds.Any(a => i.status_id == a)).ToList();
            //}

            if (statuslist.Count() > 0)
            {
                dropdown.DataSource = statuslist;
                dropdown.DataValueField = "status_id";
                dropdown.DataTextField = "status_name";
                dropdown.DataBind();
                foreach (ListItem item in dropdown.Items)
                {
                    if (!statuslist.Where(i => i.status_id.ToString() == item.Value).Select(s => s.is_active.Value).FirstOrDefault())
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