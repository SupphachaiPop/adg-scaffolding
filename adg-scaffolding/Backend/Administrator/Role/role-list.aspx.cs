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
using Entity.Backend;

namespace adg_scaffolding.Backend.Administrator.Role
{
    public partial class role_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetDataToDropDownList();
            SetControlNotHeadOffice();
        }
        public void SetDataToDropDownList()
        {
            Dropdown dropdown = new Dropdown();
            ddlCompany = dropdown.GetDropdownCompany(ddlCompany);
        }

        public void SetControlNotHeadOffice()
        {
            DropdownCompany.Attributes.Add("style", "display:block;");
            var user = userLogin();
            if (!user.is_manage)
            {
                ddlCompany.SelectedValue = user.company_id.ToString();
                DropdownCompany.Attributes.Add("style", "display:none;");
            }
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
                                                bool? is_active,
                                                int? company_id)
        {

            paramRoleEntity param = new paramRoleEntity();
            DataTables<RoleEntity> result = new DataTables<RoleEntity>();

            try
            {
                UtilityCommon utilityCommon = new UtilityCommon();
                JQDT_Order firstOrder = order.FirstOrDefault();

                int StartRec = start;
                int TotalRecords = 0;
                string OrderField = firstOrder.column;
                string OrderDir = firstOrder.dir;

                param.search = txtSearch.Trim();
                param.company_id = company_id != 0 ? company_id : null;
                param.is_active = is_active.HasValue ? is_active : null;
                param.pageSize = length;
                param.pageNumber = (StartRec + param.pageSize) / param.pageSize;

                //check user Role type
                var user = userLogin();
                if (!user.is_manage)
                {
                    param.company_id = user.company_id;
                }

                List<RoleEntity> RoleList = LoadData(param: param,
                                                      Order: OrderField,
                                                      OrderDir: OrderDir);

                if (RoleList.Count() > 0)
                {
                    RoleList.ForEach(item =>
                    {
                        item.role_encrypt = utilityCommon.EncryptDataUrlEncoder(textData: item.role_id.ToString(),
                                                                    encryptionkey: StaticKeys.DataEncrypteKey);

                    });

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

        private static List<RoleEntity> LoadData(paramRoleEntity param,
                                                      String Order,
                                                      String OrderDir)
        {
            RoleService RoleService = new RoleService();
            List<RoleEntity> RoleEntities = new List<RoleEntity>();

            try
            {
                RoleEntities = RoleService.GetDataByCondition(param: param);
                RoleEntities = buildDataForDisplay(entities: RoleEntities);
            }
            catch (Exception ex)
            {
                throw;
            }

            return RoleEntities;
        }

        private static List<RoleEntity> buildDataForDisplay(List<RoleEntity> entities)
        {
            entities = entities.Select(e =>
            {
                e.role_code = !string.IsNullOrEmpty(e.role_code) ? e.role_code : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.role_name = !string.IsNullOrEmpty(e.role_name) ? e.role_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.comment = !string.IsNullOrEmpty(e.comment) ? e.comment : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                return e;

            }).ToList();

            return entities;
        }

        [WebMethod]
        public static string UpdateStatus(string id, bool is_active)
        {
            RoleService RoleService = new RoleService();
            RoleEntity RoleEntity = new RoleEntity();

            var user = userLogin();
            RoleEntity.role_id = DecryptCode(id);
            RoleEntity.is_active = is_active;
            RoleEntity.modified_by = user.user_id;
            if (RoleService.UpdateDataStatus(RoleEntity) > 0)
            {
                return "success";
            }

            return "failure";
        }

        [WebMethod]
        public static bool DeleteData(string id)
        {
            RoleService RoleService = new RoleService();
            RoleEntity RoleEntity = new RoleEntity();
            var user = userLogin();

            RoleEntity.role_id = DecryptCode(id);
            RoleEntity.modified_by = user.user_id; ;
            var isReferred = RoleService.CheckIsReferred(RoleEntity);
            if (!isReferred)
            {
                if (RoleService.DeleteData(RoleEntity) > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static UserEntity userLogin()
        {
            UserEntity user = new UserEntity();
            if ((HttpContext.Current.Session["userLoginBackend"] != null))
            {
                user = (UserEntity)HttpContext.Current.Session["userLoginBackend"];
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