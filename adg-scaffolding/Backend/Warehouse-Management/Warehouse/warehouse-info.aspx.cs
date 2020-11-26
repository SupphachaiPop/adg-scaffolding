using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service.Backend;
using Definitions;
using Entity;
using ClosedXML;

namespace adg_scaffolding.Backend.Warehouse_Management.Warehouse
{
    public partial class warehouse_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var warehouseId = GetIdFromQueryString();
                setDataToUIByID(warehouseId);
            }
        }

        public void setDataToUIByID(Int32 ID)
        {
            DataService dataService = new DataService();
            result_info_warehouse warehouse = new result_info_warehouse();
            UtilityCommon utilityCommon = new UtilityCommon();
            chkStatus.Checked = true;
            if (ID > 0)
            {
                warehouse = dataService.GetWarehouseInfo(ID);
                if (warehouse != null)
                {
                    txtWarehouseCode.Text = warehouse.warehouse_code;
                    txtWarehouseName.Text = warehouse.warehouse_name;
                    txtPhone.Text = warehouse.phone;
                    txtAddress.Text = warehouse.address;
                    txtComment.Text = warehouse.comment;
                    chkStatus.Checked = ID != 0 ? warehouse.is_active.Value : true;
                }
            }
        }
        protected void lbnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            if (!ValidateForm(out message))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "openModalWaring('" + message + "');", true);
                return;
            }

            DataService dataService = new DataService();
            param_create_warehouse param = new param_create_warehouse();
            UtilityCommon utilityCommon = new UtilityCommon();
            DateTime _now = DateTime.Now;
            var user = userLogin();
            var warehouseId = GetIdFromQueryString();

            param.warehouse_id = warehouseId;
            param.warehouse_code = txtWarehouseCode.Text;
            param.warehouse_name = txtWarehouseName.Text;
            param.phone = txtPhone.Text;
            param.address = txtAddress.Text;
            param.comment = txtComment.Text;
            param.is_referred = true;
            param.is_active = chkStatus.Checked;
            param.is_deleted = false;
            param.created_by = user.user_id;
            param.created_date = _now;
            param.modified_by = user.user_id;
            param.modified_date = _now;

            int success = 0;
            if (warehouseId > 0)
            {
                success = dataService.UpdateWarehouse(param);
            }
            else
            {
                success = dataService.InsertWarehouse(param);
            }

            if (success > 0)
            {
                message = "บันทึกข้อมูลสำเร็จ";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "openModalSuccess('" + message + "');", true);
            }
            else
            {
                message = "บันทึกข้อมูลไม่สำเร็จ";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "openModalError('" + message + "');", true);
                return;
            }
        }
        public bool ValidateForm(out string message)
        {
            DataService dataService = new DataService();
            List<warehouse> warehouseList = dataService.GetWarehouseList();
            message = "";

            if (string.IsNullOrEmpty(txtWarehouseCode.Text.Trim()))
            {
                message = "กรุณากรอก รหัสคลังสินค้า (Warehouse code)";
                return false;
            }

            if (string.IsNullOrEmpty(txtWarehouseName.Text.Trim()))
            {
                message = "กรุณากรอก ชื่อคลังสินค้า (Warehouse name)";
                return false;
            }

            if (warehouseList != null && warehouseList.Count > 0)
            {
                int warehouseId = GetIdFromQueryString();
                warehouseList = warehouseList.Where(i => i.warehouse_id != warehouseId).ToList();
                if (warehouseList.Any(i => i.warehouse_code.Trim().Equals(txtWarehouseCode.Text.Trim())))
                {
                    message = "รหัสคลังสินค้า (Warehouse code) นี้มีอยู่ในระบบแล้ว";
                    return false;
                }

                if (warehouseList.Any(i => i.warehouse_name.Trim().Equals(txtWarehouseName.Text.Trim())))
                {
                    message = "ชื่อคลังสินค้า (Warehouse name) นี้มีอยู่ในระบบแล้ว";
                    return false;
                }
            }

            return true;
        }
        protected void lbnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.WarehouseListUrl, false);
        }
        protected void lblSuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.WarehouseListUrl, false);
        }
        public int DecryptCode(string enCryptCode)
        {
            UtilityCommon utilityCommon = new UtilityCommon();

            var id = utilityCommon.DecryptDataUrlEncoder<int>(encryptedText: enCryptCode,
                                                              encryptionkey: StaticKeys.DataEncrypteKey);

            return id;
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

        public int GetIdFromQueryString()
        {
            return Request.QueryString["ID"] != null ? DecryptCode(Request.QueryString["ID"]) : 0;
        }
    }
}