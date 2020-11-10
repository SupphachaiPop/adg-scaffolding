using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service.Backend;
using Definitions;
using Entity;

namespace adg_scaffolding.Backend
{
    public partial class loginXc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["userLoginBackend"] = null;
            }
            Session.Clear();
            Session.RemoveAll();
        }

        protected void btnSignin_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;

            if (txtUsername.Text != "" && txtPassword.Text != "")
            {
                param_user_login param = new param_user_login();
                result_user_login userLogin = new result_user_login();
                UserLoginService userLoginService = new UserLoginService();
                UtilityCommon util = new UtilityCommon();

                var passwordEncrypt = util.EncryptData(textData: txtPassword.Text,
                                                       encryptionkey: StaticKeys.PasswordEncrypteKey);
                param.username = txtUsername.Text.Trim();
                param.password = passwordEncrypt;
                userLogin = userLoginService.GetUserLogin(param);

                if (userLogin != null)
                {
                    Session["userLoginBackend"] = userLogin;
                    Response.Redirect("~/Backend/Administrator/User/user-list.aspx");
                }
                else
                {
                    msg = "Username ไม่มีในระบบ";
                    txtUsername.Text = string.Empty;
                    ScriptManager.RegisterStartupScript((sender as Control), this.GetType(), "alertError", "WarningAlertWithFocus('" + msg + "', 'input[id*=txtUsername]');", true);
                }
            }
            else
            {
                msg = "กรุณาระบุชื่อผู้ใช้งาน หรือรหัสผ่าน";
                ScriptManager.RegisterStartupScript((sender as Control), this.GetType(), "alertError", "WarningAlertWithFocus('" + msg + "', 'input[id*=txtUsername]');", true);
            }
        }

        //public UserEntity GetUserLogin()
        //{
        //    UserEntity userEntity = new UserEntity();
        //    userEntity = (UserEntity)Session["userLoginBackend"];
        //    return userEntity;
        //}
    }
}
