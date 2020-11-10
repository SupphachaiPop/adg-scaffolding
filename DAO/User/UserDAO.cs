using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Definitions;
using Entity;

namespace DAO.Backend
{
    public partial class DataDao
    {
        DBHelper DBHelper = null;
        DateTime dateNow = DateTime.Now;
        public DataDao()
        {
            DBHelper = new DBHelper();
        }

        public result_user_login GetLoginUser(param_user_login param)
        {
            result_user_login user = new result_user_login();
            user.user_menu = new List<result_user_login_menu>();
            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("username", param.username);
                        DBHelper.AddParam("password", param.password);
                        user = DBHelper.SelectStoreProcedureFirst<result_user_login>("select_login_user");

                        if (user != null)
                        {
                            DBHelper.CreateParameters();
                            DBHelper.AddParam("role_id", user.role_id);
                            user.user_menu = DBHelper.SelectStoreProcedure<result_user_login_menu>("select_info_role_menu_by_role").Where(i=> i.is_display == true).ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        DBHelper.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return user;
        }

        public List<user> GetUserList()
        {
            List<user> res = new List<user>();

            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        res = DBHelper.SelectStoreProcedure<user>("select_list_user").ToList();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        DBHelper.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public List<result_search_user> SearchUserList(param_search_user param)
        {
            List<result_search_user> res = null;

            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("page_size", param.pageSize);
                        DBHelper.AddParam("page_number", param.pageNumber);
                        DBHelper.AddParam("search", param.search);
                        DBHelper.AddParam("is_active", param.is_active);
                        res = DBHelper.SelectStoreProcedure<result_search_user>("select_search_user").ToList();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        DBHelper.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public result_info_user GetInfoUser(long id)
        {
            result_info_user res = new result_info_user();

            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("user_id", id);
                        res = DBHelper.SelectStoreProcedureFirst<result_info_user>("select_info_user");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        DBHelper.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public int InsertUser(param_create_user entity)
        {
            Int32 res = 0;
            var dateNow = DateTime.Now;
            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParamOut("user_id", entity.user_id);
                        DBHelper.AddParam("role_id", entity.role_id);
                        DBHelper.AddParam("username", entity.username);
                        DBHelper.AddParam("password", entity.password);
                        DBHelper.AddParam("first_name", entity.first_name);
                        DBHelper.AddParam("last_name", entity.last_name);
                        DBHelper.AddParam("nick_name", entity.nick_name);
                        DBHelper.AddParam("address", entity.address);
                        DBHelper.AddParam("email", entity.email);
                        DBHelper.AddParam("phone", entity.phone);
                        DBHelper.AddParam("comment", entity.comment);
                        DBHelper.AddParam("created_by", entity.created_by);
                        DBHelper.AddParam("created_date", dateNow);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.AddParam("is_active", entity.is_active);
                        DBHelper.AddParam("is_referred", entity.is_referred);
                        DBHelper.AddParam("is_deleted", entity.is_deleted);

                        DBHelper.ExecuteStoreProcedure("insert_user");
                        res = DBHelper.GetParamOut<Int32>("user_id");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        DBHelper.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
        public int UpdateUser(param_create_user entity)
        {
            Int32 res = 0;

            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParamOut("success_row", res);
                        DBHelper.AddParam("user_id", entity.user_id);
                        DBHelper.AddParam("role_id", entity.role_id);
                        DBHelper.AddParam("username", entity.username);
                        DBHelper.AddParam("password", entity.password);
                        DBHelper.AddParam("first_name", entity.first_name);
                        DBHelper.AddParam("last_name", entity.last_name);
                        DBHelper.AddParam("nick_name", entity.nick_name);
                        DBHelper.AddParam("address", entity.address);
                        DBHelper.AddParam("email", entity.email);
                        DBHelper.AddParam("phone", entity.phone);
                        DBHelper.AddParam("comment", entity.comment);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.AddParam("is_active", entity.is_active);

                        DBHelper.ExecuteStoreProcedure("update_user");
                        res = DBHelper.GetParamOut<Int32>("success_row");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        DBHelper.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public int UpdateStatusUser(user entity)
        {
            Int32 res = 0;

            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParamOut("success_row", res);
                        DBHelper.AddParam("is_active", entity.is_active);
                        DBHelper.AddParam("user_id", entity.user_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);

                        DBHelper.ExecuteStoreProcedure("update_status_user");
                        res = DBHelper.GetParamOut<Int32>("success_row");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        DBHelper.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public int UpdateRefferedUser(user entity)
        {
            Int32 res = 0;

            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParamOut("success_row", res);
                        DBHelper.AddParam("is_referred", entity.is_referred);
                        DBHelper.AddParam("user_id", entity.user_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);

                        DBHelper.ExecuteStoreProcedure("update_reffered_user");
                        res = DBHelper.GetParamOut<Int32>("success_row");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        DBHelper.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public int DeleteUser(user entity)
        {
            Int32 res = 0;
            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParamOut("success_row", res);
                        DBHelper.AddParam("user_id", entity.user_id);
                        DBHelper.AddParam("modified_by", 1);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.ExecuteStoreProcedure("delete_user");
                        res = DBHelper.GetParamOut<Int32>("success_row");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        DBHelper.CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
    }
}
