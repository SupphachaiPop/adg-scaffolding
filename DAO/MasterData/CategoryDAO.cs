using Definitions;
using Entity.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.Backend.MasterData
{
    public class CategoryDAO
    {
        DBHelper DBHelper = null;
        string conn = "ConnectionStringBackend";
        DateTime dateNow = DateTime.Now;


        public CategoryDAO()
        {
            DBHelper = new DBHelper();
        }

        public List<SwCategoryEntity> GetDataAll()
        {
            List<SwCategoryEntity> entities = new List<SwCategoryEntity>();

            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        entities = DBHelper.SelectStoreProcedure<SwCategoryEntity>("select_sw_category").ToList();
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

            return entities;
        }

        public SwCategoryEntity GetDataByID(int id)
        {
            SwCategoryEntity entity = new SwCategoryEntity();

            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("category_id", id);
                        entity = DBHelper.SelectStoreProcedureFirst<SwCategoryEntity>("select_sw_category_by_id");
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
            return entity;
        }

        public int InsertData(ParamInsertSwCategory param)
        {
            var dateNow = DateTime.Now;
            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParamOut("category_id", param.category_id);
                        DBHelper.AddParam("category_code", param.category_code);
                        DBHelper.AddParam("category_name", param.category_name);
                        DBHelper.AddParam("company_id", param.company_id);
                        DBHelper.AddParam("comment", param.comment);
                        DBHelper.AddParam("created_by", param.created_by);
                        DBHelper.AddParam("created_date", dateNow);
                        DBHelper.AddParam("is_active", param.is_active);

                        DBHelper.ExecuteStoreProcedure("insert_sw_category");
                        param.company_id = DBHelper.GetParamOut<Int32>("category_id");

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
            return param.company_id;
        }

        public bool UpdateData(ParamUpdateSwCategory param)
        {
            var dateNow = DateTime.Now;
            bool res = false;
            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("category_id", param.category_id);
                        DBHelper.AddParam("category_code", param.category_code);
                        DBHelper.AddParam("category_name", param.category_name);
                        DBHelper.AddParam("comment", param.comment);
                        DBHelper.AddParam("modified_by", param.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.AddParam("is_active", param.is_active);
                        DBHelper.AddParam("is_deleted", param.is_deleted);

                        DBHelper.ExecuteStoreProcedure("update_sw_category");
                        res = true;

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

        public int DeleteData(ParamDeleteSwCategory param)
        {
            var dateNow = DateTime.Now;
            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParamOut("success_row", param.success_row);
                        DBHelper.AddParam("category_id", param.category_id);
                        DBHelper.AddParam("modified_by", param.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);

                        DBHelper.ExecuteStoreProcedure("delete_sw_category");
                        param.success_row = DBHelper.GetParamOut<Int32>("success_row");

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
            return param.success_row;
        }

        public List<SwCategoryEntity> FindDuplicateRecord(ParamSelectSwCategoryCheckDuplicate param)
        {
            List<SwCategoryEntity> entities = new List<SwCategoryEntity>();
               var dateNow = DateTime.Now;
            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("category_code", param.category_code);
                        DBHelper.AddParam("company_id", param.company_id);

                        entities = DBHelper.SelectStoreProcedure<SwCategoryEntity>("select_sw_category_check_duplicate").ToList();

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
            return entities;
        }

        public SwCategoryEntity CheckReferred(ParamSelectSwCategoryCheckReferred param)
        {
            SwCategoryEntity entitie = new SwCategoryEntity();
            var dateNow = DateTime.Now;
            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("category_id", param.category_id);
                        DBHelper.AddParam("company_id", param.company_id);

                        entitie = DBHelper.SelectStoreProcedureFirst<SwCategoryEntity>("select_sw_category_check_referred");

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
            return entitie;
        }
    }
}
