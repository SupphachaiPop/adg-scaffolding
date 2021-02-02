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

namespace adg_scaffolding.Backend.Store.Store_Curculation
{
    [System.Web.Script.Services.ScriptService]
    public partial class store_curculation_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCreateStoreCurculation_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.StoreCurculationInfoUrl);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static object GetData(int draw,
                                    int start,
                                    int length,
                                    List<JQDT_Order> order,
                                    string txtSearch,
                                    bool? is_active)
        {

            param_search_store_curculation param = new param_search_store_curculation();
            DataTables<result_search_store_curculation> result = new DataTables<result_search_store_curculation>();

            try
            {

                JQDT_Order firstOrder = order.FirstOrDefault();
                int TotalRecords = 0;
                string OrderField = firstOrder.column;
                string OrderDir = firstOrder.dir;

                param.search = txtSearch.Trim();
                param.is_active = is_active.HasValue ? is_active : null;
                param.pageSize = length;
                param.pageNumber = (start + length) / length;

                List<result_search_store_curculation> StoreCurculationList = LoadData(param: param,
                                                      Order: OrderField,
                                                      OrderDir: OrderDir);

                if (StoreCurculationList.Count() > 0)
                {
                    TotalRecords = StoreCurculationList.FirstOrDefault().total_record;
                    result.draw = Convert.ToInt32(draw);
                    result.recordsTotal = TotalRecords;
                    result.recordsFiltered = TotalRecords;
                    result.data = StoreCurculationList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private static List<result_search_store_curculation> LoadData(param_search_store_curculation param,
                                                          String Order,
                                                          String OrderDir)
        {
            DataService dataService = new DataService();
            List<result_search_store_curculation> StoreCurculationList = new List<result_search_store_curculation>();

            try
            {
                StoreCurculationList = dataService.SearchStoreCurculationList(param: param);
                StoreCurculationList = buildDataForDisplay(entities: StoreCurculationList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StoreCurculationList;
        }

        private static List<result_search_store_curculation> buildDataForDisplay(List<result_search_store_curculation> entities)
        {
            UtilityCommon utilityCommon = new UtilityCommon();
            entities = entities.Select(e =>
            {
                e.curculation_no = !string.IsNullOrEmpty(e.curculation_no) ? e.curculation_no : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.loaner_name = !string.IsNullOrEmpty(e.loaner_name) ? e.loaner_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.return_name = !string.IsNullOrEmpty(e.return_name) ? e.return_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.curculation_status = !string.IsNullOrEmpty(e.curculation_no) ? e.curculation_status : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.comment = !string.IsNullOrEmpty(e.comment) ? e.comment : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.id = utilityCommon.EncryptDataUrlEncoder(textData: e.store_curculation_id.ToString(),
                                                                    encryptionkey: StaticKeys.DataEncrypteKey);
                return e;

            }).ToList();

            return entities;
        }

        public static result_user_login UserLogin()
        {
            result_user_login StoreCurculation = new result_user_login();
            if ((HttpContext.Current.Session["userLoginBackend"] != null))
            {
                StoreCurculation = (result_user_login)HttpContext.Current.Session["userLoginBackend"];
            }

            return StoreCurculation;
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