using Definitions;
using Entity;
using Service.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace adg_scaffolding.Backend.Product_Management.Specification
{
    public partial class specification_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var specificationId = GetIdFromQueryString();
                setDataToUIByID(specificationId);
            }
        }
        public result_info_specification GetDataspecification(Int32 specificationId)
        {
            DataService DataService = new DataService();
            result_info_specification specification = new result_info_specification();
            specification = DataService.GetSpecificationInfo(specificationId);

            return specification;
        }
        public void setDataToUIByID(Int32 ID)
        {
            chkStatus.Checked = true;
            var specification = GetDataspecification(ID);
            if (specification != null)
            {
                txtSpecificationCode.Text = specification.specification_code;
                txtSpecificationName.Text = specification.specification_name;
                txtComment.Text = specification.comment;
                chkStatus.Checked = ID != 0 ? specification.is_active.Value : true;
                setDataToRepeater(specification.sub_specifications);
            }
        }


        protected void btnCreateSubSpecification_Click(object sender, EventArgs e)
        {
            var res = new List<result_info_sub_specification>();
            var a = GetDataSubSpecification();
            a.ForEach(i =>
            {
                var subSpecification = new result_info_sub_specification();
                subSpecification.sub_specification_id = i.sub_specification_id;
                subSpecification.sub_specification_name = i.sub_specification_name;
                subSpecification.is_active = i.is_active;
                res.Add(subSpecification);
            });
            res.Add(new result_info_sub_specification());
            setDataToRepeater(res);
        }

        public void setDataToRepeater(List<result_info_sub_specification> sub_specification)
        {
            rptSubSpecification.DataSource = null;
            if (sub_specification != null && sub_specification.Count() > 0)
            {
                var seq = 1;
                sub_specification.ForEach(i =>
                {
                    i.seq = seq;
                    seq += 1;
                });
                rptSubSpecification.DataSource = sub_specification;
                rptSubSpecification.DataBind();
            }
        }

        protected void rptSubSpecification_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            result_info_sub_specification item = (result_info_sub_specification)e.Item.DataItem;
            Label lblNo = (Label)e.Item.FindControl("lblNo");
            Label lblSubSpecificationId = (Label)e.Item.FindControl("lblSubSpecificationId");
            TextBox txtSubSpecificationName = (TextBox)e.Item.FindControl("txtSubSpecificationName");
            HtmlInputCheckBox chkStatus = (HtmlInputCheckBox)e.Item.FindControl("chkStatus");
            HiddenField hdfStatus = (HiddenField)e.Item.FindControl("hdfStatus");
            LinkButton lbnDelete = (LinkButton)e.Item.FindControl("lbnDelete");

            lblNo.Text = item.seq.ToString();
            lblSubSpecificationId.Text = item.sub_specification_id.ToString();
            txtSubSpecificationName.Text = item.sub_specification_name;

            hdfStatus.Value = item.is_active.ToString();

            item.is_active = item.is_active != null ? item.is_active : false;
            if (item.is_active == true)
            {
                chkStatus.Attributes.Add("checked", "checked");
            }
            else if (item.is_active == false)
            {
                chkStatus.Attributes.Remove("checked");
            }

            if (item.sub_specification_id != 0)
            {
                lbnDelete.Enabled = false;
                lbnDelete.Attributes.Add("Style", "cursor: not-allowed");
            }

            lbnDelete.CommandName = "Delete";
            lbnDelete.CommandArgument = item.sub_specification_id.ToString();
        }

        protected void rptSubSpecification_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                e.Item.Visible = false;
            }
        }

        protected void lbnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            var user = userLogin();
            int success = 0;

            try
            {
                if (!ValidateForm(out message))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "openModalWaring('" + message + "');", true);
                    return;
                }

                DataService DataService = new DataService();
                param_create_specification specification = new param_create_specification();
                specification.sub_specifications = new List<param_create_sub_specification>();

                var specificationId = GetIdFromQueryString();
                specification.specification_id = specificationId;
                specification.specification_code = txtSpecificationCode.Text;
                specification.specification_name = txtSpecificationName.Text;
                specification.comment = txtComment.Text;
                specification.is_referred = false;
                specification.is_active = chkStatus.Checked;
                specification.is_deleted = false;
                specification.created_by = user.user_id;
                specification.modified_by = user.user_id;
                specification.sub_specifications = GetDataSubSpecification();

                if (specificationId > 0)
                {
                    success = DataService.UpdateSpecification(specification);
                }
                else
                {
                    success = DataService.InsertSpecification(specification);
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
        public bool ValidateForm(out string message)
        {
            DataService DataService = new DataService();
            var specificationList = DataService.GetSpecificationList();
            message = "";

            if (specificationList != null && specificationList.Count > 0)
            {
                var specificationId = GetIdFromQueryString();
                specificationList = specificationList.Where(i => i.specification_id != specificationId).ToList();
                if (specificationList.Any(i => i.specification_code.Trim().Equals(txtSpecificationCode.Text.Trim())))
                {
                    message = "Specification Code นี้มีอยู่ในระบบแล้ว”";
                    return false;
                }

                if (specificationList.Any(i => i.specification_name.Trim().Equals(txtSpecificationName.Text.Trim())))
                {
                    message = "Specification Name นี้มีอยู่ในระบบแล้ว”";
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtSpecificationCode.Text.Trim()))
            {
                txtSpecificationCode.Focus();
                message = "กรุณากรอก Specification Code";
                return false;
            }
            if (string.IsNullOrEmpty(txtSpecificationName.Text.Trim()))
            {
                txtSpecificationName.Focus();
                message = "กรุณากรอก Specification Name";
                return false;
            }

            return true;
        }
        public List<param_create_sub_specification> GetDataSubSpecification()
        {
            var user = userLogin();
            List<param_create_sub_specification> specification_menus = new List<param_create_sub_specification>();

            for (int item = 0; item < rptSubSpecification.Items.Count; item++)
            {
                Label lblNo = (Label)rptSubSpecification.Items[item].FindControl("lblNo");
                Label lblSubSpecificationId = (Label)rptSubSpecification.Items[item].FindControl("lblSubSpecificationId");
                TextBox txtSubSpecificationName = (TextBox)rptSubSpecification.Items[item].FindControl("txtSubSpecificationName");
                HtmlInputCheckBox chkStatus = (HtmlInputCheckBox)rptSubSpecification.Items[item].FindControl("chkStatus");
                HiddenField hdfStatus = (HiddenField)rptSubSpecification.Items[item].FindControl("hdfStatus");

                if (rptSubSpecification.Items[item].Visible)
                {
                    specification_menus.Add(new param_create_sub_specification
                    {
                        sub_specification_id = int.Parse(lblSubSpecificationId.Text),
                        sub_specification_name = txtSubSpecificationName.Text,
                        is_active = chkStatus.Checked,
                        created_by = user.user_id,
                        modified_by = user.user_id,
                    });
                }
            }

            return specification_menus;
        }
        protected void lbnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.SpecificationListUrl, false);
        }
        protected void lblSuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.SpecificationListUrl, false);
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