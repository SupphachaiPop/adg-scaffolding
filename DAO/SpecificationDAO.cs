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

        public List<specification> GetSpecificationList()
        {
            List<specification> res = new List<specification>();

            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();

                        res = DBHelper.SelectStoreProcedure<specification>("select_list_specification").ToList();
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

        public List<result_search_specification> SearchSpecificationList(param_search_specification param)
        {
            List<result_search_specification> res = null;

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
                        res = DBHelper.SelectStoreProcedure<result_search_specification>("select_search_specification").ToList();
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

        public result_info_specification GetSpecificationInfo(long id)
        {
            result_info_specification res = new result_info_specification();
            res.sub_specifications = new List<result_info_sub_specification>();
            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("specification_id", id);
                        res = DBHelper.SelectStoreProcedureFirst<result_info_specification>("select_info_specification");
                        if (res != null)
                        {
                            res.sub_specifications = DBHelper.SelectStoreProcedure<result_info_sub_specification>("select_info_sub_specification_by_specification").ToList();
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

        public int InsertSpecification(param_create_specification entity)
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
                        DBHelper.AddParamOut("specification_id", entity.specification_id);
                        DBHelper.AddParam("specification_code", entity.specification_code);
                        DBHelper.AddParam("specification_name", entity.specification_name);
                        DBHelper.AddParam("comment", entity.comment);
                        DBHelper.AddParam("created_by", entity.created_by);
                        DBHelper.AddParam("created_date", dateNow);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.AddParam("is_active", entity.is_active);
                        DBHelper.AddParam("is_referred", entity.is_referred);
                        DBHelper.AddParam("is_deleted", entity.is_deleted);
                        DBHelper.ExecuteStoreProcedure("insert_specification");
                        res = DBHelper.GetParamOut<Int32>("specification_id");

                        if (entity.sub_specifications.Count() > 0)
                        {
                            entity.sub_specifications.ForEach(i =>
                            {
                                if (!i.flag_delete)
                                {
                                    DBHelper.CreateParameters();
                                    DBHelper.AddParamOut("sub_specification_id", i.sub_specification_id);
                                    DBHelper.AddParam("specification_id", res);
                                    DBHelper.AddParam("sub_specification_name", i.sub_specification_name);
                                    DBHelper.AddParam("comment", i.comment);
                                    DBHelper.AddParam("created_by", i.created_by);
                                    DBHelper.AddParam("created_date", dateNow);
                                    DBHelper.AddParam("modified_by", i.modified_by);
                                    DBHelper.AddParam("modified_date", dateNow);
                                    DBHelper.AddParam("is_active", i.is_active);
                                    DBHelper.AddParam("is_referred", false);
                                    DBHelper.AddParam("is_deleted", false);
                                    DBHelper.ExecuteStoreProcedure("insert_sub_specification");
                                    DBHelper.GetParamOut<Int32>("sub_specification_id");
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
        public int UpdateSpecification(param_create_specification entity)
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
                        DBHelper.AddParam("specification_id", entity.specification_id);
                        DBHelper.AddParam("specification_code", entity.specification_code);
                        DBHelper.AddParam("specification_name", entity.specification_name);
                        DBHelper.AddParam("comment", entity.comment);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.AddParam("is_active", entity.is_active);
                        DBHelper.ExecuteStoreProcedure("update_specification");
                        res = DBHelper.GetParamOut<Int32>("success_row");

                        if (entity.sub_specifications.Count() > 0)
                        {
                            entity.sub_specifications.ForEach(i =>
                            {
                                // update specification menu 
                                if (i.sub_specification_id != 0)
                                {
                                    DBHelper.CreateParameters();
                                    DBHelper.AddParamOut("success_row", res);
                                    DBHelper.AddParam("sub_specification_id", i.sub_specification_id);
                                    DBHelper.AddParam("specification_id", entity.specification_id);
                                    DBHelper.AddParam("sub_specification_name", i.sub_specification_name);
                                    DBHelper.AddParam("comment", i.comment);
                                    DBHelper.AddParam("created_by", i.created_by);
                                    DBHelper.AddParam("created_date", dateNow);
                                    DBHelper.AddParam("modified_by", i.modified_by);
                                    DBHelper.AddParam("modified_date", dateNow);
                                    DBHelper.AddParam("is_active", i.is_active);
                                    DBHelper.AddParam("is_referred", false);
                                    DBHelper.AddParam("is_deleted", false);
                                    DBHelper.ExecuteStoreProcedure("update_sub_specification");
                                }
                                else
                                {
                                    DBHelper.CreateParameters();
                                    // create specification menu
                                    DBHelper.CreateParameters();
                                    DBHelper.AddParamOut("sub_specification_id", i.sub_specification_id);
                                    DBHelper.AddParam("specification_id", entity.specification_id);
                                    DBHelper.AddParam("sub_specification_name", i.sub_specification_name);
                                    DBHelper.AddParam("comment", i.comment);
                                    DBHelper.AddParam("created_by", i.created_by);
                                    DBHelper.AddParam("created_date", dateNow);
                                    DBHelper.AddParam("modified_by", i.modified_by);
                                    DBHelper.AddParam("modified_date", dateNow);
                                    DBHelper.AddParam("is_active", i.is_active);
                                    DBHelper.AddParam("is_referred", false);
                                    DBHelper.AddParam("is_deleted", false);
                                    DBHelper.ExecuteStoreProcedure("insert_sub_specification");
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

        public int UpdateStatusSpecification(specification entity)
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
                        DBHelper.AddParam("specification_id", entity.specification_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);

                        DBHelper.ExecuteStoreProcedure("update_status_specification");
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

        public int UpdateReferredSpecification(specification entity)
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
                        DBHelper.AddParam("specification_id", entity.specification_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);

                        DBHelper.ExecuteStoreProcedure("update_reffered_specification");
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
        public int DeleteSpecification(specification entity)
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
                        DBHelper.AddParam("specification_id", entity.specification_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.ExecuteStoreProcedure("delete_specification");
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
