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

namespace adg_scaffolding.Backend.Product_Management.Specification
{
    public partial class specification_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreatespecification_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.SpecificationInfoUrl);
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

            param_search_specification param = new param_search_specification();
            DataTables<result_search_specification> result = new DataTables<result_search_specification>();

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

                List<result_search_specification> specificationList = LoadData(param: param,
                                                      Order: OrderField,
                                                      OrderDir: OrderDir);

                if (specificationList.Count() > 0)
                {
                    TotalRecords = specificationList.Count();
                    result.draw = Convert.ToInt32(draw);
                    result.recordsTotal = TotalRecords;
                    result.recordsFiltered = TotalRecords;
                    result.data = specificationList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private static List<result_search_specification> LoadData(param_search_specification param,
                                                          String Order,
                                                          String OrderDir)
        {
            DataService dataService = new DataService();
            List<result_search_specification> specificationList = new List<result_search_specification>();

            try
            {
                specificationList = dataService.SearchSpecificationList(param: param);
                specificationList = buildDataForDisplay(specificationList: specificationList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return specificationList;
        }

        private static List<result_search_specification> buildDataForDisplay(List<result_search_specification> specificationList)
        {
            UtilityCommon utilityCommon = new UtilityCommon();
            specificationList = specificationList.Select(e =>
            {
                e.specification_code = !string.IsNullOrEmpty(e.specification_code) ? e.specification_code : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.specification_name = !string.IsNullOrEmpty(e.specification_name) ? e.specification_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.comment = !string.IsNullOrEmpty(e.comment) ? e.comment : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.id = utilityCommon.EncryptDataUrlEncoder(textData: e.specification_id.ToString(),
                                                                   encryptionkey: StaticKeys.DataEncrypteKey);
                return e;

            }).ToList();

            return specificationList;
        }

        [WebMethod]
        public static string UpdateStatus(string id, bool is_active)
        {
            DataService dataService = new DataService();
            specification specification = new specification();

            var user = userLogin();
            specification.specification_id = DecryptCode(id);
            specification.is_active = is_active;
            specification.modified_by = user.user_id;
            if (dataService.UpdateStatusSpecification(specification) > 0)
            {
                return "success";
            }

            return "failure";
        }

        [WebMethod]
        public static bool DeleteData(string id)
        {
            DataService dataService = new DataService();
            specification specification = new specification();
            var user = userLogin();

            specification.specification_id = DecryptCode(id);
            specification.modified_by = user.user_id;

            var isReferred = dataService.GetSpecificationInfo(specification.specification_id).is_referred;
            if (!isReferred.Value)
            {
                if (dataService.DeleteSpecification(specification) > 0)
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
