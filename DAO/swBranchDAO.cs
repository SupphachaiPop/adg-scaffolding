using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Definitions;
using Entity.Backend;

namespace DAO.Backend
{
    public class swBranchDAO : IDAORepository<swBranchEntity>
    {
        DBHelper DBHelper = null;
        string conn = "ConnectionStringBackend";
        DateTime dateNow = DateTime.Now;
        public swBranchDAO()
        {
            DBHelper = new DBHelper();
        }

        public List<swBranchEntity> GetDataAll()
        {
            List<swBranchEntity> sw_swBranchEntities = new List<swBranchEntity>();

            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();

                        sw_swBranchEntities = DBHelper.SelectStoreProcedure<swBranchEntity>("select_sw_branch").ToList();
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

            return sw_swBranchEntities;
        }

        public List<swBranchEntity> GetDataByCondition(paramSwBranchEntity param)
        {
            List<swBranchEntity> swBranchListEntities = null;

            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("pageSize", param.pageSize);
                        DBHelper.AddParam("pageNumber", param.pageNumber);
                        DBHelper.AddParam("search", param.search);
                        DBHelper.AddParam("is_active", param.is_active);
                        DBHelper.AddParam("company_id", param.company_id);
                        swBranchListEntities = DBHelper.SelectStoreProcedure<swBranchEntity>("select_sw_branch").ToList();
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
            return swBranchListEntities;
        }

        public swBranchEntity GetDataByID(long id)
        {
            swBranchEntity swBranchEntity = new swBranchEntity();

            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("branch_id", id);
                        swBranchEntity = DBHelper.SelectStoreProcedureFirst<swBranchEntity>("select_sw_branch_by_id");
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
            return swBranchEntity;
        }

        public swBranchEntity GetDataByID(long id, int company_id)
        {
            swBranchEntity swBranchEntity = new swBranchEntity();

            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("branch_id", id);
                        DBHelper.AddParam("company_id", company_id);
                        swBranchEntity = DBHelper.SelectStoreProcedureFirst<swBranchEntity>("select_sw_branch_by_id");
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
            return swBranchEntity;
        }

        public int InsertData(swBranchEntity entity)
        {
            Int32 Sw_admin_id = 0;
            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParamOut("branch_id", entity.branch_id);
                        DBHelper.AddParam("company_id", entity.company_id);
                        DBHelper.AddParam("branch_code", entity.branch_code);
                        DBHelper.AddParam("branch_name", entity.branch_name);
                        DBHelper.AddParam("branch_type", entity.branch_type);
                        DBHelper.AddParam("branch_no", entity.branch_no);
                        DBHelper.AddParam("billing_address", entity.billing_address);
                        DBHelper.AddParam("shipping_address", entity.shipping_address);
                        DBHelper.AddParam("contact_name", entity.contact_name);
                        DBHelper.AddParam("phone", entity.phone);
                        DBHelper.AddParam("email", entity.email);
                        DBHelper.AddParam("comment", entity.comment);
                        DBHelper.AddParam("created_by", 1);
                        DBHelper.AddParam("created_date", dateNow);
                        DBHelper.AddParam("is_active", entity.is_active);

                        DBHelper.ExecuteStoreProcedure("insert_sw_branch");
                        Sw_admin_id = DBHelper.GetParamOut<Int32>("branch_id");

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
            return Sw_admin_id;
        }
        public int UpdateData(swBranchEntity entity)
        {
            Int32 result = 0;

            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParamOut("success_row", result);
                        DBHelper.AddParam("branch_id", entity.branch_id);
                        DBHelper.AddParam("company_id", entity.company_id);
                        DBHelper.AddParam("branch_code", entity.branch_code);
                        DBHelper.AddParam("branch_name", entity.branch_name);
                        DBHelper.AddParam("branch_type", entity.branch_type);
                        DBHelper.AddParam("branch_no", entity.branch_no);
                        DBHelper.AddParam("billing_address", entity.billing_address);
                        DBHelper.AddParam("shipping_address", entity.shipping_address);
                        DBHelper.AddParam("contact_name", entity.contact_name);
                        DBHelper.AddParam("phone", entity.phone);
                        DBHelper.AddParam("email", entity.email);
                        DBHelper.AddParam("comment", entity.comment);
                        DBHelper.AddParam("modified_by", 1);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.AddParam("is_active", entity.is_active);

                        DBHelper.ExecuteStoreProcedure("update_sw_branch");
                        result += DBHelper.GetParamOut<Int32>("success_row");

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
            return result;
        }

        public int UpdateDataStatus(swBranchEntity entity)
        {
            Int32 result = 0;

            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParamOut("success_row", result);                     
                        DBHelper.AddParam("is_active", entity.is_active);
                        DBHelper.AddParam("branch_id", entity.branch_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);

                        DBHelper.ExecuteStoreProcedure("update_sw_branch_status");
                        result = DBHelper.GetParamOut<Int32>("success_row");
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
            return result;
        }
        public int DeleteData(swBranchEntity entity)
        {
            Int32 result = 0;
            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParamOut("success_row", result);
                        DBHelper.AddParam("branch_id", entity.branch_id);
                        DBHelper.AddParam("modified_by", 1);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.ExecuteStoreProcedure("delete_sw_branch");
                        result = DBHelper.GetParamOut<Int32>("success_row");
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
            return result;
        }
        public List<swBranchEntity> GetDataByCondition(swBranchEntity entity)
        {
            throw new NotImplementedException();
        }

        public List<swBranchEntity> GetDataByCondition(swBranchEntity entity, int Index)
        {
            throw new NotImplementedException();
        }
    }
}
