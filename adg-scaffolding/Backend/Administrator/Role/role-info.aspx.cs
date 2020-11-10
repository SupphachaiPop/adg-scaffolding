using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using Definitions;
using Service.Backend;
using System.Web.UI.HtmlControls;

namespace adg_scaffolding.Backend.Administrator.Role
{
    public partial class role_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var roleId = GetIdFromQueryString();
                setDataToUIByID(roleId);
            }
        }
        public result_info_role GetDataRole(Int32 roleId)
        {
            DataService DataService = new DataService();
            result_info_role role = new result_info_role();
            role = DataService.GetRoleInfo(roleId);

            return role;
        }
        public void setDataToUIByID(Int32 ID)
        {
            chkStatus.Checked = true;
            var role = GetDataRole(ID);
            if (role != null)
            {
                txtRoleCode.Text = role.role_code;
                txtRoleName.Text = role.role_name;
                txtComment.Text = role.comment;
                chkStatus.Checked = ID != 0 ? role.is_active.Value : true;
            }
            setDataToRepeater(ID,role.role_menu);
        }

        public void setDataToRepeater(int roleId, List<result_info_role_menu> role_menus)
        {
            rptRoleMenu.DataSource = null;
            if (role_menus != null && role_menus.Count() > 0)
            {
                rptRoleMenu.DataSource = role_menus.OrderBy(i => i.menu_seq_render);
                rptRoleMenu.DataBind();
            }
        }

        protected void rptRoleMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            result_info_role_menu role_menu = (result_info_role_menu)e.Item.DataItem;

            Label lblroleMenuId = (Label)e.Item.FindControl("lblroleMenuId");
            Label lblMenuId = (Label)e.Item.FindControl("lblMenuId");
            Label lblMenuName = (Label)e.Item.FindControl("lblMenuName");
            HtmlInputCheckBox chkStatus = (HtmlInputCheckBox)e.Item.FindControl("chkStatus");
            HiddenField hdfStatus = (HiddenField)e.Item.FindControl("hdfStatus");

            lblroleMenuId.Text = role_menu.role_menu_id.ToString();
            lblMenuId.Text = role_menu.menu_id.ToString();
            lblMenuName.Text = role_menu.menu_name_render.ToString();
            hdfStatus.Value = role_menu.is_display.ToString();

            role_menu.is_display = role_menu.is_display != null ? role_menu.is_display : false;
            if (role_menu.is_display == true)
            {
                chkStatus.Attributes.Add("checked", "checked");
            }
            else if (role_menu.is_display == false)
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

                DataService DataService = new DataService();
                param_create_role role = new param_create_role();
                role.role_menu = new List<param_create_role_menu>();

                var roleId = GetIdFromQueryString();
                role.role_id = roleId;
                role.role_code = txtRoleCode.Text;
                role.role_name = txtRoleName.Text;
                role.comment = txtComment.Text;
                role.is_referred = false;
                role.is_active = chkStatus.Checked;
                role.is_deleted = false;
                role.created_by = user.user_id;
                role.modified_by = user.user_id;
                role.role_menu = GetDataRoleMenu();

                if (roleId > 0)
                {
                    success = DataService.UpdateRole(role);
                }
                else
                {
                    success = DataService.InsertRole(role);
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
            DataService DataService = new DataService();
            var roleList = DataService.GetRoleList();
            message = "";

            if (roleList != null && roleList.Count > 0)
            {
                var roleId = GetIdFromQueryString();
                roleList = roleList.Where(i => i.role_id != roleId).ToList();
                if (roleList.Any(i => i.role_code.Trim().Equals(txtRoleCode.Text.Trim())))
                {
                    message = "role Code นี้มีอยู่ในระบบแล้ว”";
                    return false;
                }

                if (roleList.Any(i => i.role_name.Trim().Equals(txtRoleName.Text.Trim())))
                {
                    message = "role Name นี้มีอยู่ในระบบแล้ว”";
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtRoleCode.Text.Trim()))
            {
                txtRoleCode.Focus();
                message = "กรุณากรอก role Code";
                return false;
            }
            if (string.IsNullOrEmpty(txtRoleName.Text.Trim()))
            {
                txtRoleName.Focus();
                message = "กรุณากรอก role Name";
                return false;
            }

            return true;
        }
        public List<param_create_role_menu> GetDataRoleMenu()
        {
            var user = userLogin();
            List<param_create_role_menu> role_menus = new List<param_create_role_menu>();

            for (int item = 0; item < rptRoleMenu.Items.Count; item++)
            {
                Label lblroleMenuId = (Label)rptRoleMenu.Items[item].FindControl("lblroleMenuId");
                Label lblMenuId = (Label)rptRoleMenu.Items[item].FindControl("lblMenuId");
                HtmlInputCheckBox chkStatus = (HtmlInputCheckBox)rptRoleMenu.Items[item].FindControl("chkStatus");
                HiddenField hdfStatus = (HiddenField)rptRoleMenu.Items[item].FindControl("hdfStatus");

                role_menus.Add(new param_create_role_menu
                {
                    role_menu_id = int.Parse(lblroleMenuId.Text),
                    menu_id = int.Parse(lblMenuId.Text),
                    is_display = chkStatus.Checked,
                    created_by = user.user_id,
                    modified_by = user.user_id,
                    is_active = true
                });
            }

            return role_menus;
        }
        protected void lbnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.RoleListUrl, false);
        }
        protected void lblSuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.RoleListUrl, false);
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

        public int DecryptCode(string enCryptCode)
        {
            UtilityCommon utilityCommon = new UtilityCommon();
            var id = utilityCommon.DecryptDataUrlEncoder<int>(encryptedText: enCryptCode,
                                                              encryptionkey: StaticKeys.DataEncrypteKey);

            return id;
        }
    }
}