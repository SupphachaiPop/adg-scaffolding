using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity;

namespace Service.Backend
{
    public class UserLoginService
    {
        DataDao dataDao = new DataDao();

        public result_user_login GetUserLogin(param_user_login param)
        {
            return dataDao.GetUserLogin(param);
        }
    }
}
