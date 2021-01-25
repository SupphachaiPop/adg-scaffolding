
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace adg_scaffolding.Backend.Print
{
    public partial class print_quotation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    Session["quotationID"] = null;
            //    UserEntity userEntity = (UserEntity)Session["useradmin"];

            //    if (userEntity == null) Response.Redirect("/login.aspx");


            //    hdfStoreID.Value = userEntity.store_id.ToString();
            //    hdfUserID.Value = userEntity.user_id.ToString();

            //    if (Request.QueryString["quotationID"] != null)
            //    {
            //        hddfQuotationID.Value = Request.QueryString["quotationID"].ToString();
            //        setInquiryForPrint();
            //        CreateQRCode(Request.QueryString["quotationID"].ToString());
            //    }
            //}

        }

        //private void CreateQRCode(string inqId)
        //{
        //    Uri myuri = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        //    string pathQuery = myuri.PathAndQuery;
        //    string hostName = myuri.ToString().Replace(pathQuery, "");
        //    String url = hostName + "/Backoffice/Inquiry/Inquiry-job/inquiry-job-list.aspx?JobInquiryNo=" + inqId;
        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
        //    QRCode qrCode = new QRCode(qrCodeData);
        //    Bitmap qrCodeImage = qrCode.GetGraphic(20);

        //    System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        //    imgBarCode.Height = 150;
        //    imgBarCode.Width = 150;
        //    using (Bitmap bitMap = qrCodeImage)
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //            byte[] byteImage = ms.ToArray();
        //            imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
        //        }
        //        // plBarCode.Controls.Add(imgBarCode);
        //    }
        //}

        //private void setInquiryForPrint()
        //{
        //    Inquiry_QuotationtService Inquiry_QuotationtService = new Inquiry_QuotationtService();
        //    UserService userService = new UserService();
        //    InquiryQuotationEntity InquiryQuotationEntity = new InquiryQuotationEntity();
        //    Inquiry_QSparepartService Inquiry_QSparepartService = new Inquiry_QSparepartService();
        //    InquiryQuotationEntity = Inquiry_QuotationtService.GetDataByID(Int32.Parse(hddfQuotationID.Value));

        //    List<Inquiry_Quotation_partEntity> listSparePart = Inquiry_QSparepartService.GetDataByInquiryID(InquiryQuotationEntity.quotation_id);


        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.document_no)) InquiryQuotationEntity.document_no = "-";
        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.job_document_no)) InquiryQuotationEntity.job_document_no = "-";
        //    //  this.txtRepairNumber.Text = InquiryQuotationEntity.document_no + " / " + InquiryQuotationEntity.job_document_no;

        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.gsx_number)) InquiryQuotationEntity.gsx_number = "-";
        //    //    this.txtGSX.Text = InquiryQuotationEntity.gsx_number;
        //    List<int> tmp = new List<int>();
        //    tmp.Add(InquiryQuotationEntity.inquiry_id);
        //    List<UserEntity> users = userService.GetDataTechniciansByInquiryIds(tmp);
        //    //if (users.Count > 0) { 
        //    //    this.txtTechnicianName.Text = users[0].fullname;
        //    //    this.txtTechnicianId.Text = users[0].technicianId != null ? users[0].technicianId : "-";
        //    //}
        //    //else
        //    //{
        //    //    this.txtTechnicianId.Text = "-";
        //    //    this.txtTechnicianName.Text = "-";
        //    //}
        //    ////diagnostic detail
        //    //this.lblCustomerComment.Text = InquiryQuotationEntity.symptom_other;
        //    //this.lblTechnicianRemark.Text = InquiryQuotationEntity.tech_remark;
        //    //if (InquiryQuotationEntity.symtom_case_id != null && InquiryQuotationEntity.symtom_case_id.Length > 0)
        //    //{
        //    //    this.lblIssue.Text = InquiryQuotationEntity.symtom_case_desc;

        //    //    this.lblIssueDetail.Text = InquiryQuotationEntity.sub_symtom_case_desc;
        //    //}
        //    //      1   ICARE - SRV   ค่าบริการ - None      900.00    No    900.00
        //    Inquiry_Quotation_partEntity serviceQuotation = new Inquiry_Quotation_partEntity();
        //    serviceQuotation.qty = 1;
        //    serviceQuotation.product_code = "ICARE - SRV";
        //    serviceQuotation.product_name = "ค่าบริการ -";

        //    serviceQuotation.price = InquiryQuotationEntity.expense_price;
        //    listSparePart.Add(serviceQuotation);


        //    this.repeaterParts.DataSource = listSparePart;

        //    this.repeaterParts.DataBind();

        //    if (InquiryQuotationEntity.document_no != null)
        //    {
        //        // txtRepairJob.ImageUrl = BarcodeGenerator.GenerateInquiryBarCode(InquiryQuotationEntity.document_no, false);
        //        //txtRepairJob2.ImageUrl = BarcodeGenerator.GenerateInquiryBarCode(InquiryQuotationEntity.document_no, false);
        //    }
        //    else
        //    {
        //        //  txtRepairJob.ImageUrl = null;
        //        //txtRepairJob2.ImageUrl = null;
        //    }


        //    txtName.Text = InquiryQuotationEntity.first_name + " " + InquiryQuotationEntity.last_name;


        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.tax_no)) InquiryQuotationEntity.tax_no = "-";
        //    // txtTaxNo.Text = InquiryQuotationEntity.tax_no;

        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.address_line1)) InquiryQuotationEntity.address_line1 = "-";
        //    //  txtAddressLine.Text = InquiryQuotationEntity.address_line1;

        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.mobile_primary)) InquiryQuotationEntity.mobile_primary = "-";
        //    txtMobile.Text = InquiryQuotationEntity.mobile_primary;
        //    //txtMobile2.Text = InquiryQuotationEntity.mobile_primary;

        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.tel)) InquiryQuotationEntity.tel = "-";
        //    //   txtTel.Text = InquiryQuotationEntity.tel;
        //    //txtTel2.Text = InquiryQuotationEntity.tel;

        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.email)) InquiryQuotationEntity.email = "-";
        //    txtEmail.Text = InquiryQuotationEntity.email;



        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.store_tel)) InquiryQuotationEntity.store_tel = "-";

        //    txtStoreTel.Text = InquiryQuotationEntity.store_tel;
        //    //txtStoreName2.Text = InquiryQuotationEntity.store_code + " " + InquiryQuotationEntity.store_name;
        //    //txtStoreTel2.Text = InquiryQuotationEntity.store_tel;

        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.document_no)) InquiryQuotationEntity.document_no = "-";
        //    //  txtAdvanceReceive.Text = InquiryQuotationEntity.document_ref_no;
        //    //txtAdvanceReceive2.Text = InquiryQuotationEntity.document_ref_no;

        //    //  txtEsitmate_price.Text = InquiryQuotationEntity.esitmate_price.ToString("N2");
        //    //txtEsitmate_price2.Text = InquiryQuotationEntity.esitmate_price.ToString("N2");



        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.product_type_name)) InquiryQuotationEntity.product_type_name = "-";
        //    //txtProductName.Text = InquiryQuotationEntity.product_type_name;
        //    //txtProductName2.Text = InquiryQuotationEntity.product_type_name;



        //    if (InquiryQuotationEntity.is_warranty == 1)
        //    {
        //        //  txtCoverage.Text = "In Warranty";
        //        //txtCoverage2.Text = "In Warranty";
        //    }
        //    else if (InquiryQuotationEntity.is_warranty == 2)
        //    {
        //        //  txtCoverage.Text = "Out Warranty";
        //        //txtCoverage2.Text = "Out Warranty";
        //    }



        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.cosmetic)) InquiryQuotationEntity.cosmetic = "-";
        //    //   txtCosmetic.Text = InquiryQuotationEntity.cosmetic;
        //    //txtCosmetic2.Text = InquiryQuotationEntity.cosmetic;


        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.symptom_other)) InquiryQuotationEntity.symptom_other = "-";

        //    if (InquiryQuotationEntity.symptom_other.Length > 100) InquiryQuotationEntity.symptom_other = InquiryQuotationEntity.symptom_other.Substring(0, 100) + "...";

        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.comment)) InquiryQuotationEntity.comment = "-";


        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.fullname)) InquiryQuotationEntity.fullname = "-";


        //    ////lblReceivedBy.Text = InquiryQuotationEntity.fullname;
        //    //txtReceivebyDate.Text = InquiryQuotationEntity.created_date.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        //    //if(InquiryQuotationEntity.end_date != null)
        //    //{
        //    //    textReturnDateHead.Text = InquiryQuotationEntity.end_date.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    //}

        //    //demo
        //    //listSparePart
        //    List<Inquiry_Quotation_partEntity> spare = listSparePart;

        //    Decimal AllPart = 0;
        //    Decimal PriceProduct = 0;
        //    foreach (var item in spare)
        //    {
        //        PriceProduct = item.price * item.qty;
        //        AllPart += PriceProduct;
        //    }


        //    Decimal percent = 100;
        //    Decimal vatPercent = 107;
        //    Decimal vat7 = percent / vatPercent;

        //    txtSubTotal.Text = AllPart.ToString("N2");
        //    txtSaleTax.Text = (AllPart * (vat7)).ToString("N2");
        //    txtServiceCharge.Text = InquiryQuotationEntity.expense_price.ToString("N2");

        //    txtTotal.Text = (AllPart + InquiryQuotationEntity.expense_price).ToString("N2");

        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.store_address)) InquiryQuotationEntity.store_address = "-";
        //    txtStoreAddress.Text = InquiryQuotationEntity.store_address;

        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.store_tel)) InquiryQuotationEntity.store_address = "-";
        //    txtStoreTel.Text = InquiryQuotationEntity.store_tel;

        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.store_email)) InquiryQuotationEntity.store_email = "-";
        //    txtStoreEmail.Text = InquiryQuotationEntity.store_email;


        //    if (InquiryQuotationEntity.document_date != null)
        //    {
        //        txtQuotationDate.Text = InquiryQuotationEntity.document_date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    }
        //    if (InquiryQuotationEntity.document_date != null)
        //    {
        //        txtQuotationValid.Text = InquiryQuotationEntity.document_date.AddMonths(1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    }


        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.quotation_no)) InquiryQuotationEntity.quotation_no = "-";
        //    txtQuotationNo.Text = InquiryQuotationEntity.quotation_no;

        //    if (InquiryQuotationEntity.customer_id != 0)
        //    {
        //        txtQuotationCustomer.Text = InquiryQuotationEntity.customer_id.ToString();
        //    }
        //    else
        //    {

        //        txtQuotationCustomer.Text = "-";
        //    }

        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.prepare_name)) InquiryQuotationEntity.prepare_name = "-";
        //    txtQuotationPrepared.Text = InquiryQuotationEntity.prepare_name;

        //    if (String.IsNullOrEmpty(InquiryQuotationEntity.tax_no)) InquiryQuotationEntity.tax_no = "-";
        //    txtTaxID.Text = InquiryQuotationEntity.tax_no;


        //    String TotalAddr = String.Empty;

        //    if (InquiryQuotationEntity.address_line2 == "-")
        //    {
        //        InquiryQuotationEntity.address_line2 = "";
        //    }


        //    if (InquiryQuotationEntity.address_line3 == "-")
        //    {
        //        InquiryQuotationEntity.address_line3 = "";
        //    }

        //    TotalAddr = InquiryQuotationEntity.address_line1 + InquiryQuotationEntity.address_line2 + InquiryQuotationEntity.address_line3;
        //    txtCustomerAddress.Text = TotalAddr;
        //}

        //protected void repeaterParts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{

        //    Inquiry_Quotation_partEntity entity = (Inquiry_Quotation_partEntity)e.Item.DataItem;
        //    Label lblProductCode = (Label)e.Item.FindControl("lblProductCode");
        //    Label lblProductName = (Label)e.Item.FindControl("lblProductName");
        //    Label lblQuantity = (Label)e.Item.FindControl("lblQuantity");
        //    Label lblUnitPrice = (Label)e.Item.FindControl("lblUnitPrice");
        //    Label lblTaxable = (Label)e.Item.FindControl("lblTaxable");
        //    Label lblAmount = (Label)e.Item.FindControl("lblAmount");


        //    lblProductCode.Text = entity.product_code;
        //    lblProductName.Text = entity.product_name;
        //    lblQuantity.Text = entity.qty.ToString();
        //    lblUnitPrice.Text = entity.price.ToString("N2");
        //    lblTaxable.Text = entity.taxable == true ? "YES" : "NO";
        //    lblAmount.Text = (entity.qty * entity.price).ToString("N2");

        //}
    }
}