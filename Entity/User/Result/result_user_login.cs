using System.Collections.Generic;
namespace Entity
{
    public class result_user_login
    {
        public int user_id { get; set; }
        public int role_id { get; set; }
        public string username { get; set; }
        public List<result_user_login_menu> user_menu { get; set; }
        public result_user_login()
        {
            this.user_menu = new List<result_user_login_menu>();
        }
    }

    public class result_user_login_menu
    {
        public int menu_id { get; set; }
        public int? parent_menu_id { get; set; }
        public string seq { get; set; }
        public string menu_name { get; set; }
        public string menu_url { get; set; }
        public bool? is_display { get; set; }
        public List<result_user_login_menu> childmenu { get; set; }
        public result_user_login_menu()
        {
            this.childmenu = new List<result_user_login_menu>();
        }
    }
}
