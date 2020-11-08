using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Entity.Backend;
using System.Web.UI.WebControls;
using Definitions.Static_Text;
using Definitions;
using Entity;
using Service.Backend;

namespace adg_scaffolding.Backend.Administrator.Company
{
    [System.Web.Script.Services.ScriptService]
    public partial class company_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetControlNotHeadOffice();
        }

        public void SetControlNotHeadOffice()
        {
            btnCreateCompany.Visible = true;
            var user = userLogin();
            if (!user.is_manage)
            {
                btnCreateCompany.Visible = false;
            }
        }
        protected void btnCreateCompany_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.CompanyInfoUrl);
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

            paramCompanyEntity param = new paramCompanyEntity();
            DataTables<CompanyEntity> result = new DataTables<CompanyEntity>();

            try
            {
                UtilityCommon utilityCommon = new UtilityCommon();
                JQDT_Order firstOrder = order.FirstOrDefault();

                int StartRec = start;
                int TotalRecords = 0;
                string OrderField = firstOrder.column;
                string OrderDir = firstOrder.dir;

                param.search = txtSearch.Trim();

                param.is_active = is_active.HasValue ? is_active : null;
                param.pageSize = length;
                param.pageNumber = (StartRec + param.pageSize) / param.pageSize;

                //check user company type
                var userManage = true;
                var user = userLogin();
                if (!user.is_manage)
                {
                    param.company_id = user.company_id;
                    userManage = false;
                }

                List<CompanyEntity> companyList = LoadData(param: param,
                                                      Order: OrderField,
                                                      OrderDir: OrderDir);

                if (companyList.Count() > 0)
                {
                    companyList.ForEach(item =>
                    {

                        item.company_encrypt = utilityCommon.EncryptDataUrlEncoder(textData: item.company_id.ToString(),
                                                                    encryptionkey: StaticKeys.DataEncrypteKey);

                        item.user_manage = userManage;
                    });

                    TotalRecords = companyList.Count();
                    result.draw = Convert.ToInt32(draw);
                    result.recordsTotal = TotalRecords;
                    result.recordsFiltered = TotalRecords;
                    result.data = companyList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private static List<CompanyEntity> LoadData(paramCompanyEntity param,
                                                      String Order,
                                                      String OrderDir)
        {
            CompanyService companyService = new CompanyService();
            List<CompanyEntity> companyEntities = new List<CompanyEntity>();

            try
            {
                companyEntities = companyService.GetDataByCondition(param: param);
                companyEntities = buildDataForDisplay(entities: companyEntities);
            }
            catch (Exception ex)
            {
                throw;
            }

            return companyEntities;
        }

        private static List<CompanyEntity> buildDataForDisplay(List<CompanyEntity> entities)
        {
            entities = entities.Select(e =>
            {
                e.company_code = !string.IsNullOrEmpty(e.company_code) ? e.company_code : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.company_name = !string.IsNullOrEmpty(e.company_name) ? e.company_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.company_type_name = !string.IsNullOrEmpty(e.company_type_name) ? e.company_type_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.contact_name = !string.IsNullOrEmpty(e.contact_name) ? e.contact_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.phone = !string.IsNullOrEmpty(e.phone) ? e.phone : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                return e;

            }).ToList();

            return entities;
        }

        [WebMethod]
        public static string UpdateStatus(string id, bool is_active)
        {
            CompanyService companyService = new CompanyService();
            CompanyEntity companyEntity = new CompanyEntity();

            var user = userLogin();
            companyEntity.company_id = DecryptCode(id);
            companyEntity.is_active = is_active;
            companyEntity.modified_by = user.user_id;
            if (companyService.UpdateDataStatus(companyEntity) > 0)
            {
                return "success";
            }

            return "failure";
        }

        [WebMethod]
        public static bool DeleteData(string id)
        {
            CompanyService companyService = new CompanyService();
            CompanyEntity companyEntity = new CompanyEntity();
            var user = userLogin();

            companyEntity.company_id = DecryptCode(id);
            companyEntity.modified_by = user.user_id; ;
            var isReferred = companyService.CheckIsReferred(companyEntity);
            if (!isReferred)
            {
                if (companyService.DeleteData(companyEntity) > 0)
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