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

namespace adg_scaffolding.Backend.Administrator.User
{
    [System.Web.Script.Services.ScriptService]
    public partial class user_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.UserInfoUrl);
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

            param_search_user param = new param_search_user();
            DataTables<result_search_user> result = new DataTables<result_search_user>();

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

                List<result_search_user> userList = LoadData(param: param,
                                                      Order: OrderField,
                                                      OrderDir: OrderDir);

                if (userList.Count() > 0)
                {
                    TotalRecords = userList.FirstOrDefault().total_record;
                    result.draw = Convert.ToInt32(draw);
                    result.recordsTotal = TotalRecords;
                    result.recordsFiltered = TotalRecords;
                    result.data = userList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private static List<result_search_user> LoadData(param_search_user param,
                                                          String Order,
                                                          String OrderDir)
        {
            DataService dataService = new DataService();
            List<result_search_user> userList = new List<result_search_user>();

            try
            {
                userList = dataService.SearchUserList(param: param);
                userList = buildDataForDisplay(entities: userList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return userList;
        }

        private static List<result_search_user> buildDataForDisplay(List<result_search_user> entities)
        {
            UtilityCommon utilityCommon = new UtilityCommon();
            entities = entities.Select(e =>
            {
                e.username = !string.IsNullOrEmpty(e.username) ? e.username : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.full_name = !string.IsNullOrEmpty(e.full_name) ? e.full_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.role_name = !string.IsNullOrEmpty(e.role_name) ? e.role_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.phone = !string.IsNullOrEmpty(e.phone) ? e.phone : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.email = !string.IsNullOrEmpty(e.email) ? e.email : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.id = utilityCommon.EncryptDataUrlEncoder(textData: e.user_id.ToString(),
                                                                    encryptionkey: StaticKeys.DataEncrypteKey);
                return e;

            }).ToList();

            return entities;
        }

        [WebMethod]
        public static bool UpdateStatus(string id, bool is_active)
        {
            DataService dataService = new DataService();
            user userEntity = new user();
            var user = userLogin();

            userEntity.user_id = DecryptCode(id);
            userEntity.is_active = is_active;
            userEntity.modified_by = user.user_id;

            if (dataService.UpdateStatusUser(userEntity) > 0)
            {
                return true;
            }

            return false;
        }

        [WebMethod]
        public static bool DeleteData(string id)
        {
            DataService dataService = new DataService();
            user userEntity = new user();
            var user = userLogin();

            userEntity.user_id = DecryptCode(id);
            userEntity.modified_by = user.user_id;
            var isReferred = dataService.GetUserInfo(userEntity.user_id).is_referred;
            if (!isReferred.Value)
            {
                if (dataService.DeleteUser(userEntity) > 0)
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