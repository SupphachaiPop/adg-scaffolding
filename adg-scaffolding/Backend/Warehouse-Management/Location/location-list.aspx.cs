using Definitions;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service.Backend;
using Definitions.Static_Text;

namespace adg_scaffolding.Backend.Warehouse_Management.Location
{
    [System.Web.Script.Services.ScriptService]
    public partial class location_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetDropdownList();
        }

        public void SetDropdownList() {
            Dropdown dropdown = new Dropdown();
            ddlWarehouse = dropdown.GetDropdownWarehouse(ddlWarehouse);
        }

        protected void btnCreateLocation_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.LocationInfoUrl);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static object GetData(int draw,
                                                int start,
                                                int length,
                                                List<JQDT_Order> order,
                                                string txtSearch,
                                                int? warehouse_id,
                                                bool? is_active)
        {

            param_search_location param = new param_search_location();
            DataTables<result_search_location> result = new DataTables<result_search_location>();

            try
            {
                JQDT_Order firstOrder = order.FirstOrDefault();
                int StartRec = start;
                int TotalRecords = 0;
                string OrderField = firstOrder.column;
                string OrderDir = firstOrder.dir;

                param.search = txtSearch.Trim();
                param.is_active = is_active.HasValue ? is_active : null;
                param.warehouse_id = warehouse_id != 0 ? warehouse_id : null;
                param.pageSize = length;
                param.pageNumber = (StartRec + param.pageSize) / param.pageSize;

                List<result_search_location> locationList = LoadData(param: param,
                                                      Order: OrderField,
                                                      OrderDir: OrderDir);

                if (locationList.Count() > 0)
                {
                    TotalRecords = locationList.Count();
                    result.draw = Convert.ToInt32(draw);
                    result.recordsTotal = TotalRecords;
                    result.recordsFiltered = TotalRecords;
                    result.data = locationList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private static List<result_search_location> LoadData(param_search_location param,
                                                          String Order,
                                                          String OrderDir)
        {
            DataService dataService = new DataService();
            List<result_search_location> locationList = new List<result_search_location>();

            try
            {
                locationList = dataService.SearchLocationList(param: param);
                locationList = buildDataForDisplay(locationList: locationList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return locationList;
        }

        private static List<result_search_location> buildDataForDisplay(List<result_search_location> locationList)
        {
            UtilityCommon utilityCommon = new UtilityCommon();
            locationList = locationList.Select(e =>
            {
                e.location_code = !string.IsNullOrEmpty(e.location_code) ? e.location_code : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.location_name = !string.IsNullOrEmpty(e.location_name) ? e.location_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.comment = !string.IsNullOrEmpty(e.comment) ? e.comment : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.id = utilityCommon.EncryptDataUrlEncoder(textData: e.location_id.ToString(),
                                                                   encryptionkey: StaticKeys.DataEncrypteKey);
                return e;

            }).ToList();

            return locationList;
        }

        [WebMethod]
        public static string UpdateStatus(string id, bool is_active)
        {
            DataService dataService = new DataService();
            location location = new location();

            var user = userLogin();
            location.location_id = DecryptCode(id);
            location.is_active = is_active;
            location.modified_by = user.user_id;
            if (dataService.UpdateStatusLocation(location) > 0)
            {
                return "success";
            }

            return "failure";
        }

        [WebMethod]
        public static bool DeleteData(string id)
        {
            DataService dataService = new DataService();
            location location = new location();
            var user = userLogin();

            location.location_id = DecryptCode(id);
            location.modified_by = user.user_id;

            var isReferred = dataService.GetLocationInfo(location.location_id).is_referred;
            if (!isReferred.Value)
            {
                if (dataService.DeleteLocation(location) > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static result_user_login userLogin()
        {
            result_user_login user = new result_user_login();
            if ((HttpContext.Current.Session["userLoginBackend"] != null))
            {
                user = (result_user_login)HttpContext.Current.Session["userLoginBackend"];
            }

            return user;
        }

        public static int DecryptCode(string enCryptCode)
        {
            UtilityCommon utilityCommon = new UtilityCommon();
            var id = utilityCommon.DecryptDataUrlEncoder<int>(encryptedText: enCryptCode,
                                                              encryptionkey: StaticKeys.DataEncrypteKey);

            return id;
        }
    }
}