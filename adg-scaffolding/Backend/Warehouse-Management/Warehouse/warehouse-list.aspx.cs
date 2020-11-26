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

namespace adg_scaffolding.Backend.Warehouse_Management.Warehouse
{
    [System.Web.Script.Services.ScriptService]
    public partial class warehouse_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCreateWarehouse_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.WarehouseInfoUrl);
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

            param_search_warehouse param = new param_search_warehouse();
            DataTables<result_search_warehouse> result = new DataTables<result_search_warehouse>();

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

                List<result_search_warehouse> warehouseList = LoadData(param: param,
                                                      Order: OrderField,
                                                      OrderDir: OrderDir);

                if (warehouseList.Count() > 0)
                {
                    TotalRecords = warehouseList.FirstOrDefault().total_record;
                    result.draw = Convert.ToInt32(draw);
                    result.recordsTotal = TotalRecords;
                    result.recordsFiltered = TotalRecords;
                    result.data = warehouseList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private static List<result_search_warehouse> LoadData(param_search_warehouse param,
                                                          String Order,
                                                          String OrderDir)
        {
            DataService dataService = new DataService();
            List<result_search_warehouse> warehouseList = new List<result_search_warehouse>();

            try
            {
                warehouseList = dataService.SearchWarehouseList(param: param);
                warehouseList = buildDataForDisplay(entities: warehouseList);
            }
            catch (Exception ex)
            {
                throw;
            }

            return warehouseList;
        }

        private static List<result_search_warehouse> buildDataForDisplay(List<result_search_warehouse> entities)
        {
            UtilityCommon utilityCommon = new UtilityCommon();
            entities = entities.Select(e =>
            {
                e.warehouse_code = !string.IsNullOrEmpty(e.warehouse_code) ? e.warehouse_code : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.warehouse_name = !string.IsNullOrEmpty(e.warehouse_name) ? e.warehouse_name : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.phone = !string.IsNullOrEmpty(e.phone) ? e.phone : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.comment = !string.IsNullOrEmpty(e.comment) ? e.comment : Static_Text.DEFAULT_VALUE.DEFAULT_REPLACE_STRING_EMPTY;
                e.id = utilityCommon.EncryptDataUrlEncoder(textData: e.warehouse_id.ToString(),
                                                                    encryptionkey: StaticKeys.DataEncrypteKey);
                return e;

            }).ToList();

            return entities;
        }

        [WebMethod]
        public static bool UpdateStatus(string id, bool is_active)
        {
            DataService dataService = new DataService();
            warehouse warehouseEntity = new warehouse();
            var user = userLogin();

            warehouseEntity.warehouse_id = DecryptCode(id);
            warehouseEntity.is_active = is_active;
            warehouseEntity.modified_by = user.role_id;

            if (dataService.UpdateStatusWarehouse(warehouseEntity) > 0)
            {
                return true;
            }

            return false;
        }

        [WebMethod]
        public static bool DeleteData(string id)
        {
            DataService dataService = new DataService();
            warehouse warehouseEntity = new warehouse();
            var user = userLogin();

            warehouseEntity.warehouse_id = DecryptCode(id);
            warehouseEntity.modified_by = user.user_id;
            var isReferred = dataService.GetWarehouseInfo(warehouseEntity.warehouse_id).is_referred;
            if (!isReferred.Value)
            {
                if (dataService.DeleteWarehouse(warehouseEntity) > 0)
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