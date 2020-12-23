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

namespace adg_scaffolding.Backend.Job_Management.Job
{
    public partial class job_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var zoneId = GetIdFromQueryString();
                SetDropdownList();
                setDataToUIByID(zoneId);
            }
        }

        public void SetDropdownList()
        {
            Dropdown dropdown = new Dropdown();
            ddlProduct = dropdown.GetDropdownProduct(ddlProduct);
        }

        public void setDataToUIByID(Int32 ID)
        {
            DataService dataService = new DataService();
            result_info_job_zone job = new result_info_job_zone();
            UtilityCommon utilityCommon = new UtilityCommon();
            if (ID > 0)
            {
                var statusId = (int)_BaseConst.status_job.cleaning_processing;
                job = dataService.GetJobZoneInfo(zoneId: ID, statusId: statusId);
                if (job != null)
                {
                    txtLocationName.Text = job.location_name;
                    txtZoneName.Text = job.zone_name;
                    setDataToRepeater(job.items);
                }
            }
        }

        protected void btnCreateJob_Click(object sender, EventArgs e)
        {
            string message = "";
            if (!ValidateForm(out message))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "openModalWaring('" + message + "');", true);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script2", "InitSelect2();", true);
                return;
            }

            var res = PrepareDataBeforeToRepeater();

            var newSelectProductId = int.Parse(ddlProduct.SelectedValue);
            var newSelectProductAmount = int.Parse(txtAmount.Text);
            var resNewItem = new List<result_info_job_zone_item>();
            if (res.Where(s => s.is_deleted == false).Count() > 0)
            {
                res.ForEach(i =>
                {
                    if (i.product_id == newSelectProductId)
                    {
                        i.amount += newSelectProductAmount;
                    }
                    else
                    {
                        resNewItem.Add(new result_info_job_zone_item
                        {
                            job_id = 0,
                            product_id = int.Parse(ddlProduct.SelectedValue),
                            product_name = ddlProduct.SelectedItem.ToString(),
                            amount = int.Parse(txtAmount.Text),
                            is_deleted = false
                        });
                    }
                });
            }
            else
            {
                resNewItem.Add(new result_info_job_zone_item
                {
                    job_id = 0,
                    product_id = int.Parse(ddlProduct.SelectedValue),
                    product_name = ddlProduct.SelectedItem.ToString(),
                    amount = int.Parse(txtAmount.Text),
                    is_deleted = false
                });
            }
            res.AddRange(resNewItem);
            setDataToRepeater(res);

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "InitSelect2();", true);
        }

        public List<result_info_job_zone_item> PrepareDataBeforeToRepeater()
        {
            var res = new List<result_info_job_zone_item>();
            var zoneId = GetIdFromQueryString();
            var jobList = GetDataJob(zoneId);
            jobList.ForEach(i =>
            {
                var job = new result_info_job_zone_item();
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

        public void setDataToRepeater(List<result_info_job_zone_item> item)
        {
            rptJob.DataSource = null;
            if (item != null && item.Count() > 0)
            {
                var seq = 1;
                item.ForEach(i =>
                {
                    if (!i.is_deleted.Value)
                    {
                        i.seq = seq;
                        seq += 1;
                    }

                });
                rptJob.DataSource = item.OrderBy(i => i.seq);
                rptJob.DataBind();
            }
        }

        protected void rptJob_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            result_info_job_zone_item item = (result_info_job_zone_item)e.Item.DataItem;


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

        protected void rptJob_ItemDataBound(object source, RepeaterCommandEventArgs e)
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

            foreach (RepeaterItem item in rptJob.Items)
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
                    status_id = (int)_BaseConst.status_job.cleaning_processing,
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
            Response.Redirect(StaticUrl.JobListUrl, false);
        }
        protected void lblSuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.JobListUrl, false);
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