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

namespace adg_scaffolding.Backend.Administrator.Role
{
    public partial class role_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var roleId = GetIdFromQueryString();
                SetDataToDropDownList();
                SetControlNotHeadOffice();
                setDataToUIByID(roleId);
            }
        }

        public void SetControlNotHeadOffice()
        {
            var user = userLogin();
            if (!user.is_manage)
            {
                ddlCompany.SelectedValue = user.company_id.ToString();
                DropdownCompany.Attributes.Add("style", "display:none;");
                setDataToRepeater(0, user.company_id);
            }
        }
        public void SetDataToDropDownList()
        {
            Dropdown dropdown = new Dropdown();
            ddlCompany = dropdown.GetDropdownCompany(ddlCompany);
        }
        public RoleEntity GetDataRole(Int32 roleId)
        {
            RoleService roleService = new RoleService();
            RoleEntity roleEntity = new RoleEntity();
            roleEntity = roleService.GetDataByID(roleId);

            return roleEntity;
        }
        public List<RoleMenuEntity> GetDataRoleMenu(Int32 roleId, Int32 companyId)
        {
            RoleMenuService roleMenuService = new RoleMenuService();
            List<RoleMenuEntity> roleMenuEntities = new List<RoleMenuEntity>();
            roleMenuEntities = roleMenuService.GetDataByCondition(roleId, companyId);

            return roleMenuEntities;
        }
        public void setDataToUIByID(Int32 ID)
        {
            var companyId = 0;
            chkStatus.Checked = true;
            if (ID != 0)
            {
                var roleEntity = GetDataRole(ID);
                if (roleEntity != null)
                {
                    companyId = roleEntity.company_id;
                    txtRoleCode.Text = roleEntity.role_code;
                    ddlCompany.SelectedValue = companyId.ToString();
                    txtRoleName.Text = roleEntity.role_name;
                    txtComment.Text = roleEntity.comment;
                    chkStatus.Checked = ID != 0 ? roleEntity.is_active : true;
                }
            }
            setDataToRepeater(ID, companyId);
        }

        public void setDataToRepeater(Int32 roleId, Int32 companyId)
        {
            List<RoleMenuEntity> roleMenuEntities = new List<RoleMenuEntity>();
            roleMenuEntities = GetDataRoleMenu(roleId, companyId);

            rptRoleMenu.DataSource = null;
            if (roleMenuEntities != null && roleMenuEntities.Count() > 0)
            {
                rptRoleMenu.DataSource = roleMenuEntities;
                rptRoleMenu.DataBind();
            }
        }

        protected void rptRoleMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RoleMenuEntity copmpanyMenuEntity = (RoleMenuEntity)e.Item.DataItem;

            Label lblroleMenuId = (Label)e.Item.FindControl("lblroleMenuId");
            Label lblMenuId = (Label)e.Item.FindControl("lblMenuId");
            Label lblMenuName = (Label)e.Item.FindControl("lblMenuName");
            HtmlInputCheckBox chkStatus = (HtmlInputCheckBox)e.Item.FindControl("chkStatus");
            HiddenField hdfStatus = (HiddenField)e.Item.FindControl("hdfStatus");

            lblroleMenuId.Text = copmpanyMenuEntity.role_item_id.ToString();
            lblMenuId.Text = copmpanyMenuEntity.menu_id.ToString();
            lblMenuName.Text = copmpanyMenuEntity.menu_name.ToString();
            hdfStatus.Value = copmpanyMenuEntity.is_active.ToString();

             if (copmpanyMenuEntity.is_display == true)
            {
                chkStatus.Attributes.Add("checked", "checked");
            }
            else if (copmpanyMenuEntity.is_display == false)
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

                RoleService roleService = new RoleService();
                RoleEntity roleEntity = new RoleEntity();
                roleEntity.roleMenuEntities = new List<RoleMenuEntity>();

                var roleId = GetIdFromQueryString();
                roleEntity.role_id = roleId;
                roleEntity.role_code = txtRoleCode.Text;
                roleEntity.role_name = txtRoleName.Text;
                roleEntity.company_id = int.Parse(ddlCompany.SelectedValue);
                roleEntity.comment = txtComment.Text;
                roleEntity.is_active = chkStatus.Checked;
                roleEntity.created_by = user.user_id;
                roleEntity.modified_by = user.user_id;
                roleEntity.roleMenuEntities = GetDataRoleMenu();

                if (roleId > 0)
                {
                    success = roleService.UpdateData(roleEntity);
                }
                else
                {
                    success = roleService.InsertData(roleEntity);
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
            RoleService roleService = new RoleService();
            var roleList = roleService.GetDataAll();
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
            if (ddlCompany.SelectedValue == "0")
            {
                message = "กรุณากรอก Company";
                return false;
            }

            return true;
        }
        public List<RoleMenuEntity> GetDataRoleMenu()
        {
            var user = userLogin();
            List<RoleMenuEntity> roleMenuEntities = new List<RoleMenuEntity>();

            for (int item = 0; item < rptRoleMenu.Items.Count; item++)
            {
                Label lblroleMenuId = (Label)rptRoleMenu.Items[item].FindControl("lblroleMenuId");
                Label lblMenuId = (Label)rptRoleMenu.Items[item].FindControl("lblMenuId");
                HtmlInputCheckBox chkStatus = (HtmlInputCheckBox)rptRoleMenu.Items[item].FindControl("chkStatus");
                HiddenField hdfStatus = (HiddenField)rptRoleMenu.Items[item].FindControl("hdfStatus");

                roleMenuEntities.Add(new RoleMenuEntity
                {
                    role_item_id = int.Parse(lblroleMenuId.Text),
                    menu_id = int.Parse(lblMenuId.Text),
                    is_display = chkStatus.Checked,
                    created_by = user.user_id,
                    modified_by = user.user_id,
                    is_active = true
                });
            }

            return roleMenuEntities;
        }
        protected void lbnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.RoleListUrl, false);
        }
        protected void lblSuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.RoleListUrl, false);
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

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            var roleId = GetIdFromQueryString();
            setDataToRepeater(roleId, int.Parse(ddlCompany.SelectedValue));
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "InitSelect2();", true);
        }
    }
}