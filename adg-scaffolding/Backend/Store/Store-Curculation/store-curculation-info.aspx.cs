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

namespace adg_scaffolding.Backend.Store.Store_Curculation
{
    public partial class store_curculation_info : System.Web.UI.Page
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
            ddlStore = dropdown.GetDropdownStore(ddlStore);
        }

        public void setDataToUIByID(Int32 ID)
        {
            DataService dataService = new DataService();
            result_info_store_curculation storeCurculation = new result_info_store_curculation();
            UtilityCommon utilityCommon = new UtilityCommon();
            if (ID > 0)
            {
                storeCurculation = dataService.GetStoreCurculationInfo(storeCurculationId: ID);
                if (storeCurculation != null)
                {
                    txtStoreCurculationNo.Text = storeCurculation.curculation_no;
                    txtLoanerDate.Text = storeCurculation.loaner_date.ToString();
                    txtLoanerName.Text = storeCurculation.loaner_name;
                    txtReturnDate.Text = storeCurculation.return_date.ToString();
                    txtReturnName.Text = storeCurculation.return_name;
                    setDataToRepeater(storeCurculation.store_curculation_item);
                }
            }
        }
        protected void btnCreatestoreCurculation_Click(object sender, EventArgs e)
        {
            string message = "";
            if (!ValidateForm(out message))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "openModalWaring('" + message + "');", true);
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script2", "InitSelect2();", true);
                return;
            }

            var res = PrepareDataBeforeToRepeater();

            var newSelectStoreId = int.Parse(ddlStore.SelectedValue);
            var newSelectAmount = int.Parse(txtAmount.Text);
            var resNewItem = new List<result_info_store_curculation_item>();
            if (res.Where(s => s.is_deleted == false).Count() > 0)
            {
                res.ForEach(i =>
                {
                    if (i.store_id == newSelectStoreId)
                    {
                        i.qty_loaner += newSelectAmount;
                    }
                    else
                    {
                        resNewItem.Add(new result_info_store_curculation_item
                        {
                            store_curculation_item_id = 0,
                            store_id = newSelectStoreId,
                            qty_loaner = newSelectAmount,
                            is_deleted = false
                        });
                    }
                });
            }
            else
            {
                resNewItem.Add(new result_info_store_curculation_item
                {
                    store_curculation_item_id = 0,
                    store_id = newSelectStoreId,
                    qty_loaner = newSelectAmount,
                    is_deleted = false
                });
            }
            res.AddRange(resNewItem);
            setDataToRepeater(res);

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "InitSelect2();", true);
        }

        public List<result_info_store_curculation_item> PrepareDataBeforeToRepeater()
        {
            var res = new List<result_info_store_curculation_item>();
            var storeCurculationId = GetIdFromQueryString();
            var storeCurculationItemList = GetDatastoreCurculationItem(storeCurculationId : storeCurculationId);
            storeCurculationItemList.ForEach(i =>
            {
                var storeCurculation = new result_info_store_curculation_item();
                storeCurculation.store_curculation_item_id = i.store_curculation_item_id;
                storeCurculation.store_id = i.store_id;
                storeCurculation.qty_loaner = i.qty_loaner;
                storeCurculation.qty_return = i.qty_return;
                storeCurculation.comment = i.comment;
                storeCurculation.is_deleted = i.is_deleted;
                res.Add(storeCurculation);
            });

            return res;
        }

        public void setDataToRepeater(List<result_info_store_curculation_item> item)
        {
            rptStoreCurculationItem.DataSource = null;
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
                rptStoreCurculationItem.DataSource = item.OrderBy(i => i.seq);
                rptStoreCurculationItem.DataBind();
            }
        }

        protected void rptstoreCurculationItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            result_info_store_curculation_item item = (result_info_store_curculation_item)e.Item.DataItem;


            Label lblNo = (Label)e.Item.FindControl("lblNo");
            Label lblStoreCurculationItemId = (Label)e.Item.FindControl("lblStoreCurculationItemId");
            Label lblStoreId = (Label)e.Item.FindControl("lblStoreId");
            Label lblProducStoretName = (Label)e.Item.FindControl("lblProducStoretName");
            Label lblQtyLoaner = (Label)e.Item.FindControl("lblQtyLoaner");
            Label lblQtyReturn = (Label)e.Item.FindControl("lblQtyReturn");
            TextBox txtComment = (TextBox)e.Item.FindControl("txtComment");
            LinkButton lbnDelete = (LinkButton)e.Item.FindControl("lbnDelete");


            lblNo.Text = item.seq.ToString();
            lblStoreCurculationItemId.Text = item.store_curculation_item_id.ToString();
            lblStoreId.Text = item.store_id.ToString();
            lblProducStoretName.Text = item.product_store_name;
            lblQtyLoaner.Text = item.qty_loaner.ToString("#.##");
            lblQtyReturn.Text = item.qty_return.ToString("#.##");
            txtComment.Text = item.comment;

            //if (zone.zone_id != 0)
            //{
            //    lbnDelete.Enabled = false;
            //    lbnDelete.Attributes.Add("Style", "cursor: not-allowed");
            //}

            lbnDelete.CommandName = "Delete";
            lbnDelete.CommandArgument = item.store_curculation_item_id.ToString();
            if (item.is_deleted.Value)
            {
                e.Item.Visible = false;
            }

        }

        protected void rptstoreCurculationItem_ItemDataBound(object source, RepeaterCommandEventArgs e)
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
            param_create_store_curculation param = new param_create_store_curculation();
            param.store_curculation_item = new List<param_create_store_curculation_item>();
            UtilityCommon utilityCommon = new UtilityCommon();
            DateTime _now = DateTime.Now;
            var user = userLogin();
            var storeCurculationId = GetIdFromQueryString();
            param.store_curculation_item = GetDatastoreCurculationItem(storeCurculationId: storeCurculationId);

            int success = 0;
            success = dataService.UpdateStoreCurculation(param);
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

        public List<param_create_store_curculation_item> GetDatastoreCurculationItem(int storeCurculationId)
        {
            var user = userLogin();
            List<param_create_store_curculation_item> storeCurculation = new List<param_create_store_curculation_item>();

            foreach (RepeaterItem item in rptStoreCurculationItem.Items)
            {
                Label lblStoreCurculationItemId = (Label)item.FindControl("lblStoreCurculationItemId");
                Label lblStoreId = (Label)item.FindControl("lblStoreId");
                Label lblProducStoretName = (Label)item.FindControl("lblProducStoretName");
                Label lblQtyLoaner = (Label)item.FindControl("lblQtyLoaner");
                Label lblQtyReturn = (Label)item.FindControl("lblQtyReturn");
                TextBox txtComment = (TextBox)item.FindControl("txtComment");

                storeCurculation.Add(new param_create_store_curculation_item
                {
                    store_curculation_item_id = int.Parse(lblStoreCurculationItemId.Text),
                    store_curculation_id = storeCurculationId,
                    store_id = int.Parse(lblStoreId.Text),
                    product_store_name = lblProducStoretName.Text,
                    qty_loaner = int.Parse(lblQtyLoaner.Text),
                    qty_return = int.Parse(lblQtyReturn.Text),
                    comment = txtComment.Text,
                    created_by = user.user_id,
                    modified_by = user.user_id,
                    is_active = true,
                    is_deleted = !item.Visible
                });
            };

            return storeCurculation;
        }
        public bool ValidateForm(out string message)
        {
            DataService dataService = new DataService();
            message = "";

            if (ddlStore.SelectedValue == "0")
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
            Response.Redirect(StaticUrl.StoreCurculationListUrl, false);
        }
        protected void lblSuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect(StaticUrl.StoreCurculationListUrl, false);
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