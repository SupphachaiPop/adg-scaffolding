using Entity.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Definitions.Static_Text;
using Definitions;
using Service.Backend;
using Entity;

namespace adg_scaffolding.Backend.Administrator.Branch
{
    [System.Web.Script.Services.ScriptService]
    public partial class swBranch_list : System.Web.UI.Page
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
                DropdownCompany.Attributes.Add("style", "display:none;");
            }
        }

        protected void btnCreateBranch_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.BranchInfoUrl);
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

            paramSwBranchEntity param = new paramSwBranchEntity();
            DataTables<swBranchEntity> result = new DataTables<swBranchEntity>();

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

                //check user company type
                var user = userLogin();
                if (!user.is_manage)
                {
                    param.company_id = user.company_id;
                }

                List<swBranchEntity> swBranchList = LoadData(param: param,
                                                      Order: OrderField,
                                                      OrderDir: OrderDir);
 
                if (swBranchList.Count() > 0)
                {
                    swBranchList.ForEach(item =>
                    {
                        item.branch_encrypt = utilityCommon.EncryptDataUrlEncoder(textData: item.branch_id.ToString(),
                                                                    encryptionkey: StaticKeys.DataEncrypteKey);
                    });

                    TotalRecords = swBranchList.FirstOrDefault().total_record;
                    result.draw = Convert.ToInt32(draw);
                    result.recordsTotal = TotalRecords;
                    result.recordsFiltered = TotalRecords;
                    result.data = swBranchList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private static List<swBranchEntity> LoadData(paramSwBranchEntity param,
                                                      String Order,
                                                      String OrderDir)
        {
            swBranchService swBranchService = new swBranchService();
            List<swBranchEntity> swBranchEntities = new List<swBranchEntity>();

            try
            {
                swBranchEntities = swBranchService.GetDataByCondition(param: param);
                swBranchEntities = buildDataForDisplay(entities: swBranchEntities);
            }
            catch (Exception ex)
            {
                throw;
            }

            return swBranchEntities;
        }

        private static List<swBranchEntity> buildDataForDisplay(List<swBranchEntity> entities)
        {
            entities = entities.Select(e =>
            {
                e.branch_code = !string.IsNullOrEmpty(e.branch_code) ? e.branch_code : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.branch_name = !string.IsNullOrEmpty(e.branch_name) ? e.branch_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.company_name = !string.IsNullOrEmpty(e.company_name) ? e.company_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.contact_name = !string.IsNullOrEmpty(e.contact_name) ? e.contact_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.phone = !string.IsNullOrEmpty(e.phone) ? e.phone : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                return e;

            }).ToList();

            return entities;
        }

        [WebMethod]
        public static bool UpdateStatus(string id, bool is_active)
        {
            swBranchService swBranchService = new swBranchService();
            swBranchEntity swBranchEntity = new swBranchEntity();
            var user = userLogin();

            swBranchEntity.branch_id = DecryptCode(id);
            swBranchEntity.is_active = is_active;
            swBranchEntity.modified_by = user.user_id;

            if (swBranchService.UpdateDataStatus(swBranchEntity) > 0)
            {
                return true;
            }

            return false;
        }

        [WebMethod]
        public static bool DeleteData(string id)
        {
            swBranchService swBranchService = new swBranchService();
            swBranchEntity swBranchEntity = new swBranchEntity();
            var user = userLogin();

            swBranchEntity.branch_id = DecryptCode(id);
            swBranchEntity.modified_by = user.user_id;

            if (swBranchService.DeleteData(swBranchEntity) > 0)
            {
                return true;
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