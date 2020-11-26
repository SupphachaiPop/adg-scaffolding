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
        public List<customer> GetCustomerList()
        {
            List<customer> res = new List<customer>();

            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        res = DBHelper.SelectStoreProcedure<customer>("select_list_customer").ToList();
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

        public List<result_search_customer> SearchCustomerList(param_search_customer param)
        {
            List<result_search_customer> res = null;

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
                        res = DBHelper.SelectStoreProcedure<result_search_customer>("select_search_customer").ToList();
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

        public result_info_customer GetCustomerInfo(long id)
        {
            result_info_customer res = new result_info_customer();

            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("customer_id", id);
                        res = DBHelper.SelectStoreProcedureFirst<result_info_customer>("select_info_customer");
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

        public int InsertCustomer(param_create_customer entity)
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
                        DBHelper.AddParamOut("customer_id", entity.customer_id);
                        DBHelper.AddParam("customer_code", entity.customer_code);
                        DBHelper.AddParam("customer_name", entity.customer_name);
                        DBHelper.AddParam("tax_no", entity.tax_no);
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

                        DBHelper.ExecuteStoreProcedure("insert_customer");
                        res = DBHelper.GetParamOut<Int32>("customer_id");
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
        public int UpdateCustomer(param_create_customer entity)
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
                        DBHelper.AddParam("customer_id", entity.customer_id);
                        DBHelper.AddParam("customer_code", entity.customer_code);
                        DBHelper.AddParam("customer_name", entity.customer_name);
                        DBHelper.AddParam("tax_no", entity.tax_no);
                        DBHelper.AddParam("address", entity.address);
                        DBHelper.AddParam("email", entity.email);
                        DBHelper.AddParam("phone", entity.phone);
                        DBHelper.AddParam("comment", entity.comment);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.AddParam("is_active", entity.is_active);

                        DBHelper.ExecuteStoreProcedure("update_customer");
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

        public int UpdateStatusCustomer(customer entity)
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
                        DBHelper.AddParam("customer_id", entity.customer_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);

                        DBHelper.ExecuteStoreProcedure("update_status_customer");
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

        public int UpdateRefferedCustomer(customer entity)
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
                        DBHelper.AddParam("customer_id", entity.customer_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);

                        DBHelper.ExecuteStoreProcedure("update_reffered_customer");
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

        public int DeleteCustomer(customer entity)
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
                        DBHelper.AddParam("customer_id", entity.customer_id);
                        DBHelper.AddParam("modified_by", 1);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.ExecuteStoreProcedure("delete_customer");
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
