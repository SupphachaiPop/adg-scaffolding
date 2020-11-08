using Definitions;
using Entity.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.Backend.MasterData
{
    public class BrandDAO
    {
        DBHelper DBHelper = null;
        string conn = "ConnectionStringBackend";
        DateTime dateNow = DateTime.Now;

        public BrandDAO()
        {
            DBHelper = new DBHelper();
        }

        public List<Backend_sw_branch_Entity> GetDataAll()
        {
            List<Backend_sw_branch_Entity> entities = new List<Backend_sw_branch_Entity>();

            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        entities = DBHelper.SelectStoreProcedure<Backend_sw_branch_Entity>("select_sw_brand").ToList();
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

        public Backend_sw_branch_Entity GetDataByID(int id)
        {
            Backend_sw_branch_Entity entity = new Backend_sw_branch_Entity();

            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("brand_id", id);
                        entity = DBHelper.SelectStoreProcedureFirst<Backend_sw_branch_Entity>("select_sw_brand_by_id");
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

        public int InsertData(ParamInsertSwBrand param)
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
                        DBHelper.AddParamOut("brand_id", param.brand_id);
                        DBHelper.AddParam("brand_code", param.brand_code);
                        DBHelper.AddParam("brand_name", param.brand_name);
                        DBHelper.AddParam("company_id", param.company_id);
                        DBHelper.AddParam("comment", param.comment);
                        DBHelper.AddParam("created_by", param.created_by);
                        DBHelper.AddParam("created_date", dateNow);
                        DBHelper.AddParam("is_active", param.is_active);

                        DBHelper.ExecuteStoreProcedure("insert_sw_brand");
                        param.company_id = DBHelper.GetParamOut<Int32>("brand_id");

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

        public bool UpdateData(ParamUpdateSwBrand param)
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
                        DBHelper.AddParam("brand_id", param.brand_id);
                        DBHelper.AddParam("brand_code", param.brand_code);
                        DBHelper.AddParam("brand_name", param.brand_name);
                        DBHelper.AddParam("comment", param.comment);
                        DBHelper.AddParam("modified_by", param.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.AddParam("is_active", param.is_active);
                        DBHelper.AddParam("is_deleted", param.is_deleted);

                        DBHelper.ExecuteStoreProcedure("update_sw_brand");
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

        public int DeleteData(ParamDeleteSwBrand param)
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
                        DBHelper.AddParam("brand_id", param.brand_id);
                        DBHelper.AddParam("modified_by", param.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);

                        DBHelper.ExecuteStoreProcedure("delete_sw_brand");
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

        public List<Backend_sw_branch_Entity> FindDuplicateRecord(ParamSelectSwBrandCheckDuplicate param)
        {
            List<Backend_sw_branch_Entity> entities = new List<Backend_sw_branch_Entity>();
            var dateNow = DateTime.Now;
            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("brand_code", param.brand_code);
                        DBHelper.AddParam("company_id", param.company_id);

                        entities = DBHelper.SelectStoreProcedure<Backend_sw_branch_Entity>("select_sw_brand_check_duplicate").ToList();

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

        public Backend_sw_branch_Entity CheckReferred(ParamSelectSwBrandCheckReferred param)
        {
            Backend_sw_branch_Entity entitie = new Backend_sw_branch_Entity();
            var dateNow = DateTime.Now;
            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("brand_id", param.brand_id);
                        DBHelper.AddParam("company_id", param.company_id);

                        entitie = DBHelper.SelectStoreProcedureFirst<Backend_sw_branch_Entity>("select_sw_brand_check_referred");

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
