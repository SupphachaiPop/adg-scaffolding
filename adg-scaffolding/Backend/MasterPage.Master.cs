using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Definitions;
using Entity;
using Service;

namespace adg_scaffolding
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public result_user_login UserLogin
        {
            get
            {

                if (Session["userLoginBackend"] == null)
                {
                    return null;
                }
                else
                {
                    return ((result_user_login)Session["userLoginBackend"]);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (Session["userLoginBackend"] != null)
                {
                    lblUsername.Text = UserLogin.username.ToString();
                    setUserMenu();
                }
                else
                {
                    Response.Redirect("/Backend/login.aspx");
                }

            }
        }

        public result_user_login GetLogin()
        {
            result_user_login userLogin = new result_user_login();
            userLogin = (result_user_login)Session["userLoginBackend"];
            return userLogin;
        }
        private void setUserMenu()
        {
            result_user_login userLogin = GetLogin();
            List<result_user_login_menu> masterMenu = new List<result_user_login_menu>();

            masterMenu = userLogin.user_menu.Where(i => i.parent_menu_id == null).OrderBy(o => o.seq).ToList();
            masterMenu.ForEach(i =>
            {
                i.childmenu = userLogin.user_menu.Where(o => o.parent_menu_id == i.menu_id).OrderBy(o => o.seq).ToList();
            });
            rptMenu.DataSource = masterMenu;
            rptMenu.DataBind();
        }
        protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            result_user_login_menu item = (result_user_login_menu)e.Item.DataItem;

            HtmlGenericControl liParent = (HtmlGenericControl)e.Item.FindControl("liParent");
            HtmlAnchor parentUrlMenu = (HtmlAnchor)e.Item.FindControl("parentUrlMenu");
            HtmlGenericControl parentLabel = (HtmlGenericControl)e.Item.FindControl("parentLabel");
            Repeater rptChildMenu = (Repeater)e.Item.FindControl("rptChildMenu");

            parentUrlMenu.HRef = string.IsNullOrEmpty(item.menu_url) ? StaticUrl.DEFAULT_EMPTY_URL : item.menu_url.Trim();
            parentLabel.InnerHtml = item.menu_name;

            // child menu
            rptChildMenu.DataSource = item.childmenu;
            rptChildMenu.DataBind();

        }
        protected void rptChildMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            result_user_login_menu item = (result_user_login_menu)e.Item.DataItem;
            //HtmlGenericControl uiChild = (HtmlGenericControl)e.Item.FindControl("uiChild");
            HtmlAnchor childUrlMenu = (HtmlAnchor)e.Item.FindControl("childUrlMenu");
            HtmlGenericControl childLabel = (HtmlGenericControl)e.Item.FindControl("childLabel");

            childUrlMenu.HRef = item.menu_url.Trim();
            childLabel.InnerHtml = item.menu_name;
        }
        protected void log_out_Click(object sender, EventArgs e)
        {
            clearSession();
            clearCacheAfterLogout();
            Response.Redirect("/Backend/login.aspx");
        }

        private void clearCacheAfterLogout()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        private void clearSession()
        {
            Session["userLoginBackend"] = null;
        }
    }
}