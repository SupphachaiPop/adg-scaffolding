using System;
using System.Collections.Generic;
using System.Text;

namespace Definitions
{
    public class StaticUrl
    {
        public const string RoleListUrl = "/Backend/Administrator/Role/role-list.aspx";
        public const string RoleInfoUrl = "/Backend/Administrator/Role/role-info.aspx";
        public const string UserListUrl = "/Backend/Administrator/User/user-list.aspx";
        public const string UserInfoUrl = "/Backend/Administrator/User/user-info.aspx";
        public const string SpecificationListUrl = "/Backend/Product-Management/Specification/specification-list.aspx";
        public const string SpecificationInfoUrl = "/Backend/Product-Management/Specification/specification-info.aspx";
        public const string ProductListUrl = "/Backend/Product-Management/Product/product-list.aspx";
        public const string ProductInfoUrl = "/Backend/Product-Management/Product/product-info.aspx";

        public const string DEFAULT_EMPTY_URL = "javascript:void(0);";
        public const string BASE_URL = "/Backend";
    }
}
