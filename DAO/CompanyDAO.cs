using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Definitions;
using Entity.Backend;

namespace DAO.Backend
{
    public class CompanyDAO : IDAORepository<CompanyEntity>
    {
        DBHelper DBHelper = null;
        string conn = "ConnectionStringBackend";
        DateTime dateNow = DateTime.Now;


        public CompanyDAO()
        {
            DBHelper = new DBHelper();
        }

        public List<CompanyEntity> GetDataAll()
        {
            List<CompanyEntity> sw_CompanyEntities = new List<CompanyEntity>();

            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();

                        sw_CompanyEntities = DBHelper.SelectStoreProcedure<CompanyEntity>("select_sw_company").ToList();
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

            return sw_CompanyEntities;
        }

        public List<CompanyEntity> GetDataByCondition(paramCompanyEntity param)
        {
            List<CompanyEntity> companyListEntities = null;

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
                        companyListEntities = DBHelper.SelectStoreProcedure<CompanyEntity>("select_sw_company").ToList();
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
            return companyListEntities;
        }

        public bool CheckIsReferred(CompanyEntity companyEntity)
        {
            var isReferred = new bool();
            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("company_id", companyEntity.company_id);
                        isReferred = DBHelper.SelectStoreProcedureFirst<bool>("select_sw_company_check_referred");
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

            return isReferred;
        }

        public CompanyEntity GetDataByID(long id)
        {
            CompanyEntity companyEntity = new CompanyEntity();
            companyEntity.companyMenuEntities = new List<CompanyMenuEntity>();
            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("company_id", id);
                        companyEntity = DBHelper.SelectStoreProcedureFirst<CompanyEntity>("select_sw_company_by_id");
                        if (companyEntity != null)
                        {
                            companyEntity.companyMenuEntities = DBHelper.SelectStoreProcedure<CompanyMenuEntity>("select_sw_company_menu").ToList();
                        }
                        else
                        {
                            companyEntity = new CompanyEntity();
                            companyEntity.companyMenuEntities = DBHelper.SelectStoreProcedure<CompanyMenuEntity>("select_sw_company_menu").ToList();
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
            return companyEntity;
        }

        public int InsertData(CompanyEntity entity)
        {
            Int32 company_id = 0;
            var dateNow = DateTime.Now;
            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParamOut("company_id", entity.company_id);
                        DBHelper.AddParam("company_code", entity.company_code);
                        DBHelper.AddParam("company_name", entity.company_name);
                        DBHelper.AddParam("company_type", entity.company_type);
                        DBHelper.AddParam("tax_no", entity.tax_no);
                        DBHelper.AddParam("billing_address", entity.billing_address);
                        DBHelper.AddParam("shipping_address", entity.shipping_address);
                        DBHelper.AddParam("contact_name", entity.contact_name);
                        DBHelper.AddParam("phone", entity.phone);
                        DBHelper.AddParam("email", entity.email);
                        DBHelper.AddParam("comment", entity.comment);
                        DBHelper.AddParam("created_by", entity.created_by);
                        DBHelper.AddParam("created_date", dateNow);
                        DBHelper.AddParam("is_active", entity.is_active);

                        DBHelper.ExecuteStoreProcedure("insert_sw_company");
                        company_id = DBHelper.GetParamOut<Int32>("company_id");

                        entity.companyMenuEntities.ForEach(item =>
                        {
                            DBHelper.CreateParameters();
                            DBHelper.AddParamOut("company_menu_id", item.company_menu_id);
                            DBHelper.AddParam("company_id", company_id);
                            DBHelper.AddParam("menu_id", item.menu_id);
                            DBHelper.AddParam("comment", item.comment);
                            DBHelper.AddParam("created_by", item.created_by);
                            DBHelper.AddParam("created_date", dateNow);
                            DBHelper.AddParam("is_active", item.is_active);
                            DBHelper.ExecuteStoreProcedure("insert_sw_company_menu");
                        });

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
            return company_id;
        }
        public int UpdateData(CompanyEntity entity)
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
                        DBHelper.AddParam("company_id", entity.company_id);
                        DBHelper.AddParam("company_code", entity.company_code);
                        DBHelper.AddParam("company_name", entity.company_name);
                        DBHelper.AddParam("company_type", entity.company_type);
                        DBHelper.AddParam("tax_no", entity.tax_no);
                        DBHelper.AddParam("billing_address", entity.billing_address);
                        DBHelper.AddParam("shipping_address", entity.shipping_address);
                        DBHelper.AddParam("contact_name", entity.contact_name);
                        DBHelper.AddParam("phone", entity.phone);
                        DBHelper.AddParam("email", entity.email);
                        DBHelper.AddParam("comment", entity.comment);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.AddParam("is_active", entity.is_active);

                        DBHelper.ExecuteStoreProcedure("update_sw_company");
                        result = DBHelper.GetParamOut<Int32>("success_row");

                        entity.companyMenuEntities.ForEach(item =>
                        {
                            if (item.company_menu_id != 0)
                            {
                                DBHelper.CreateParameters();
                                DBHelper.AddParamOut("success_row", result);
                                DBHelper.AddParam("company_menu_id", item.company_menu_id);
                                DBHelper.AddParam("company_id", entity.company_id);
                                DBHelper.AddParam("menu_id", item.menu_id);
                                DBHelper.AddParam("comment", item.comment);
                                DBHelper.AddParam("modified_by", item.modified_by);
                                DBHelper.AddParam("modified_date", dateNow);
                                DBHelper.AddParam("is_active", item.is_active);
                                DBHelper.ExecuteStoreProcedure("update_sw_company_menu");
                            }
                            else
                            {
                                DBHelper.CreateParameters();
                                DBHelper.AddParamOut("company_menu_id", item.company_menu_id);
                                DBHelper.AddParam("company_id", entity.company_id);
                                DBHelper.AddParam("menu_id", item.menu_id);
                                DBHelper.AddParam("comment", item.comment);
                                DBHelper.AddParam("created_by", item.created_by);
                                DBHelper.AddParam("created_date", dateNow);
                                DBHelper.AddParam("is_active", item.is_active);
                                DBHelper.ExecuteStoreProcedure("insert_sw_company_menu");
                            }

                        });

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

        public int UpdateDataStatus(CompanyEntity entity)
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
                        DBHelper.AddParam("company_id", entity.company_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);

                        DBHelper.ExecuteStoreProcedure("update_sw_company_status");
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
        public int DeleteData(CompanyEntity entity)
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
                        DBHelper.AddParam("company_id", entity.company_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.ExecuteStoreProcedure("delete_sw_company");
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
        public List<CompanyEntity> GetDataByCondition(CompanyEntity entity)
        {
            throw new NotImplementedException();
        }

        public List<CompanyEntity> GetDataByCondition(CompanyEntity entity, int Index)
        {
            throw new NotImplementedException();
        }
    }
}
