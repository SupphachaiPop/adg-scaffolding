﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service.Backend;
using Definitions;
using Entity;
using ClosedXML;

namespace adg_scaffolding.Backend.Store.Product_Store
{
    public partial class product_store_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var productStoreId = GetIdFromQueryString();
                setDataToUIByID(productStoreId);
            }
        }

        public void setDataToUIByID(Int32 ID)
        {
            DataService dataService = new DataService();
            result_info_product_store productStore = new result_info_product_store();
            UtilityCommon utilityCommon = new UtilityCommon();
            chkStatus.Checked = true;
            if (ID > 0)
            {
                productStore = dataService.GetProductStoreInfo(ID);
                if (productStore != null)
                {
                    lblProductStoreId.Text = ID.ToString();
                    txtProductStoreName.Text = productStore.product_store_name;
                    txtProductStoreCode.Text = productStore.product_store_code;
                    txtComment.Text = productStore.comment;
                    chkStatus.Checked = ID != 0 ? productStore.is_active.Value : true;
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
            param_create_product_store param = new param_create_product_store();
            UtilityCommon utilityCommon = new UtilityCommon();
            DateTime _now = DateTime.Now;
            var user = userLogin();
            var productStoreId = GetIdFromQueryString();
            param.product_store_id = productStoreId;
            param.product_store_name = txtProductStoreName.Text;
            param.product_store_code = txtProductStoreCode.Text;
            param.comment = txtComment.Text;
            param.is_referred = true;
            param.is_active = chkStatus.Checked;
            param.is_deleted = false;
            param.created_by = user.user_id;
            param.created_date = _now;
            param.modified_by = user.user_id;
            param.modified_date = _now;

            int success = 0;
            if (productStoreId > 0)
            {
                success = dataService.UpdateProductStore(param);
            }
            else
            {
                success = dataService.InsertProductStore(param);
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
            List<product_store> productStoreList = dataService.GetProductStoreList();
            message = "";

            if (string.IsNullOrEmpty(txtProductStoreCode.Text.Trim()))
            {
                message = "กรุณากรอก รหัสลูกค้า (Product Store Code)";
                return false;
            }
            if (string.IsNullOrEmpty(txtProductStoreName.Text.Trim()))
            {
                message = "กรุณากรอก ชื่อลูกค้า (Product Store Name)";
                return false;
            }

            if (productStoreList != null && productStoreList.Count > 0)
            {
                int productStoreId = GetIdFromQueryString();
                productStoreList = productStoreList.Where(i => i.product_store_id != productStoreId).ToList();
                if (productStoreList.Any(i => i.product_store_name.Trim().Equals(txtProductStoreName.Text.Trim())))
                {
                    message = "ชื่อลูกค้า (productStore Name) นี้มีอยู่ในระบบแล้ว";
                    return false;
                }

                if (productStoreList.Any(i => i.product_store_code.Trim().Equals(txtProductStoreCode.Text.Trim())))
                {
                    message = "รหัสลูกค้า (productStore Code) นี้มีอยู่ในระบบแล้ว";
                    return false;
                }
            }

            return true;
        }
        protected void lbnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.ProductStoreListUrl, false);
        }
        protected void lblSuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.ProductStoreListUrl, false);
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