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

namespace adg_scaffolding.Backend.Store.Product_Store
{
    [System.Web.Script.Services.ScriptService]
    public partial class product_store_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCreateProductStore_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.ProductStoreInfoUrl);
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

            param_search_product_store param = new param_search_product_store();
            DataTables<result_search_product_store> result = new DataTables<result_search_product_store>();

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

                List<result_search_product_store> ProductStoreList = LoadData(param: param,
                                                      Order: OrderField,
                                                      OrderDir: OrderDir);

                if (ProductStoreList.Count() > 0)
                {
                    TotalRecords = ProductStoreList.FirstOrDefault().total_record;
                    result.draw = Convert.ToInt32(draw);
                    result.recordsTotal = TotalRecords;
                    result.recordsFiltered = TotalRecords;
                    result.data = ProductStoreList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private static List<result_search_product_store> LoadData(param_search_product_store param,
                                                          String Order,
                                                          String OrderDir)
        {
            DataService dataService = new DataService();
            List<result_search_product_store> ProductStoreList = new List<result_search_product_store>();

            try
            {
                ProductStoreList = dataService.SearchProductStoreList(param: param);
                ProductStoreList = buildDataForDisplay(entities: ProductStoreList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return ProductStoreList;
        }

        private static List<result_search_product_store> buildDataForDisplay(List<result_search_product_store> entities)
        {
            UtilityCommon utilityCommon = new UtilityCommon();
            entities = entities.Select(e =>
            {
                e.product_store_code = !string.IsNullOrEmpty(e.product_store_code) ? e.product_store_code : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.product_store_name = !string.IsNullOrEmpty(e.product_store_name) ? e.product_store_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.comment = !string.IsNullOrEmpty(e.comment) ? e.comment : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.id = utilityCommon.EncryptDataUrlEncoder(textData: e.product_store_id.ToString(),
                                                                    encryptionkey: StaticKeys.DataEncrypteKey);
                return e;

            }).ToList();

            return entities;
        }

        [WebMethod]
        public static bool UpdateStatus(string id, bool is_active)
        {
            DataService dataService = new DataService();
            product_store ProductStoreEntity = new product_store();
            var user = UserLogin();

            ProductStoreEntity.product_store_id = DecryptCode(id);
            ProductStoreEntity.is_active = is_active;
            ProductStoreEntity.modified_by = user.user_id;

            if (dataService.UpdateStatusProductStore(ProductStoreEntity) > 0)
            {
                return true;
            }

            return false;
        }

        [WebMethod]
        public static bool DeleteData(string id)
        {
            DataService dataService = new DataService();
            product_store productStoreEntity = new product_store();
            var ProductStore = UserLogin();

            productStoreEntity.product_store_id = DecryptCode(id);
            productStoreEntity.modified_by = productStoreEntity.product_store_id;
            var isReferred = dataService.GetProductStoreInfo(productStoreEntity.product_store_id).is_referred;
            if (!isReferred.Value)
            {
                if (dataService.DeleteProductStore(productStoreEntity) > 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static result_user_login UserLogin()
        {
            result_user_login ProductStore = new result_user_login();
            if ((HttpContext.Current.Session["userLoginBackend"] != null))
            {
                ProductStore = (result_user_login)HttpContext.Current.Session["userLoginBackend"];
            }

            return ProductStore;
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