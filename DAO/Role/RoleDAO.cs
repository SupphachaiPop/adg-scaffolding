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

        public List<role> GetRoleList()
        {
            List<role> res = new List<role>();

            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();

                        res = DBHelper.SelectStoreProcedure<role>("select_list_role").ToList();
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

        public List<result_search_role> SearchRoleList(param_search_role param)
        {
            List<result_search_role> res = null;

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
                        res = DBHelper.SelectStoreProcedure<result_search_role>("select_search_role").ToList();
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

        public result_info_role GetRoleInfo(long id)
        {
            result_info_role res = new result_info_role();
            res.role_menu = new List<result_info_role_menu>();
            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("role_id", id);
                        res = DBHelper.SelectStoreProcedureFirst<result_info_role>("select_info_role");
                        res = res != null ? res : new result_info_role();
                        res.role_menu = DBHelper.SelectStoreProcedure<result_info_role_menu>("select_info_role_menu_by_role").ToList();

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

        public int InsertRole(param_create_role entity)
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
                        DBHelper.AddParamOut("role_id", entity.role_id);
                        DBHelper.AddParam("role_code", entity.role_code);
                        DBHelper.AddParam("role_name", entity.role_name);
                        DBHelper.AddParam("comment", entity.comment);
                        DBHelper.AddParam("created_by", entity.created_by);
                        DBHelper.AddParam("created_date", dateNow);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.AddParam("is_active", entity.is_active);
                        DBHelper.AddParam("is_referred", entity.is_referred);
                        DBHelper.AddParam("is_deleted", entity.is_deleted);
                        DBHelper.ExecuteStoreProcedure("insert_role");
                        res = DBHelper.GetParamOut<Int32>("role_id");

                        if (entity.role_menu.Count() > 0)
                        {
                            entity.role_menu.ForEach(i =>
                            {
                                DBHelper.CreateParameters();
                                DBHelper.AddParamOut("role_menu_id", i.role_menu_id);
                                DBHelper.AddParam("role_id", res);
                                DBHelper.AddParam("menu_id", i.menu_id);
                                DBHelper.AddParam("is_display", i.is_display);
                                DBHelper.AddParam("comment", i.comment);
                                DBHelper.AddParam("created_by", i.created_by);
                                DBHelper.AddParam("created_date", dateNow);
                                DBHelper.AddParam("modified_by", i.modified_by);
                                DBHelper.AddParam("modified_date", dateNow);
                                DBHelper.AddParam("is_active", true);
                                DBHelper.AddParam("is_referred", false);
                                DBHelper.AddParam("is_deleted", false);
                                DBHelper.ExecuteStoreProcedure("insert_role_menu");
                                DBHelper.GetParamOut<Int32>("role_menu_id");
                            });
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
            return res;
        }
        public int UpdateRole(param_create_role entity)
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
                        DBHelper.AddParam("role_id", entity.role_id);
                        DBHelper.AddParam("role_code", entity.role_code);
                        DBHelper.AddParam("role_name", entity.role_name);
                        DBHelper.AddParam("comment", entity.comment);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.AddParam("is_active", entity.is_active);
                        DBHelper.ExecuteStoreProcedure("update_role");
                        res = DBHelper.GetParamOut<Int32>("success_row");

                        if (entity.role_menu.Count() > 0)
                        {
                            entity.role_menu.ForEach(i =>
                            {
                                // update role menu 
                                if (i.role_menu_id != 0)
                                {
                                    DBHelper.CreateParameters();
                                    DBHelper.AddParamOut("success_row", res);
                                    DBHelper.AddParam("role_menu_id", i.role_menu_id);
                                    DBHelper.AddParam("role_id", entity.role_id);
                                    DBHelper.AddParam("menu_id", i.menu_id);
                                    DBHelper.AddParam("is_display", i.is_display);
                                    DBHelper.AddParam("comment", i.comment);
                                    DBHelper.AddParam("created_by", i.created_by);
                                    DBHelper.AddParam("created_date", dateNow);
                                    DBHelper.AddParam("modified_by", i.modified_by);
                                    DBHelper.AddParam("modified_date", dateNow);
                                    DBHelper.AddParam("is_active", true);
                                    DBHelper.AddParam("is_referred", false);
                                    DBHelper.AddParam("is_deleted", false);
                                    DBHelper.ExecuteStoreProcedure("update_role_menu");
                                }
                                else
                                {
                                    DBHelper.CreateParameters();
                                    // create role menu
                                    DBHelper.AddParamOut("role_menu_id", i.role_menu_id);
                                    DBHelper.AddParam("role_id", entity.role_id);
                                    DBHelper.AddParam("menu_id", i.menu_id);
                                    DBHelper.AddParam("is_display", i.is_display);
                                    DBHelper.AddParam("comment", i.comment);
                                    DBHelper.AddParam("created_by", i.created_by);
                                    DBHelper.AddParam("created_date", dateNow);
                                    DBHelper.AddParam("modified_by", i.modified_by);
                                    DBHelper.AddParam("modified_date", dateNow);
                                    DBHelper.AddParam("is_active", true);
                                    DBHelper.AddParam("is_referred", false);
                                    DBHelper.AddParam("is_deleted", false);
                                    DBHelper.ExecuteStoreProcedure("insert_role_menu");
                                }
                               
                            });
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
            return res;
        }

        public int UpdateStatusRole(role entity)
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
                        DBHelper.AddParam("role_id", entity.role_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);

                        DBHelper.ExecuteStoreProcedure("update_status_role");
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

        public int UpdateReferredRole(role entity)
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
                        DBHelper.AddParam("role_id", entity.role_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);

                        DBHelper.ExecuteStoreProcedure("update_reffered_role");
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
        public int DeleteRole(role entity)
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
                        DBHelper.AddParam("role_id", entity.role_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.ExecuteStoreProcedure("delete_role");
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
