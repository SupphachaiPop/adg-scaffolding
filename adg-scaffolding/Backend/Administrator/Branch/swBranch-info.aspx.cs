using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Backend;
using Definitions;
using Service.Backend;

namespace adg_scaffolding.Backend.Administrator.Branch
{
    public partial class swBranch_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var branchId = GetIdFromQueryString();
                SetDataToDropDownList();
                SetControlNotHeadOffice();
                setDataToUIByID(branchId);
                txtPhone.Attributes.Add("type", "tel");
            }
        }

        public void SetDataToDropDownList()
        {
            Dropdown dropdown = new Dropdown();
            #region Company
            ddlCompany = dropdown.GetDropdownCompany(ddlCompany);
            #endregion

            #region Branch Type
            ddlBranchType = dropdown.GetDropdownBranchType(ddlBranchType);
            #endregion
        }

        public void SetControlNotHeadOffice()
        {
            var user = userLogin();
            if (!user.is_manage)
            {
                ddlCompany.SelectedValue = user.company_id.ToString();
                DropdownCompany.Attributes.Add("style", "display:none;");
            }
        }

        public void setDataToUIByID(Int32 ID)
        {
            swBranchService swBranchService = new swBranchService();
            swBranchEntity swBranchEntity = new swBranchEntity();
            chkStatus.Checked = true;
            if (ID != 0)
            {
                swBranchEntity = swBranchService.GetDataByID(ID);
                if (swBranchEntity != null)
                {
                    txtBranchCode.Text = swBranchEntity.branch_code;
                    ddlCompany.SelectedValue = swBranchEntity.company_id.ToString();
                    ddlBranchType.SelectedValue = swBranchEntity.branch_type.ToString();
                    txtBranchName.Text = swBranchEntity.branch_name;
                    txtBillingAddress.Text = swBranchEntity.billing_address;
                    txtShippingAddress.Text = swBranchEntity.shipping_address;
                    txtContactName.Text = swBranchEntity.contact_name;
                    txtPhone.Text = swBranchEntity.phone;
                    txtBranchNo.Text = swBranchEntity.branch_no;
                    txtEmail.Text = swBranchEntity.email;
                    txtComment.Text = swBranchEntity.comment;
                    chkStatus.Checked = ID != 0 ? swBranchEntity.is_active : true;
                }
            }
        }
        protected void lbnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            if (!validateForm(out message))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "openModalWaring('" + message + "');", true);
                return;
            }

            swBranchService swBranchService = new swBranchService();
            swBranchEntity swBranchEntity = new swBranchEntity();

            var user = userLogin();
            var branchId = GetIdFromQueryString();
            swBranchEntity.branch_id = branchId;
            swBranchEntity.branch_code = txtBranchCode.Text;
            swBranchEntity.branch_name = txtBranchName.Text;
            swBranchEntity.company_id = int.Parse(ddlCompany.SelectedValue);
            swBranchEntity.branch_type = int.Parse(ddlBranchType.SelectedValue);
            swBranchEntity.branch_no = txtBranchNo.Text;
            swBranchEntity.billing_address = txtBillingAddress.Text;
            swBranchEntity.shipping_address = txtShippingAddress.Text;
            swBranchEntity.contact_name = txtContactName.Text;
            swBranchEntity.phone = txtPhone.Text;
            swBranchEntity.email = txtEmail.Text;
            swBranchEntity.comment = txtComment.Text;
            swBranchEntity.is_active = chkStatus.Checked;
            swBranchEntity.created_by = user.user_id;
            swBranchEntity.modified_by = user.user_id;

            int success = 0;
            if (branchId > 0)
            {
                success = swBranchService.UpdateData(swBranchEntity);
            }
            else
            {
                success = swBranchService.InsertData(swBranchEntity);
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
        public bool validateForm(out string message)
        {
            swBranchService swBranchService = new swBranchService();
            var branchList = swBranchService.GetDataAll();
            message = "";

            if (branchList != null && branchList.Count > 0)
            {
                var branchId = GetIdFromQueryString();
                branchList = branchList.Where(i => i.branch_id != branchId).ToList();
                if (branchList.Any(i => i.branch_code.Trim().Equals(txtBranchCode.Text.Trim())))
                {
                    message = "Branch Code นี้มีอยู่ในระบบแล้ว";
                    return false;
                }

                if (branchList.Any(i => i.branch_name.Trim().Equals(txtBranchName.Text.Trim())))
                {
                    message = "Branch Name นี้มีอยู่ในระบบแล้ว";
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtBranchCode.Text.Trim()))
            {
                txtBranchCode.Focus();
                message = "กรุณากรอก Branch Code";
                return false;
            }
            if (string.IsNullOrEmpty(txtBranchName.Text.Trim()))
            {
                txtBranchName.Focus();
                message = "กรุณากรอก Branch Name";
                return false;
            }
            if (ddlBranchType.SelectedValue == "0")
            {
                message = "กรุณากรอก Branch Type";
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
        protected void lbnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.BranchListUrl, false);
        }
        protected void lblSuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.BranchListUrl, false);
        }
        public int DecryptCode(string enCryptCode)
        {
            UtilityCommon utilityCommon = new UtilityCommon();

            var id = utilityCommon.DecryptDataUrlEncoder<int>(encryptedText: enCryptCode,
                                                              encryptionkey: StaticKeys.DataEncrypteKey);

            return id;
        }

        public UserEntity userLogin()
        {
            UserEntity user = new UserEntity()
                ;
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
    }
}