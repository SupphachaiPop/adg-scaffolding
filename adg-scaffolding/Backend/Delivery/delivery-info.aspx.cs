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

namespace adg_scaffolding.Backend.Delivery
{
    public partial class delivery_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var deliveryId = GetIdFromQueryString();
                setDataToUIByID(deliveryId);
            }
        }

        public void setDataToUIByID(Int32 ID)
        {
            DataService dataService = new DataService();
            result_info_delivery delivery = new result_info_delivery();
            UtilityCommon utilityCommon = new UtilityCommon();
            chkStatus.Checked = true;
            if (ID > 0)
            {
                delivery = dataService.GetDeliveryInfo(ID);
                if (delivery != null)
                {
                    lblDeliveryId.Text = ID.ToString();
                    txtDeliveryName.Text = delivery.delivery_name;
                    txtDeliveryCode.Text = delivery.delivery_code;
                    txtTaxNo.Text = delivery.tax_no;
                    txtAddress.Text = delivery.address;
                    txtPhone.Text = delivery.phone;
                    txtEmail.Text = delivery.email;
                    txtComment.Text = delivery.comment;
                    chkStatus.Checked = ID != 0 ? delivery.is_active.Value : true;
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
            param_create_delivery param = new param_create_delivery();
            UtilityCommon utilityCommon = new UtilityCommon();
            DateTime _now = DateTime.Now;
            var user = userLogin();
            var deliveryId = GetIdFromQueryString();
            param.delivery_id = deliveryId;
            param.delivery_name = txtDeliveryName.Text;
            param.delivery_code = txtDeliveryCode.Text;
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
            if (deliveryId > 0)
            {
                success = dataService.UpdateDelivery(param);
            }
            else
            {
                success = dataService.InsertDelivery(param);
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
            List<delivery> deliveryList = dataService.GetDeliveryList();
            message = "";

            if (string.IsNullOrEmpty(txtDeliveryCode.Text.Trim()))
            {
                message = "กรุณากรอก รหัสบริษัทขนส่ง (delivery Code)";
                return false;
            }
            if (string.IsNullOrEmpty(txtDeliveryName.Text.Trim()))
            {
                message = "กรุณากรอก ชื่อบริษัทขนส่ง (delivery Name)";
                return false;
            }

            if (deliveryList != null && deliveryList.Count > 0)
            {
                int deliveryId = GetIdFromQueryString();
                deliveryList = deliveryList.Where(i => i.delivery_id != deliveryId).ToList();
                if (deliveryList.Any(i => i.delivery_name.Trim().Equals(txtDeliveryName.Text.Trim())))
                {
                    message = "ชื่อลูกบริษัทขนส่ง (delivery Name) นี้มีอยู่ในระบบแล้ว";
                    return false;
                }

                if (deliveryList.Any(i => i.delivery_code.Trim().Equals(txtDeliveryCode.Text.Trim())))
                {
                    message = "รหัสบริษัทขนส่ง (delivery Code) นี้มีอยู่ในระบบแล้ว";
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
            Response.Redirect(StaticUrl.DeliveryListUrl, false);
        }
        protected void lblSuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.DeliveryListUrl, false);
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