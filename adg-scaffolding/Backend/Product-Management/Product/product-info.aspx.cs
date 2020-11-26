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

namespace adg_scaffolding.Backend.Product_Management.Product
{
    public partial class product_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var productId = GetIdFromQueryString();
                SetDropdownlist();
                SetDataToUIByID(productId);
            }
        }

        public void SetDropdownlist()
        {
            Dropdown dropdown = new Dropdown();
            ddlSpecification = dropdown.GetDropdownSpecification(ddlSpecification);
        }

        protected void ddlSpecification_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataService dataService = new DataService();
            result_info_specification specification = new result_info_specification();
            List<result_info_product_specification> productSpecification = new List<result_info_product_specification>();
            var specificationId = int.Parse(ddlSpecification.SelectedValue);
            if (specificationId != 0)
            {
                specification = dataService.GetSpecificationInfo(specificationId);
                specification.sub_specifications.Where(i => i.is_active == true).ToList().ForEach(i =>
                {
                    var subSpecification = new result_info_product_specification();
                    subSpecification.sub_specification_id = i.sub_specification_id;
                    subSpecification.product_specification_name = i.sub_specification_name;
                    productSpecification.Add(subSpecification);
                });
            }
            setDataToRepeater(productSpecification);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "InitSelect2();", true);
        }
        public result_info_product GetDataProduct(Int32 productId)
        {
            DataService daataService = new DataService();
            result_info_product product = new result_info_product();
            product = daataService.GetProductInfo(productId);
            return product;
        }
        public void SetDataToUIByID(Int32 ID)
        {
            chkStatus.Checked = true;
            var product = GetDataProduct(ID);
            if (product != null)
            {
                txtProductCode.Text = product.product_code;
                txtProductName.Text = product.product_name;
                txtStock.Text = product.stock_qty.ToString();
                ddlSpecification.SelectedValue = product.specification_id.ToString();
                txtCostPrice.Text = product.product_cost_price.ToString("#.##");
                txtSalePrice.Text = product.product_sale_price.ToString("#.##");
                txtProductDescription.Text = product.product_description;
                txtComment.Text = product.comment;
                chkStatus.Checked = ID != 0 ? product.is_active.Value : true;
                setDataToRepeater(product.product_specifications);
            }
        }
        public void setDataToRepeater(List<result_info_product_specification> productSpecification)
        {
            rptProductSpecification.DataSource = null;
            if (productSpecification != null && productSpecification.Count() > 0)
            {
                var seq = 1;
                productSpecification.ForEach(i =>
                {
                    i.seq = seq;
                    seq += 1;
                });
                rptProductSpecification.DataSource = productSpecification;
            }
            rptProductSpecification.DataBind();
        }

        protected void rptProductSpecification_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            result_info_product_specification item = (result_info_product_specification)e.Item.DataItem;
            Label lblProductSpecificationId = (Label)e.Item.FindControl("lblProductSpecificationId");
            Label lblNo = (Label)e.Item.FindControl("lblNo");
            Label lblSubSpecificationId = (Label)e.Item.FindControl("lblSubSpecificationId");
            Label lblProductSpecificationName = (Label)e.Item.FindControl("lblProductSpecificationName");
            TextBox txtProductSpecificationValue = (TextBox)e.Item.FindControl("txtProductSpecificationValue");
            HtmlInputCheckBox chkStatus = (HtmlInputCheckBox)e.Item.FindControl("chkStatus");
            HiddenField hdfStatus = (HiddenField)e.Item.FindControl("hdfStatus");

            lblNo.Text = item.seq.ToString();
            lblProductSpecificationId.Text = item.product_specification_id.ToString();
            lblSubSpecificationId.Text = item.sub_specification_id.ToString();
            lblProductSpecificationName.Text = item.product_specification_name;
            txtProductSpecificationValue.Text = item.product_specification_value;
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

                DataService dataService = new DataService();
                param_create_product product = new param_create_product();
                product.product_specifications = new List<param_create_product_specification>();

                var productId = GetIdFromQueryString();
                product.product_id = productId;
                product.product_code = txtProductCode.Text;
                product.product_name = txtProductName.Text;
                product.stock_qty = int.Parse(txtStock.Text);
                product.specification_id = int.Parse(ddlSpecification.SelectedValue);
                product.product_cost_price = decimal.Parse(txtCostPrice.Text);
                product.product_sale_price = decimal.Parse(txtSalePrice.Text);
                product.product_description = txtProductDescription.Text;
                product.comment = txtComment.Text;
                product.is_referred = false;
                product.is_active = chkStatus.Checked;
                product.is_deleted = false;
                product.created_by = user.user_id;
                product.modified_by = user.user_id;
                product.product_specifications = GetDataProductSpecification();

                if (productId > 0)
                {
                    success = dataService.UpdateProduct(product);
                }
                else
                {
                    success = dataService.InsertProduct(product);
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
            DataService dataService = new DataService();
            var productList = dataService.GetProductList();
            message = "";

            if (productList != null && productList.Count > 0)
            {
                if (string.IsNullOrEmpty(txtProductCode.Text.Trim()))
                {
                    txtProductCode.Focus();
                    message = "กรุณากรอก รหัสสินค้า (Product Code)";
                    return false;
                }
                if (string.IsNullOrEmpty(txtProductName.Text.Trim()))
                {
                    txtProductName.Focus();
                    message = "กรุณากรอก ชื่อสินค้า (Product Name)";
                    return false;
                }

                var productId = GetIdFromQueryString();
                productList = productList.Where(i => i.product_id != productId).ToList();
                if (productList.Any(i => i.product_code.Trim().Equals(txtProductCode.Text.Trim())))
                {
                    message = "รหัสสินค้า (Product Code) นี้มีอยู่ในระบบแล้ว";
                    return false;
                }

                if (productList.Any(i => i.product_name.Trim().Equals(txtProductName.Text.Trim())))
                {
                    message = "ชื่อสินค้า (Product Name) นี้มีอยู่ในระบบแล้ว";
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtStock.Text.Trim()))
            {
                txtStock.Focus();
                message = "กรุณากรอก จำนวนสินค้าคงค้าง (Stock)";
                return false;
            }
            if (ddlSpecification.SelectedValue == "0")
            {
                ddlSpecification.Focus();
                message = "กรุณาเลือก ลักษณะเฉพาะสินค้า (Specification) ";
                return false;
            }
            if (string.IsNullOrEmpty(txtCostPrice.Text.Trim()))
            {
                txtCostPrice.Focus();
                message = "กรุณากรอก ต้นทุนสินค้า (Cost Price)";
                return false;
            }
            if (string.IsNullOrEmpty(txtSalePrice.Text.Trim()))
            {
                txtSalePrice.Focus();
                message = "กรุณากรอก ราคาขายสินค้า (Sale Price)";
                return false;
            }

            return true;
        }
        public List<param_create_product_specification> GetDataProductSpecification()
        {
            var user = userLogin();
            List<param_create_product_specification> productSpecifications = new List<param_create_product_specification>();

            foreach (RepeaterItem item in rptProductSpecification.Items)
            {
                Label lblProductSpecificationId = (Label)item.FindControl("lblProductSpecificationId");
                Label lblSubSpecificationId = (Label)item.FindControl("lblSubSpecificationId");
                Label lblProductSpecificationName = (Label)item.FindControl("lblProductSpecificationName");
                TextBox txtProductSpecificationValue = (TextBox)item.FindControl("txtProductSpecificationValue");
                HtmlInputCheckBox chkStatus = (HtmlInputCheckBox)item.FindControl("chkStatus");
                HiddenField hdfStatus = (HiddenField)item.FindControl("hdfStatus");

                productSpecifications.Add(new param_create_product_specification
                {
                    product_specification_id = int.Parse(lblProductSpecificationId.Text),
                    sub_specification_id = int.Parse(lblSubSpecificationId.Text),
                    sub_specification_value = txtProductSpecificationValue.Text,
                    is_active = chkStatus.Checked,
                    created_by = user.user_id,
                    modified_by = user.user_id,
                });
            }

            return productSpecifications;
        }
        protected void lbnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.ProductListUrl, false);
        }
        protected void lblSuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.ProductListUrl, false);
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