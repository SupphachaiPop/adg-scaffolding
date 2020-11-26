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

namespace adg_scaffolding.Backend.Administrator.Role
{
    [System.Web.Script.Services.ScriptService]
    public partial class role_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreateRole_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.RoleInfoUrl);
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

            param_search_role param = new param_search_role();
            DataTables<result_search_role> result = new DataTables<result_search_role>();

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

                List<result_search_role> RoleList = LoadData(param: param,
                                                      Order: OrderField,
                                                      OrderDir: OrderDir);

                if (RoleList.Count() > 0)
                {
                    TotalRecords = RoleList.Count();
                    result.draw = Convert.ToInt32(draw);
                    result.recordsTotal = TotalRecords;
                    result.recordsFiltered = TotalRecords;
                    result.data = RoleList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private static List<result_search_role> LoadData(param_search_role param,
                                                          String Order,
                                                          String OrderDir)
        {
            DataService dataService = new DataService();
            List<result_search_role> roleList = new List<result_search_role>();

            try
            {
                roleList = dataService.SearchRoleList(param: param);
                roleList = buildDataForDisplay(roleList: roleList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return roleList;
        }

        private static List<result_search_role> buildDataForDisplay(List<result_search_role> roleList)
        {
            UtilityCommon utilityCommon = new UtilityCommon();
            roleList = roleList.Select(e =>
            {
                e.role_code = !string.IsNullOrEmpty(e.role_code) ? e.role_code : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.role_name = !string.IsNullOrEmpty(e.role_name) ? e.role_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.comment = !string.IsNullOrEmpty(e.comment) ? e.comment : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.id = utilityCommon.EncryptDataUrlEncoder(textData: e.role_id.ToString(),
                                                                   encryptionkey: StaticKeys.DataEncrypteKey);
                return e;

            }).ToList();

            return roleList;
        }

        [WebMethod]
        public static string UpdateStatus(string id, bool is_active)
        {
            DataService dataService = new DataService();
            role role = new role();

            var user = userLogin();
            role.role_id = DecryptCode(id);
            role.is_active = is_active;
            role.modified_by = user.user_id;
            if (dataService.UpdateStatusRole(role) > 0)
            {
                return "success";
            }

            return "failure";
        }

        [WebMethod]
        public static bool DeleteData(string id)
        {
            DataService dataService = new DataService();
            role role = new role();
            var user = userLogin();

            role.role_id = DecryptCode(id);
            role.modified_by = user.user_id;

            var isReferred = dataService.GetRoleInfo(role.role_id).is_referred;
            if (!isReferred.Value)
            {
                if (dataService.DeleteRole(role) > 0)
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