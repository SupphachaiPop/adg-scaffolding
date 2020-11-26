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

namespace adg_scaffolding.Backend.Customer
{
    [System.Web.Script.Services.ScriptService]
    public partial class Customer_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCreateCustomer_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.CustomerInfoUrl);
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

            param_search_customer param = new param_search_customer();
            DataTables<result_search_customer> result = new DataTables<result_search_customer>();

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

                List<result_search_customer> customerList = LoadData(param: param,
                                                      Order: OrderField,
                                                      OrderDir: OrderDir);

                if (customerList.Count() > 0)
                {
                    TotalRecords = customerList.FirstOrDefault().total_record;
                    result.draw = Convert.ToInt32(draw);
                    result.recordsTotal = TotalRecords;
                    result.recordsFiltered = TotalRecords;
                    result.data = customerList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private static List<result_search_customer> LoadData(param_search_customer param,
                                                          String Order,
                                                          String OrderDir)
        {
            DataService dataService = new DataService();
            List<result_search_customer> customerList = new List<result_search_customer>();

            try
            {
                customerList = dataService.SearchCustomerList(param: param);
                customerList = buildDataForDisplay(entities: customerList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return customerList;
        }

        private static List<result_search_customer> buildDataForDisplay(List<result_search_customer> entities)
        {
            UtilityCommon utilityCommon = new UtilityCommon();
            entities = entities.Select(e =>
            {
                e.customer_code = !string.IsNullOrEmpty(e.customer_code) ? e.customer_code : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.customer_name = !string.IsNullOrEmpty(e.customer_name) ? e.customer_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.tax_no = !string.IsNullOrEmpty(e.tax_no) ? e.tax_no : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.phone = !string.IsNullOrEmpty(e.phone) ? e.phone : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.email = !string.IsNullOrEmpty(e.email) ? e.email : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.comment = !string.IsNullOrEmpty(e.comment) ? e.comment : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.id = utilityCommon.EncryptDataUrlEncoder(textData: e.customer_id.ToString(),
                                                                    encryptionkey: StaticKeys.DataEncrypteKey);
                return e;

            }).ToList();

            return entities;
        }

        [WebMethod]
        public static bool UpdateStatus(string id, bool is_active)
        {
            DataService dataService = new DataService();
            customer customerEntity = new customer();
            var user = userLogin();

            customerEntity.customer_id = DecryptCode(id);
            customerEntity.is_active = is_active;
            customerEntity.modified_by = user.user_id;

            if (dataService.UpdateStatusCustomer(customerEntity) > 0)
            {
                return true;
            }

            return false;
        }

        [WebMethod]
        public static bool DeleteData(string id)
        {
            DataService dataService = new DataService();
            customer customerEntity = new customer();
            var user = userLogin();

            customerEntity.customer_id = DecryptCode(id);
            customerEntity.modified_by = user.user_id;
            var isReferred = dataService.GetCustomerInfo(customerEntity.customer_id).is_referred;
            if (!isReferred.Value)
            {
                if (dataService.DeleteCustomer(customerEntity) > 0)
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