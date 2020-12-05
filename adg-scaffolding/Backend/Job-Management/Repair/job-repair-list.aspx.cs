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

namespace adg_scaffolding.Backend.Job_Management.Repair
{
    [System.Web.Script.Services.ScriptService]
    public partial class job_repair_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static object GetData(int draw,
                                    int start,
                                    int length,
                                    List<JQDT_Order> order,
                                    string txtSearch)
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

                List<result_search_job_zone> JobRepairList = LoadData(param: param,
                                                      Order: OrderField,
                                                      OrderDir: OrderDir);

                if (JobRepairList.Count() > 0)
                {
                    TotalRecords = JobRepairList.FirstOrDefault().total_record;
                    result.draw = Convert.ToInt32(draw);
                    result.recordsTotal = TotalRecords;
                    result.recordsFiltered = TotalRecords;
                    result.data = JobRepairList;
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
            List<result_search_job_zone> JobRepairList = new List<result_search_job_zone>();

            try
            {
                param.location_id = (int)_BaseConst.location.Repair;
                param.status_id = (int)_BaseConst.status_job.repair_processing;
                JobRepairList = dataService.SearchJobZoneList(param: param);
                JobRepairList = buildDataForDisplay(entities: JobRepairList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return JobRepairList;
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