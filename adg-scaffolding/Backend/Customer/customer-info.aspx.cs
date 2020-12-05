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

namespace adg_scaffolding.Backend.Customer
{
    public partial class customer_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var customerId = GetIdFromQueryString();
                setDataToUIByID(customerId);
            }
        }

        public void setDataToUIByID(Int32 ID)
        {
            DataService dataService = new DataService();
            result_info_customer customer = new result_info_customer();
            UtilityCommon utilityCommon = new UtilityCommon();
            chkStatus.Checked = true;
            if (ID > 0)
            {
                customer = dataService.GetCustomerInfo(ID);
                if (customer != null)
                {
                    lblCustomerId.Text = ID.ToString();
                    txtCustomerName.Text = customer.customer_name;
                    txtCustomerCode.Text = customer.customer_code;
                    txtTaxNo.Text = customer.tax_no;
                    txtAddress.Text = customer.address;
                    txtPhone.Text = customer.phone;
                    txtEmail.Text = customer.email;
                    txtComment.Text = customer.comment;
                    chkStatus.Checked = ID != 0 ? customer.is_active.Value : true;
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
            param_create_customer param = new param_create_customer();
            UtilityCommon utilityCommon = new UtilityCommon();
            DateTime _now = DateTime.Now;
            var user = userLogin();
            var customerId = GetIdFromQueryString();
            param.customer_id = customerId;
            param.customer_name = txtCustomerName.Text;
            param.customer_code = txtCustomerCode.Text;
            param.tax_no = txtTaxNo.Text;
            param.address = txtAddress.Text;
            param.phone = txtPhone.Text;
            param.email = txtEmail.Text;
            param.comment = txtComment.Text;
            param.is_referred = true;
            param.is_active = chkStatus.Checked;
            param.is_deleted = false;
            param.created_by = user.user_id;
            param.created_date = _now;
            param.modified_by = user.user_id;
            param.modified_date = _now;

            int success = 0;
            if (customerId > 0)
            {
                success = dataService.UpdateCustomer(param);
            }
            else
            {
                success = dataService.InsertCustomer(param);
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
            List<customer> customerList = dataService.GetCustomerList();
            message = "";

            if (string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
            {
                message = "กรุณากรอก รหัสลูกค้า (Customer Code)";
                return false;
            }
            if (string.IsNullOrEmpty(txtCustomerName.Text.Trim()))
            {
                message = "กรุณากรอก ชื่อลูกค้า (Customer Name)";
                return false;
            }

            if (customerList != null && customerList.Count > 0)
            {
                int customerId = GetIdFromQueryString();
                customerList = customerList.Where(i => i.customer_id != customerId).ToList();
                if (customerList.Any(i => i.customer_name.Trim().Equals(txtCustomerName.Text.Trim())))
                {
                    message = "ชื่อลูกค้า (Customer Name) นี้มีอยู่ในระบบแล้ว";
                    return false;
                }

                if (customerList.Any(i => i.customer_code.Trim().Equals(txtCustomerCode.Text.Trim())))
                {
                    message = "รหัสลูกค้า (Customer Code) นี้มีอยู่ในระบบแล้ว";
                    return false;
                }
            }


            if (string.IsNullOrEmpty(txtTaxNo.Text.Trim()))
            {
                txtTaxNo.Focus();
                message = "กรุณากรอกเลขประจำตัวผู้เสียภาษี (Tax No)";
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
                    message = "รูปแบบอีเมล์ไม่ถูกต้อง (Email)";
                    return false;
                }
            }

            return true;
        }
        protected void lbnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.CustomerListUrl, false);
        }
        protected void lblSuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.CustomerListUrl, false);
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