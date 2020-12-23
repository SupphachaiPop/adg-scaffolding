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
using Definitions;

namespace adg_scaffolding.Backend.Job_Management.Job
{
    [System.Web.Script.Services.ScriptService]
    public partial class job_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetDataDropDownList();
        }

        private void SetDataDropDownList()
        {
            Dropdown dropdown = new Dropdown();
            ddlWarehouse = dropdown.GetDropdownWarehouse(ddlWarehouse);
            ddlLocation = dropdown.GetDropdownLocation(ddlLocation);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static object GetData(int draw,
                                    int start,
                                    int length,
                                    List<JQDT_Order> order,
                                    string txtSearch,
                                    int warehouseId,
                                    int LocationId)
        {

            param_search_job_zone param = new param_search_job_zone();
            DataTables<result_search_job_zone> result = new DataTables<result_search_job_zone>();

            try
            {

                JQDT_Order firstOrder = order.FirstOrDefault();
                int TotalRecords = 0;
                string OrderField = firstOrder.column;
                string OrderDir = firstOrder.dir;

                param.search = txtSearch.Trim();
                param.pageSize = length;
                param.pageNumber = (start + length) / length;
                param.warehouse_id = warehouseId;
                param.location_id = LocationId;

                List<result_search_job_zone> jobList = LoadData(param: param,
                                                      Order: OrderField,
                                                      OrderDir: OrderDir);

                if (jobList.Count() > 0)
                {
                    TotalRecords = jobList.FirstOrDefault().total_record;
                    result.draw = Convert.ToInt32(draw);
                    result.recordsTotal = TotalRecords;
                    result.recordsFiltered = TotalRecords;
                    result.data = jobList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private static List<result_search_job_zone> LoadData(param_search_job_zone param,
                                                          String Order,
                                                          String OrderDir)
        {
            DataService dataService = new DataService();
            List<result_search_job_zone> jobList = new List<result_search_job_zone>();

            try
            {
                jobList = dataService.SearchJobZoneList(param: param);
                jobList = buildDataForDisplay(entities: jobList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return jobList;
        }

        private static List<result_search_job_zone> buildDataForDisplay(List<result_search_job_zone> entities)
        {
            UtilityCommon utilityCommon = new UtilityCommon();
            entities = entities.Select(e =>
            {
                e.warehouse_name = !string.IsNullOrEmpty(e.warehouse_name) ? e.warehouse_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.location_name = !string.IsNullOrEmpty(e.location_name) ? e.location_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.zone_name = !string.IsNullOrEmpty(e.zone_name) ? e.zone_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.amount = e.amount >= 0 ? e.amount : 0;
                e.comment = !string.IsNullOrEmpty(e.comment) ? e.comment : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.id = utilityCommon.EncryptDataUrlEncoder(textData: e.zone_id.ToString(),
                                                                   encryptionkey: StaticKeys.DataEncrypteKey);
                return e;

            }).ToList();

            return entities;
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