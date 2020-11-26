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

namespace adg_scaffolding.Backend.Delivery
{
    [System.Web.Script.Services.ScriptService]
    public partial class delivery_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCreateDelivery_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.DeliveryInfoUrl);
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

            param_search_delivery param = new param_search_delivery();
            DataTables<result_search_delivery> result = new DataTables<result_search_delivery>();

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

                List<result_search_delivery> deliveryList = LoadData(param: param,
                                                      Order: OrderField,
                                                      OrderDir: OrderDir);

                if (deliveryList.Count() > 0)
                {
                    TotalRecords = deliveryList.FirstOrDefault().total_record;
                    result.draw = Convert.ToInt32(draw);
                    result.recordsTotal = TotalRecords;
                    result.recordsFiltered = TotalRecords;
                    result.data = deliveryList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private static List<result_search_delivery> LoadData(param_search_delivery param,
                                                          String Order,
                                                          String OrderDir)
        {
            DataService dataService = new DataService();
            List<result_search_delivery> deliveryList = new List<result_search_delivery>();

            try
            {
                deliveryList = dataService.SearchDeliveryList(param: param);
                deliveryList = buildDataForDisplay(entities: deliveryList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return deliveryList;
        }

        private static List<result_search_delivery> buildDataForDisplay(List<result_search_delivery> entities)
        {
            UtilityCommon utilityCommon = new UtilityCommon();
            entities = entities.Select(e =>
            {
                e.delivery_code = !string.IsNullOrEmpty(e.delivery_code) ? e.delivery_code : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.delivery_name = !string.IsNullOrEmpty(e.delivery_name) ? e.delivery_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.tax_no = !string.IsNullOrEmpty(e.tax_no) ? e.tax_no : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.phone = !string.IsNullOrEmpty(e.phone) ? e.phone : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.email = !string.IsNullOrEmpty(e.email) ? e.email : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.comment = !string.IsNullOrEmpty(e.comment) ? e.comment : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.id = utilityCommon.EncryptDataUrlEncoder(textData: e.delivery_id.ToString(),
                                                                    encryptionkey: StaticKeys.DataEncrypteKey);
                return e;

            }).ToList();

            return entities;
        }

        [WebMethod]
        public static bool UpdateStatus(string id, bool is_active)
        {
            DataService dataService = new DataService();
            delivery deliveryEntity = new delivery();
            var user = userLogin();

            deliveryEntity.delivery_id = DecryptCode(id);
            deliveryEntity.is_active = is_active;
            deliveryEntity.modified_by = user.user_id;

            if (dataService.UpdateStatusDelivery(deliveryEntity) > 0)
            {
                return true;
            }

            return false;
        }

        [WebMethod]
        public static bool DeleteData(string id)
        {
            DataService dataService = new DataService();
            delivery deliveryEntity = new delivery();
            var user = userLogin();

            deliveryEntity.delivery_id = DecryptCode(id);
            deliveryEntity.modified_by = user.user_id;
            var isReferred = dataService.GetDeliveryInfo(deliveryEntity.delivery_id).is_referred;
            if (!isReferred.Value)
            {
                if (dataService.DeleteDelivery(deliveryEntity) > 0)
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