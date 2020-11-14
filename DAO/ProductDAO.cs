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

        public List<product> GetProductList()
        {
            List<product> res = new List<product>();

            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();

                        res = DBHelper.SelectStoreProcedure<product>("select_list_product").ToList();
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

        public List<result_search_product> SearchProductList(param_search_product param)
        {
            List<result_search_product> res = null;

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
                        res = DBHelper.SelectStoreProcedure<result_search_product>("select_search_product").ToList();
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

        public result_info_product GetProductInfo(long id)
        {
            result_info_product res = new result_info_product();
            res.product_specifications = new List<result_info_product_specification>();
            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("product_id", id);
                        res = DBHelper.SelectStoreProcedureFirst<result_info_product>("select_info_product");
                        if (res != null)
                        {
                            res.product_specifications = DBHelper.SelectStoreProcedure<result_info_product_specification>("select_info_product_specification_by_product").ToList();
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

        public int InsertProduct(param_create_product entity)
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
                        DBHelper.AddParamOut("product_id", entity.product_id);
                        DBHelper.AddParam("specification_id", entity.specification_id);
                        DBHelper.AddParam("product_code", entity.product_code);
                        DBHelper.AddParam("product_name", entity.product_name);
                        DBHelper.AddParam("stock_qty", entity.stock_qty);
                        DBHelper.AddParam("product_cost_price", entity.product_cost_price);
                        DBHelper.AddParam("product_sale_price", entity.product_sale_price);
                        DBHelper.AddParam("product_description", entity.product_description);
                        DBHelper.AddParam("comment", entity.comment);
                        DBHelper.AddParam("created_by", entity.created_by);
                        DBHelper.AddParam("created_date", dateNow);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.AddParam("is_active", entity.is_active);
                        DBHelper.AddParam("is_referred", entity.is_referred);
                        DBHelper.AddParam("is_deleted", entity.is_deleted);
                        DBHelper.ExecuteStoreProcedure("insert_product");
                        res = DBHelper.GetParamOut<Int32>("product_id");

                        if (entity.product_specifications.Count() > 0)
                        {
                            entity.product_specifications.ForEach(i =>
                            {
                                DBHelper.CreateParameters();
                                DBHelper.AddParamOut("product_specification_id", i.product_specification_id);
                                DBHelper.AddParam("product_id", res);
                                DBHelper.AddParam("sub_specification_id", i.sub_specification_id);
                                DBHelper.AddParam("sub_specification_value", i.sub_specification_value);
                                DBHelper.AddParam("comment", i.comment);
                                DBHelper.AddParam("created_by", i.created_by);
                                DBHelper.AddParam("created_date", dateNow);
                                DBHelper.AddParam("modified_by", i.modified_by);
                                DBHelper.AddParam("modified_date", dateNow);
                                DBHelper.AddParam("is_active", i.is_active);
                                DBHelper.AddParam("is_referred", false);
                                DBHelper.AddParam("is_deleted", false);
                                DBHelper.ExecuteStoreProcedure("insert_product_specifications");
                                DBHelper.GetParamOut<Int32>("product_specification_id");
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
        public int UpdateProduct(param_create_product entity)
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
                        DBHelper.AddParam("product_id", entity.product_id);
                        DBHelper.AddParam("specification_id", entity.specification_id);
                        DBHelper.AddParam("product_code", entity.product_code);
                        DBHelper.AddParam("product_name", entity.product_name);
                        DBHelper.AddParam("stock_qty", entity.stock_qty);
                        DBHelper.AddParam("product_cost_price", entity.product_cost_price);
                        DBHelper.AddParam("product_sale_price", entity.product_sale_price);
                        DBHelper.AddParam("product_description", entity.product_description);
                        DBHelper.AddParam("comment", entity.comment);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.AddParam("is_active", entity.is_active);
                        DBHelper.ExecuteStoreProcedure("update_product");
                        res = DBHelper.GetParamOut<Int32>("success_row");

                        if (entity.product_specifications.Count() > 0)
                        {
                            entity.product_specifications.ForEach(i =>
                            {
                                // update product specification 
                                if (i.product_specification_id != 0)
                                {
                                    DBHelper.CreateParameters();
                                    DBHelper.AddParamOut("success_row", res);
                                    DBHelper.AddParam("product_specification_id", i.product_specification_id);
                                    DBHelper.AddParam("product_id", entity.product_id);
                                    DBHelper.AddParam("sub_specification_id", i.sub_specification_id);
                                    DBHelper.AddParam("sub_specification_value", i.sub_specification_value);
                                    DBHelper.AddParam("comment", i.comment);
                                    DBHelper.AddParam("created_by", i.created_by);
                                    DBHelper.AddParam("created_date", dateNow);
                                    DBHelper.AddParam("modified_by", i.modified_by);
                                    DBHelper.AddParam("modified_date", dateNow);
                                    DBHelper.AddParam("is_active", i.is_active);
                                    DBHelper.AddParam("is_referred", false);
                                    DBHelper.AddParam("is_deleted", false);
                                    DBHelper.ExecuteStoreProcedure("update_product_specifications");
                                }
                                else
                                {
                                    DBHelper.CreateParameters();
                                    // create product specification
                                    DBHelper.AddParamOut("product_specification_id", i.product_specification_id);
                                    DBHelper.AddParam("product_id", entity.product_id);
                                    DBHelper.AddParam("sub_specification_id", i.sub_specification_id);
                                    DBHelper.AddParam("sub_specification_value", i.sub_specification_value);
                                    DBHelper.AddParam("comment", i.comment);
                                    DBHelper.AddParam("created_by", i.created_by);
                                    DBHelper.AddParam("created_date", dateNow);
                                    DBHelper.AddParam("modified_by", i.modified_by);
                                    DBHelper.AddParam("modified_date", dateNow);
                                    DBHelper.AddParam("is_active", i.is_active);
                                    DBHelper.AddParam("is_referred", false);
                                    DBHelper.AddParam("is_deleted", false);
                                    DBHelper.ExecuteStoreProcedure("insert_product_specifications");
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

        public int UpdateStatusProduct(product entity)
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
                        DBHelper.AddParam("product_id", entity.product_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);

                        DBHelper.ExecuteStoreProcedure("update_status_product");
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

        public int UpdateReferredProduct(product entity)
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
                        DBHelper.AddParam("product_id", entity.product_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);

                        DBHelper.ExecuteStoreProcedure("update_reffered_product");
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
        public int DeleteProduct(product entity)
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
                        DBHelper.AddParam("product_id", entity.product_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.ExecuteStoreProcedure("delete_product");
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
