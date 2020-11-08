using Definitions;
using Entity.Backend;
using Service.Backend.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adg_scaffolding.Backend.MasterData
{
    public partial class category_info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int categoryId = GetIdFromQueryString();
                setDataToUIByID(categoryId);
            }
        }

        public int GetIdFromQueryString()
        {
            UtilityCommon utilityCommon = new UtilityCommon();
            return Request.QueryString["ID"] != null ? utilityCommon.DecryptDataUrlEncoder<int>(Request.QueryString["ID"],
                                                              encryptionkey: StaticKeys.DataEncrypteKey) : 0;
        }
        public void setDataToUIByID(int categoryId)
        {
            SwCategoryEntity categoryEntity = new SwCategoryEntity();
            CategoryService categoryService = new CategoryService();
            chkStatus.Checked = true;
            if (categoryId != 0)
            {
                categoryEntity = categoryService.GetDataByID(categoryId);
                if (categoryService != null)
                {
                    txtCategoryCode.Text = categoryEntity.category_code;
                    txtCategoryName.Text = categoryEntity.category_name;
                    txtComment.Text = categoryEntity.comment;
                    lblCategoryId.Text = categoryId.ToString();
                    chkStatus.Checked = categoryId != 0 ? categoryEntity.is_active : true;
                }
            }
        }
        protected void lbnBack_Click(object sender, EventArgs e)
        {

        }

        protected void lbnSave_Click(object sender, EventArgs e)
        {

        }

        public UserEntity GetUserFromSession()
        {
            UserEntity user = new UserEntity()
                ;
            if ((HttpContext.Current.Session["userLoginBackend"] != null))
            {
                user = (UserEntity)HttpContext.Current.Session["userLoginBackend"];
            }

            return user;
        }

        protected void lbnSuccess_Click(object sender, EventArgs e)
        {

        }
    }
}