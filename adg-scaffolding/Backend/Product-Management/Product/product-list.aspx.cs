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

namespace adg_scaffolding.Backend.Product_Management.Product
{
    public partial class product_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreateProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.ProductInfoUrl);
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

            param_search_product param = new param_search_product();
            DataTables<result_search_product> result = new DataTables<result_search_product>();

            try
            {
                JQDT_Order firstOrder = order.FirstOrDefault();
                int StartRec = start;
                int TotalRecords = 0;
                string OrderField = firstOrder.column;
                string OrderDir = firstOrder.dir;

                param.search = txtSearch.Trim();
                param.is_active = is_active.HasValue ? is_active : null;
                param.pageSize = length;
                param.pageNumber = (StartRec + param.pageSize) / param.pageSize;

                List<result_search_product> productList = LoadData(param: param,
                                                      Order: OrderField,
                                                      OrderDir: OrderDir);

                if (productList.Count() > 0)
                {
                    TotalRecords = productList.Count();
                    result.draw = Convert.ToInt32(draw);
                    result.recordsTotal = TotalRecords;
                    result.recordsFiltered = TotalRecords;
                    result.data = productList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private static List<result_search_product> LoadData(param_search_product param,
                                                          String Order,
                                                          String OrderDir)
        {
            DataService dataService = new DataService();
            List<result_search_product> productList = new List<result_search_product>();

            try
            {
                productList = dataService.SearchProductList(param: param);
                productList = buildDataForDisplay(productList: productList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return productList;
        }

        private static List<result_search_product> buildDataForDisplay(List<result_search_product> productList)
        {
            UtilityCommon utilityCommon = new UtilityCommon();
            productList = productList.Select(e =>
            {
                e.product_code = !string.IsNullOrEmpty(e.product_code) ? e.product_code : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.product_name = !string.IsNullOrEmpty(e.product_name) ? e.product_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.comment = !string.IsNullOrEmpty(e.comment) ? e.comment : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.id = utilityCommon.EncryptDataUrlEncoder(textData: e.product_id.ToString(),
                                                                   encryptionkey: StaticKeys.DataEncrypteKey);
                return e;

            }).ToList();

            return productList;
        }

        [WebMethod]
        public static string UpdateStatus(string id, bool is_active)
        {
            DataService dataService = new DataService();
            product product = new product();

            var user = userLogin();
            product.product_id = DecryptCode(id);
            product.is_active = is_active;
            product.modified_by = user.user_id;
            if (dataService.UpdateStatusProduct(product) > 0)
            {
                return "success";
            }

            return "failure";
        }

        [WebMethod]
        public static bool DeleteData(string id)
        {
            DataService dataService = new DataService();
            product product = new product();
            var user = userLogin();

            product.product_id = DecryptCode(id);
            product.modified_by = user.user_id;

            var isReferred = dataService.GetProductInfo(product.product_id).is_referred;
            if (!isReferred.Value)
            {
                if (dataService.DeleteProduct(product) > 0)
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
