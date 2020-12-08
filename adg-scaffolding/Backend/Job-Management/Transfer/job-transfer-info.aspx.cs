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

namespace adg_scaffolding.Backend.Job_Management.Transfer
{
    public partial class job_transfer_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var zoneId = GetIdFromQueryString();
                SetDropdownList();
            }
        }

        public void SetDropdownList()
        {
            Dropdown dropdown = new Dropdown();
            ddlLocationSource = dropdown.GetDropdownProduct(ddlLocationSource);
        }

        protected void ddlLocationSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dropdown dropdown = new Dropdown();
            ddlLocationDestination = dropdown.GetDropdownProduct(ddlLocationDestination);
            ddlZoneSource = dropdown.GetDropdownProduct(ddlZoneSource);

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "InitSelect2();", true);
        }

        protected void ddlLocationDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dropdown dropdown = new Dropdown();
            ddlZoneDestination = dropdown.GetDropdownProduct(ddlZoneDestination);

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "InitSelect2();", true);
        }

        protected void ddlZoneSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dropdown dropdown = new Dropdown();
            var selectSourceZoneId = int.Parse(ddlZoneSource.SelectedValue);
            ddlProduct = dropdown.GetDropdownJob(ddlZoneDestination, selectSourceZoneId);
            txtAmount.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "InitSelect2();", true);
        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAmount.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "InitSelect2();", true);
        }

        protected void btnCreateJobTransfer_Click(object sender, EventArgs e)
        {
            string message = "";
            if (!ValidateForm(out message))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "openModalWaring('" + message + "');", true);
                return;
            }

            var res = PrepareDataBeforeToRepeater();
            res.Add(new result_info_job_zone
            {
                job_id = 0,
                product_id = int.Parse(ddlProduct.SelectedValue),
                product_name = ddlProduct.SelectedItem.ToString(),
                amount = int.Parse(txtAmount.Text),
                is_deleted = false
            });
            setDataToRepeater(res);

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "InitSelect2();", true);
        }

        public List<result_info_job_zone> PrepareDataBeforeToRepeater()
        {
            var res = new List<result_info_job_zone>();
            var zoneId = GetIdFromQueryString();
            var jobList = GetDataJob(zoneId);
            jobList.ForEach(i =>
            {
                var job = new result_info_job_zone();
                job.job_id = i.job_id;
                job.product_id = i.product_id;
                job.product_name = i.product_name;
                job.amount = i.amount;
                job.comment = i.comment;
                job.is_deleted = i.is_deleted;
                res.Add(job);
            });

            return res;
        }

        public void setDataToRepeater(List<result_info_job_zone> jobTransferList)
        {
            rptJobTransfer.DataSource = null;
            if (jobTransferList != null && jobTransferList.Count() > 0)
            {
                var seq = 1;
                jobTransferList.ForEach(i =>
                {
                    if (!i.is_deleted.Value)
                    {
                        i.seq = seq;
                        seq += 1;
                    }

                });
                rptJobTransfer.DataSource = jobTransferList.OrderBy(i => i.seq);
                rptJobTransfer.DataBind();
            }
        }

        protected void rptJobTransfer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            result_info_job_zone item = (result_info_job_zone)e.Item.DataItem;


            Label lblNo = (Label)e.Item.FindControl("lblNo");
            Label lblJobId = (Label)e.Item.FindControl("lblJobId");
            Label lblProductId = (Label)e.Item.FindControl("lblProductId");
            Label lblProductName = (Label)e.Item.FindControl("lblProductName");
            Label lblAmount = (Label)e.Item.FindControl("lblAmount");
            TextBox txtComment = (TextBox)e.Item.FindControl("txtComment");
            LinkButton lbnDelete = (LinkButton)e.Item.FindControl("lbnDelete");


            lblNo.Text = item.seq.ToString();
            lblJobId.Text = item.job_id.ToString();
            lblProductId.Text = item.product_id.ToString();
            lblProductName.Text = item.product_name;
            lblAmount.Text = item.amount.ToString("#.##");
            txtComment.Text = item.comment;

            //if (zone.zone_id != 0)
            //{
            //    lbnDelete.Enabled = false;
            //    lbnDelete.Attributes.Add("Style", "cursor: not-allowed");
            //}

            lbnDelete.CommandName = "Delete";
            lbnDelete.CommandArgument = item.job_id.ToString();
            if (item.is_deleted.Value)
            {
                e.Item.Visible = false;
            }

        }

        protected void rptJobTransfer_ItemDataBound(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                e.Item.Visible = false;
                var res = PrepareDataBeforeToRepeater();
                setDataToRepeater(res);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "InitSelect2();", true);
            }
        }
        protected void lbnSave_Click(object sender, EventArgs e)
        {
            var message = "";
            DataService dataService = new DataService();
            List<param_create_job> param = new List<param_create_job>();
            UtilityCommon utilityCommon = new UtilityCommon();
            DateTime _now = DateTime.Now;
            var user = userLogin();
            var zoneId = GetIdFromQueryString();
            param = GetDataJob(zoneId: zoneId);

            int success = 0;
            success = dataService.UpdateJob(param);
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

        public List<param_create_job> GetDataJob(int zoneId)
        {
            var user = userLogin();
            List<param_create_job> job = new List<param_create_job>();

            foreach (RepeaterItem item in rptJobTransfer.Items)
            {
                Label lblJobId = (Label)item.FindControl("lblJobId");
                Label lblProductId = (Label)item.FindControl("lblProductId");
                Label lblProductName = (Label)item.FindControl("lblProductName");
                Label lblAmount = (Label)item.FindControl("lblAmount");
                TextBox txtComment = (TextBox)item.FindControl("txtComment");

                job.Add(new param_create_job
                {
                    job_id = int.Parse(lblJobId.Text),
                    zone_id = zoneId,
                    product_id = int.Parse(lblProductId.Text),
                    product_name = lblProductName.Text,
                    amount = int.Parse(lblAmount.Text),
                    status_id = (int)_BaseConst.status_job.Transfer_processing,
                    comment = txtComment.Text,
                    created_by = user.user_id,
                    modified_by = user.user_id,
                    is_active = true,
                    is_deleted = !item.Visible
                });
            };

            return job;
        }
        public bool ValidateForm(out string message)
        {
            DataService dataService = new DataService();
            message = "";

            if (ddlProduct.SelectedValue == "0")
            {
                message = "กรุณาเลือกสินค้า (product)";
                return false;
            }


            if (!int.TryParse(txtAmount.Text, out int a))
            {
                message = "กรุณากรอกจำนวนเป็นตัวเลขเท่านั้น (Amount Only Pls.)";
                return false;
            }

            return true;
        }
        protected void lbnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.JobTransferListUrl, false);
        }
        protected void lblSuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.JobTransferListUrl, false);
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