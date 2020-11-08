using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Backend;
using Definitions;
using Service.Backend;
using System.Web.UI.HtmlControls;

namespace adg_scaffolding.Backend.Administrator.Company
{
    public partial class company_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var companyId = GetIdFromQueryString();
                SetDataToDropDownList();
                SetControlNotHeadOffice();
                setDataToUIByID(companyId);
            }
        }

        public void SetControlNotHeadOffice()
        {
            var user = userLogin();
            if (!user.is_manage)
            {
                DropdownCompanyType.Attributes.Add("style", "display:none;");
            }
        }

        public void SetDataToDropDownList()
        {
            Dropdown dropdown = new Dropdown();
            ddlCompanyType = dropdown.GetDropdownCompanyType(ddlCompanyType);
        }
        public void setDataToUIByID(Int32 ID)
        {
            CompanyService companyService = new CompanyService();
            CompanyEntity companyEntity = new CompanyEntity();
            companyEntity = companyService.GetDataByID(ID);

            chkStatus.Checked = true;
            if (companyEntity != null)
            {
                txtCompanyCode.Text = companyEntity.company_code;
                ddlCompanyType.SelectedValue = companyEntity.company_type.ToString();
                txtCompanyName.Text = companyEntity.company_name;
                txtBillingAddress.Text = companyEntity.billing_address;
                txtShippingAddress.Text = companyEntity.shipping_address;
                txtContactName.Text = companyEntity.contact_name;
                txtPhone.Text = companyEntity.phone;
                txtTax.Text = companyEntity.tax_no;
                txtEmail.Text = companyEntity.email;
                txtComment.Text = companyEntity.comment;
                chkStatus.Checked = ID != 0 ? companyEntity.is_active : true;
            }
            txtPhone.Attributes.Add("type", "tel");

            if (companyEntity.companyMenuEntities != null && companyEntity.companyMenuEntities.Count() > 0)
            {
                rptCompanyMenu.DataSource = companyEntity.companyMenuEntities;
                rptCompanyMenu.DataBind();
            }
        }
        protected void rptCompanyMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            CompanyMenuEntity copmpanyMenuEntity = (CompanyMenuEntity)e.Item.DataItem;

            Label lblCompanyMenuId = (Label)e.Item.FindControl("lblCompanyMenuId");
            Label lblMenuId = (Label)e.Item.FindControl("lblMenuId");
            Label lblMenuName = (Label)e.Item.FindControl("lblMenuName");
            HtmlInputCheckBox chkStatus = (HtmlInputCheckBox)e.Item.FindControl("chkStatus");
            HiddenField hdfStatus = (HiddenField)e.Item.FindControl("hdfStatus");

            lblCompanyMenuId.Text = copmpanyMenuEntity.company_menu_id.ToString();
            lblMenuId.Text = copmpanyMenuEntity.menu_id.ToString();
            lblMenuName.Text = copmpanyMenuEntity.menu_name.ToString();
            hdfStatus.Value = copmpanyMenuEntity.is_active.ToString();

            if (copmpanyMenuEntity.is_active == true)
            {
                chkStatus.Attributes.Add("checked", "checked");
            }
            else if (copmpanyMenuEntity.is_active == false)
            {
                chkStatus.Attributes.Remove("checked");
            }
        }
        protected void lbnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            var user = userLogin();
            int success = 0;

            try
            {
                if (!validateForm(out message))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "openModalWaring('" + message + "');", true);
                    return;
                }

                CompanyService companyService = new CompanyService();
                CompanyEntity companyEntity = new CompanyEntity();
                companyEntity.companyMenuEntities = new List<CompanyMenuEntity>();

                var companyId = GetIdFromQueryString();
                companyEntity.company_id = companyId;
                companyEntity.company_code = txtCompanyCode.Text;
                companyEntity.company_name = txtCompanyName.Text;
                companyEntity.company_type = int.Parse(ddlCompanyType.SelectedValue);
                companyEntity.tax_no = txtTax.Text;
                companyEntity.billing_address = txtBillingAddress.Text;
                companyEntity.shipping_address = txtShippingAddress.Text;
                companyEntity.contact_name = txtContactName.Text;
                companyEntity.phone = txtPhone.Text;
                companyEntity.email = txtEmail.Text;
                companyEntity.comment = txtComment.Text;
                companyEntity.is_active = chkStatus.Checked;
                companyEntity.created_by = user.user_id;
                companyEntity.modified_by = user.user_id;
                companyEntity.companyMenuEntities = GetDataCompanyMenu();

                if (companyId > 0)
                {
                    success = companyService.UpdateData(companyEntity);
                }
                else
                {
                    success = companyService.InsertData(companyEntity);
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
                var mEr = ex.Message;
            }

            if (success > 0)
            {
                message = "บันทึกข้อมูลสำเร็จ";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "openModalSuccess('" + message + "');", true);
            }
            else
            {
                message = "บันทึกข้อมูลไม่สำเร็จ";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "openModalError();", true);
                return;
            }
        }
        public bool validateForm(out string message)
        {
            CompanyService companyService = new CompanyService();
            var companyList = companyService.GetDataAll();
            message = "";

            if (companyList != null && companyList.Count > 0)
            {
                var companyId = GetIdFromQueryString();
                companyList = companyList.Where(i => i.company_id != companyId).ToList();
                if (companyList.Any(i => i.company_code.Trim().Equals(txtCompanyCode.Text.Trim())))
                {
                    message = "Company Code นี้มีอยู่ในระบบแล้ว”";
                    return false;
                }

                if (companyList.Any(i => i.company_name.Trim().Equals(txtCompanyName.Text.Trim())))
                {
                    message = "Company Name นี้มีอยู่ในระบบแล้ว”";
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtCompanyCode.Text.Trim()))
            {
                txtCompanyCode.Focus();
                message = "กรุณากรอก Company Code";
                return false;
            }
            if (string.IsNullOrEmpty(txtCompanyName.Text.Trim()))
            {
                txtCompanyName.Focus();
                message = "กรุณากรอก Company Name";
                return false;
            }
            if (ddlCompanyType.SelectedValue == "0")
            {
                message = "กรุณากรอก Company Type";
                return false;
            }
            if (string.IsNullOrEmpty(txtBillingAddress.Text.Trim()))
            {
                txtBillingAddress.Focus();
                message = "กรุณากรอก Billing Address";
                return false;
            }
            if (string.IsNullOrEmpty(txtShippingAddress.Text.Trim()))
            {
                txtShippingAddress.Focus();
                message = "กรุณากรอก Shipping Address";
                return false;
            }
            if (string.IsNullOrEmpty(txtTax.Text.Trim()))
            {
                txtTax.Focus();
                message = "กรุณากรอก Tax no";
                return false;
            }

            if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                try
                {
                    var email = txtEmail.Text.Trim();
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    message = "รูปแบบอีเมล์ไม่ถูกต้อง";
                    return false;
                }
            }

            return true;
        }
        public List<CompanyMenuEntity> GetDataCompanyMenu()
        {
            var user = userLogin();
            List<CompanyMenuEntity> companyMenuEntities = new List<CompanyMenuEntity>();

            for (int item = 0; item < rptCompanyMenu.Items.Count; item++)
            {
                Label lblCompanyMenuId = (Label)rptCompanyMenu.Items[item].FindControl("lblCompanyMenuId");
                Label lblMenuId = (Label)rptCompanyMenu.Items[item].FindControl("lblMenuId");
                HtmlInputCheckBox chkStatus = (HtmlInputCheckBox)rptCompanyMenu.Items[item].FindControl("chkStatus");
                HiddenField hdfStatus = (HiddenField)rptCompanyMenu.Items[item].FindControl("hdfStatus");

                companyMenuEntities.Add(new CompanyMenuEntity
                {
                    company_menu_id = int.Parse(lblCompanyMenuId.Text),
                    menu_id = int.Parse(lblMenuId.Text),
                    is_active = chkStatus.Checked,
                    created_by = user.user_id,
                    modified_by = user.user_id
                });
            }

            return companyMenuEntities;
        }
        protected void lbnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.CompanyListUrl, false);
        }
        protected void lblSuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.CompanyListUrl, false);
        }
        public UserEntity userLogin()
        {
            UserEntity user = new UserEntity();
            if ((HttpContext.Current.Session["userLoginBackend"] != null))
            {
                user = (UserEntity)HttpContext.Current.Session["userLoginBackend"];
            }

            return user;
        }
        public int GetIdFromQueryString()
        {
            return Request.QueryString["ID"] != null ? DecryptCode(Request.QueryString["ID"]) : 0;
        }
        public int DecryptCode(string enCryptCode)
        {
            UtilityCommon utilityCommon = new UtilityCommon();
            var id = utilityCommon.DecryptDataUrlEncoder<int>(encryptedText: enCryptCode,
                                                              encryptionkey: StaticKeys.DataEncrypteKey);

            return id;
        }
    }
}