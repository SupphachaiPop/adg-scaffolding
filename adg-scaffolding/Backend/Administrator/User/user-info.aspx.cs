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

namespace adg_scaffolding.Backend.Administrator.User
{
    public partial class user_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userId = GetIdFromQueryString();
                SetDataToDropDownList();
                setDataToUIByID(userId);
            }
        }

        public void SetDataToDropDownList()
        {
            Dropdown dropdown = new Dropdown();
            #region Role
            ddlRole = dropdown.GetDropdownRole(ddlRole);
            #endregion
        }

        public void setDataToUIByID(Int32 ID)
        {
            dataService dataService = new dataService();
            result_info_user user = new result_info_user();
            UtilityCommon utilityCommon = new UtilityCommon();
            chkStatus.Checked = true;
            if (ID > 0)
            {
                user = dataService.GetDataByID(ID);
                if (user != null)
                {
                    var passwordDecrypt = utilityCommon.DecryptData(encryptedText: user.password,
                                                                              encryptionkey: StaticKeys.PasswordEncrypteKey);

                    txtUserName.Text = user.username;
                    txtPassword.Text = passwordDecrypt;
                    ddlRole.SelectedValue = user.role_id.ToString();
                    txtFirstname.Text = user.first_name;
                    txtLastname.Text = user.last_name;
                    txtNickName.Text = user.nick_name;
                    txtAddress.Text = user.address;
                    txtPhone.Text = user.email;
                    txtEmail.Text = user.email;
                    txtComment.Text = user.comment;
                    chkStatus.Checked = ID != 0 ? user.is_active.Value : true;
                }
            }
            txtPassword.Attributes.Add("type", "password");
        }
        protected void lbnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            if (!validateForm(out message))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "openModalWaring('" + message + "');", true);
                return;
            }

            dataService dataService = new dataService();
            param_create_user param = new param_create_user();
            UtilityCommon utilityCommon = new UtilityCommon();
            DateTime _now = DateTime.Now;
            var user = userLogin();
            var userId = GetIdFromQueryString();
            var passwordEncrypt = utilityCommon.EncryptData(textData: txtPassword.Text,
                                                                      encryptionkey: StaticKeys.PasswordEncrypteKey);
            param.user_id = userId;
            param.username = txtUserName.Text;
            param.password = passwordEncrypt;
            param.role_id = int.Parse(ddlRole.SelectedValue);
            param.first_name = txtFirstname.Text;
            param.last_name = txtLastname.Text;
            param.nick_name = txtNickName.Text;
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
            if (userId > 0)
            {
                success = dataService.UpdateData(param);
            }
            else
            {
                success = dataService.InsertData(param);
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
            dataService dataService = new dataService();
            List<user> userList = dataService.GetDataAll();
            message = "";

            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                message = "กรุณากรอก Username";
                return false;
            }

            if (userList != null && userList.Count > 0)
            {
                int userId = GetIdFromQueryString();
                userList = userList.Where(i => i.user_id != userId).ToList();
                if (userList.Any(i => i.username.Trim().Equals(txtUserName.Text.Trim())))
                {
                    message = "Username นี้มีอยู่ในระบบแล้ว";
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                txtPassword.Focus();
                message = "กรุณากรอก Password";
                return false;
            }

            if (ddlRole.SelectedValue == "0")
            {
                message = "กรุณากรอก Company";
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
            Response.Redirect(StaticUrl.UserListUrl, false);
        }
        protected void lblSuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.UserListUrl, false);
        }
        public int DecryptCode(string enCryptCode)
        {
            UtilityCommon utilityCommon = new UtilityCommon();

            var id = utilityCommon.DecryptDataUrlEncoder<int>(encryptedText: enCryptCode,
                                                              encryptionkey: StaticKeys.DataEncrypteKey);

            return id;
        }

        public result_user_login userLogin()
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