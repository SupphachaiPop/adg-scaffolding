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

namespace adg_scaffolding.Backend.Store
{
    [System.Web.Script.Services.ScriptService]
    public partial class store_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCreateStore_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.StoreInfoUrl);
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

            param_search_store param = new param_search_store();
            DataTables<result_search_store> result = new DataTables<result_search_store>();

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

                List<result_search_store> StoreList = LoadData(param: param,
                                                      Order: OrderField,
                                                      OrderDir: OrderDir);

                if (StoreList.Count() > 0)
                {
                    TotalRecords = StoreList.FirstOrDefault().total_record;
                    result.draw = Convert.ToInt32(draw);
                    result.recordsTotal = TotalRecords;
                    result.recordsFiltered = TotalRecords;
                    result.data = StoreList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private static List<result_search_store> LoadData(param_search_store param,
                                                          String Order,
                                                          String OrderDir)
        {
            DataService dataService = new DataService();
            List<result_search_store> StoreList = new List<result_search_store>();

            try
            {
                StoreList = dataService.SearchStoreList(param: param);
                StoreList = buildDataForDisplay(entities: StoreList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return StoreList;
        }

        private static List<result_search_store> buildDataForDisplay(List<result_search_store> entities)
        {
            UtilityCommon utilityCommon = new UtilityCommon();
            entities = entities.Select(e =>
            {
                e.product_store_name = !string.IsNullOrEmpty(e.product_store_name) ? e.product_store_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.qty = e.qty >= 0 ? e.qty : 0;
                e.comment = !string.IsNullOrEmpty(e.comment) ? e.comment : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.id = utilityCommon.EncryptDataUrlEncoder(textData: e.store_id.ToString(),
                                                                    encryptionkey: StaticKeys.DataEncrypteKey);
                return e;

            }).ToList();

            return entities;
        }

        [WebMethod]
        public static bool UpdateStatus(string id, bool is_active)
        {
            DataService dataService = new DataService();
            store StoreEntity = new store();
            var user = UserLogin();

            StoreEntity.store_id = DecryptCode(id);
            StoreEntity.is_active = is_active;
            StoreEntity.modified_by = user.user_id;

            if (dataService.UpdateStatusStore(StoreEntity) > 0)
            {
                return true;
            }

            return false;
        }

        [WebMethod]
        public static bool DeleteData(string id)
        {
            DataService dataService = new DataService();
            store StoreEntity = new store();
            var Store = UserLogin();

            StoreEntity.store_id = DecryptCode(id);
            StoreEntity.modified_by = StoreEntity.store_id;
            var isReferred = dataService.GetStoreInfo(StoreEntity.store_id).is_referred;
            if (!isReferred.Value)
            {
                if (dataService.DeleteStore(StoreEntity) > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static result_user_login UserLogin()
        {
            result_user_login Store = new result_user_login();
            if ((HttpContext.Current.Session["userLoginBackend"] != null))
            {
                Store = (result_user_login)HttpContext.Current.Session["userLoginBackend"];
            }

            return Store;
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