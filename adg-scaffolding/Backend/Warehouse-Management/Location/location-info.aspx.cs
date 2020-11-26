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

namespace adg_scaffolding.Backend.Warehouse_Management.Location
{
    public partial class location_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var locationId = GetIdFromQueryString();
                SetDropdownList();
                setDataToUIByID(locationId);
            }
        }
        public void SetDropdownList()
        {
            Dropdown dropdown = new Dropdown();
            ddlWarehouse = dropdown.GetDropdownWarehouse(ddlWarehouse);
        }

        public result_info_location GetDatalocation(Int32 locationId)
        {
            DataService DataService = new DataService();
            result_info_location location = new result_info_location();
            location = DataService.GetLocationInfo(locationId);

            return location;
        }
        public void setDataToUIByID(Int32 ID)
        {
            chkStatus.Checked = true;
            var location = GetDatalocation(ID);
            if (location != null)
            {
                ddlWarehouse.SelectedValue = location.warehouse_id.ToString();
                txtLocationCode.Text = location.location_code;
                txtLocationName.Text = location.location_name;
                txtComment.Text = location.comment;
                chkStatus.Checked = ID != 0 ? location.is_active.Value : true;
                setDataToRepeater(location.zone);
            }
        }

        protected void btnCreateZone_Click(object sender, EventArgs e)
        {
            var res = new List<result_info_zone>();
            var zoneList = GetDataZone();
            zoneList.ForEach(i =>
            {
                var zone = new result_info_zone();
                zone.zone_id = i.zone_id;
                zone.zone_code = i.zone_code;
                zone.zone_name = i.zone_name;
                zone.is_active = i.is_active;
                res.Add(zone);
            });
            res.Add(new result_info_zone());
            setDataToRepeater(res);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "InitSelect2();", true);
        }

        public void setDataToRepeater(List<result_info_zone> zone)
        {
            rptZone.DataSource = null;
            if (zone != null && zone.Count() > 0)
            {
                var seq = 1;
                zone.ForEach(i =>
                {
                    i.seq = seq;
                    seq += 1;
                });
                rptZone.DataSource = zone.OrderBy(i => i.seq);
                rptZone.DataBind();
            }
        }

        protected void rptZone_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            result_info_zone zone = (result_info_zone)e.Item.DataItem;

            Label lblNo = (Label)e.Item.FindControl("lblNo");
            Label lblZoneId = (Label)e.Item.FindControl("lblZoneId");
            TextBox txtZoneCode = (TextBox)e.Item.FindControl("txtZoneCode");
            TextBox txtZoneName = (TextBox)e.Item.FindControl("txtZoneName");
            TextBox txtComment = (TextBox)e.Item.FindControl("txtComment");
            HtmlInputCheckBox chkStatus = (HtmlInputCheckBox)e.Item.FindControl("chkStatus");
            HiddenField hdfStatus = (HiddenField)e.Item.FindControl("hdfStatus");
            LinkButton lbnDelete = (LinkButton)e.Item.FindControl("lbnDelete");

            lblNo.Text = zone.seq.ToString();
            lblZoneId.Text = zone.zone_id.ToString();
            txtZoneCode.Text = zone.zone_code;
            txtZoneName.Text = zone.zone_name;
            txtComment.Text = zone.comment;
            hdfStatus.Value = zone.is_active.ToString();

            zone.is_active = zone.is_active != null ? zone.is_active : false;
            if (zone.is_active == true)
            {
                chkStatus.Attributes.Add("checked", "checked");
            }
            else if (zone.is_active == false)
            {
                chkStatus.Attributes.Remove("checked");
            }

            if (zone.zone_id != 0)
            {
                lbnDelete.Enabled = false;
                lbnDelete.Attributes.Add("Style", "cursor: not-allowed");
            }

            lbnDelete.CommandName = "Delete";
            lbnDelete.CommandArgument = zone.zone_id.ToString();
        }

        protected void rptZone_ItemDataBound(object source, RepeaterCommandEventArgs e)
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
                param_create_location location = new param_create_location();
                location.zone = new List<param_create_zone>();

                var locationId = GetIdFromQueryString();
                location.location_id = locationId;
                location.warehouse_id = int.Parse(ddlWarehouse.SelectedValue);
                location.location_code = txtLocationCode.Text;
                location.location_name = txtLocationName.Text;
                location.comment = txtComment.Text;
                location.is_referred = false;
                location.is_active = chkStatus.Checked;
                location.is_deleted = false;
                location.created_by = user.user_id;
                location.modified_by = user.user_id;
                location.zone = GetDataZone();

                if (locationId > 0)
                {
                    success = DataService.UpdateLocation(location);
                }
                else
                {
                    success = DataService.InsertLocation(location);
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
            var locationList = DataService.GetLocationList();
            message = "";

            if (ddlWarehouse.SelectedValue == "0")
            {
                ddlWarehouse.Focus();
                message = "กรุณาเลือก คลังสินค้า (Warehouse)";
                return false;
            }

            if (string.IsNullOrEmpty(txtLocationName.Text.Trim()))
            {
                txtLocationName.Focus();
                message = "กรุณากรอก พื้นที่การทำงาน (Location Name)";
                return false;
            }
            if (string.IsNullOrEmpty(txtLocationCode.Text.Trim()))
            {
                txtLocationCode.Focus();
                message = "กรุณากรอก รหัสพื้นที่การทำงาน (Location Code)";
                return false;
            }

            if (locationList != null && locationList.Count > 0)
            {
                var locationId = GetIdFromQueryString();
                locationList = locationList.Where(i => i.location_id != locationId).ToList();
                if (locationList.Any(i => i.location_code.Trim().Equals(txtLocationCode.Text.Trim())))
                {
                    message = "รหัสพื้นที่การทำงาน (Location Code) นี้มีอยู่ในระบบแล้ว”";
                    return false;
                }

                if (locationList.Any(i => i.location_name.Trim().Equals(txtLocationName.Text.Trim())))
                {
                    message = "พื้นที่การทำงาน (Location Name) นี้มีอยู่ในระบบแล้ว”";
                    return false;
                }
            }



            return true;
        }
        public List<param_create_zone> GetDataZone()
        {
            var user = userLogin();
            List<param_create_zone> zone = new List<param_create_zone>();

            foreach (RepeaterItem item in rptZone.Items)
            {
                Label lblZoneId = (Label)item.FindControl("lblZoneId");
                TextBox txtZoneCode = (TextBox)item.FindControl("txtZoneCode");
                TextBox txtZoneName = (TextBox)item.FindControl("txtZoneName");
                TextBox txtComment = (TextBox)item.FindControl("txtComment");
                HtmlInputCheckBox chkStatus = (HtmlInputCheckBox)item.FindControl("chkStatus");
                HiddenField hdfStatus = (HiddenField)item.FindControl("hdfStatus");

                zone.Add(new param_create_zone
                {
                    zone_id = int.Parse(lblZoneId.Text),
                    zone_code = txtZoneCode.Text,
                    zone_name = txtZoneName.Text,
                    created_by = user.user_id,
                    modified_by = user.user_id,
                    is_active = chkStatus.Checked,
                    flag_delete = !item.Visible
                }); 
            };

            return zone;
        }
        protected void lbnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.LocationListUrl, false);
        }
        protected void lblSuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.LocationListUrl, false);
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