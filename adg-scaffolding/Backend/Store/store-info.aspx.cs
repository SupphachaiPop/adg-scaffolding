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
using System.Web.Services;
using System.Web.Script.Services;
using Definitions.Static_Text;

namespace adg_scaffolding.Backend.Store
{
    [System.Web.Script.Services.ScriptService]
    public partial class store_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var StoreId = GetIdFromQueryString();
                setDataToUIByID(StoreId);
            }
        }

        public void setDataToUIByID(Int32 ID)
        {
            DataService dataService = new DataService();
            result_info_store store = new result_info_store();
            UtilityCommon utilityCommon = new UtilityCommon();
            chkStatus.Checked = true;
            if (ID > 0)
            {
                store = dataService.GetStoreInfo(ID);
                if (store != null)
                {
                    lblStoreId.Text = ID.ToString();
                    ddlProductStore.Text = store.product_store_id.ToString();
                    txtQty.Text = store.qty.ToString();
                    txtComment.Text = store.comment;
                    chkStatus.Checked = ID != 0 ? store.is_active.Value : true;
                    setDataToRepeater(store.store_history);
                }
            }
        }


        #region store history
        public void setDataToRepeater(List<result_info_store_history> storeHistoryList)
        {
            rptStoreHistory.DataSource = null;
            if (storeHistoryList != null && storeHistoryList.Count() > 0)
            {
                rptStoreHistory.DataSource = storeHistoryList.OrderBy(i => i.created_date);
                rptStoreHistory.DataBind();
            }
        }
        protected void rptStoreHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            result_info_store_history storeHistory = (result_info_store_history)e.Item.DataItem;

            Label lblDate = (Label)e.Item.FindControl("lblDate");
            Label lblLocationName = (Label)e.Item.FindControl("lblLocationName");
            Label lblZoneName = (Label)e.Item.FindControl("lblZoneName");
            Label lblQty = (Label)e.Item.FindControl("lblQty");
            Label lblComment = (Label)e.Item.FindControl("lblComment");

            lblDate.Text = storeHistory.created_date.ToString("dd/MM/YYYY");
            lblLocationName.Text = storeHistory.location_name;
            lblZoneName.Text = storeHistory.zone_name;
            lblQty.Text = storeHistory.qty.ToString();
            lblComment.Text = storeHistory.comment;

            lblQty.Attributes.Add("checked", "color:green");
            if (!storeHistory.is_inbound)
            {
                lblQty.Attributes.Add("style", "color:red");
            }
        }
        #endregion
        protected void lbnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            if (!ValidateForm(out message))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "openModalWaring('" + message + "');", true);
                return;
            }

            DataService dataService = new DataService();
            param_create_store param = new param_create_store();
            UtilityCommon utilityCommon = new UtilityCommon();
            DateTime _now = DateTime.Now;
            var user = userLogin();
            var storeId = GetIdFromQueryString();
            param.store_id = storeId;
            param.product_store_id = int.Parse(ddlProductStore.SelectedValue);
            param.qty = int.Parse(txtQty.Text);
            param.comment = txtComment.Text;
            param.is_referred = true;
            param.is_active = chkStatus.Checked;
            param.is_deleted = false;
            param.created_by = user.user_id;
            param.created_date = _now;
            param.modified_by = user.user_id;
            param.modified_date = _now;

            int success = 0;
            success = dataService.InsertStore(param);

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
            List<store> StoreList = dataService.GetStoreList();
            message = "";

            if (ddlProductStore.SelectedValue != "")
            {
                message = "กรุณาเลือกสินค้า (Product Store Name)";
                return false;
            }
            if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                message = "กรุณากรอกจำนวน (Qty)";
                return false;
            }

            return true;
        }
        protected void lbnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.StoreListUrl, false);
        }
        protected void lblSuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.StoreListUrl, false);
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