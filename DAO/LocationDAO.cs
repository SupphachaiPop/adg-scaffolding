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

        public List<location> GetLocationList()
        {
            List<location> res = new List<location>();

            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();

                        res = DBHelper.SelectStoreProcedure<location>("select_list_location").ToList();
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

        public List<result_search_location> SearchLocationList(param_search_location param)
        {
            List<result_search_location> res = null;

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
                        DBHelper.AddParam("warehouse_id", param.warehouse_id);
                        DBHelper.AddParam("is_active", param.is_active);
                        res = DBHelper.SelectStoreProcedure<result_search_location>("select_search_location").ToList();
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

        public result_info_location GetLocationInfo(long id)
        {
            result_info_location res = new result_info_location();
            res.zone = new List<result_info_zone>();
            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("location_id", id);
                        res = DBHelper.SelectStoreProcedureFirst<result_info_location>("select_info_location");
                        if (res != null)
                        {
                            res.zone = DBHelper.SelectStoreProcedure<result_info_zone>("select_info_zone_by_location").ToList();
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

        public int InsertLocation(param_create_location entity)
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
                        DBHelper.AddParamOut("location_id", entity.location_id);
                        DBHelper.AddParam("warehouse_id", entity.warehouse_id);
                        DBHelper.AddParam("location_code", entity.location_code);
                        DBHelper.AddParam("location_name", entity.location_name);
                        DBHelper.AddParam("comment", entity.comment);
                        DBHelper.AddParam("created_by", entity.created_by);
                        DBHelper.AddParam("created_date", dateNow);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.AddParam("is_active", entity.is_active);
                        DBHelper.AddParam("is_referred", entity.is_referred);
                        DBHelper.AddParam("is_deleted", entity.is_deleted);
                        DBHelper.ExecuteStoreProcedure("insert_location");
                        res = DBHelper.GetParamOut<Int32>("location_id");

                        if (entity.zone.Count() > 0)
                        {
                            entity.zone.ForEach(i =>
                            {
                                if (!i.flag_delete)
                                {
                                    DBHelper.CreateParameters();
                                    DBHelper.AddParamOut("zone_id", i.zone_id);
                                    DBHelper.AddParam("location_id", res);
                                    DBHelper.AddParam("zone_code", i.zone_code);
                                    DBHelper.AddParam("zone_name", i.zone_name);
                                    DBHelper.AddParam("comment", i.comment);
                                    DBHelper.AddParam("created_by", i.created_by);
                                    DBHelper.AddParam("created_date", dateNow);
                                    DBHelper.AddParam("modified_by", i.modified_by);
                                    DBHelper.AddParam("modified_date", dateNow);
                                    DBHelper.AddParam("is_active", true);
                                    DBHelper.AddParam("is_referred", false);
                                    DBHelper.AddParam("is_deleted", false);
                                    DBHelper.ExecuteStoreProcedure("insert_zone");
                                    DBHelper.GetParamOut<Int32>("zone_id");
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
        public int UpdateLocation(param_create_location entity)
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
                        DBHelper.AddParam("location_id", entity.location_id);
                        DBHelper.AddParam("location_code", entity.location_code);
                        DBHelper.AddParam("location_name", entity.location_name);
                        DBHelper.AddParam("comment", entity.comment);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.AddParam("is_active", entity.is_active);
                        DBHelper.ExecuteStoreProcedure("update_location");
                        res = DBHelper.GetParamOut<Int32>("success_row");

                        if (entity.zone.Count() > 0)
                        {
                            entity.zone.ForEach(i =>
                            {
                                // update zone
                                if (i.zone_id != 0)
                                {
                                    if (!i.flag_delete)
                                    {
                                        DBHelper.CreateParameters();
                                        DBHelper.AddParamOut("success_row", res);
                                        DBHelper.AddParam("zone_id", i.zone_id);
                                        DBHelper.AddParam("location_id", entity.location_id);
                                        DBHelper.AddParam("zone_code", i.zone_code);
                                        DBHelper.AddParam("zone_name", i.zone_name);
                                        DBHelper.AddParam("comment", i.comment);
                                        DBHelper.AddParam("created_by", i.created_by);
                                        DBHelper.AddParam("created_date", dateNow);
                                        DBHelper.AddParam("modified_by", i.modified_by);
                                        DBHelper.AddParam("modified_date", dateNow);
                                        DBHelper.AddParam("is_active", true);
                                        DBHelper.AddParam("is_referred", false);
                                        DBHelper.AddParam("is_deleted", false);
                                        DBHelper.ExecuteStoreProcedure("update_zone");
                                    }
                                    else
                                    {
                                        DBHelper.CreateParameters();
                                        DBHelper.AddParamOut("success_row", res);
                                        DBHelper.AddParam("zone_id", i.zone_id);
                                        DBHelper.AddParam("modified_by", i.modified_by);
                                        DBHelper.AddParam("modified_date", dateNow);
                                        DBHelper.ExecuteStoreProcedure("delete_zone");
                                    }
                                }
                                else
                                {
                                    if (!i.flag_delete)
                                    {
                                        DBHelper.CreateParameters();
                                        // create zone
                                        DBHelper.AddParamOut("zone_id", i.zone_id);
                                        DBHelper.AddParam("location_id", entity.location_id);
                                        DBHelper.AddParam("zone_code", i.zone_code);
                                        DBHelper.AddParam("zone_name", i.zone_name);
                                        DBHelper.AddParam("comment", i.comment);
                                        DBHelper.AddParam("created_by", i.created_by);
                                        DBHelper.AddParam("created_date", dateNow);
                                        DBHelper.AddParam("modified_by", i.modified_by);
                                        DBHelper.AddParam("modified_date", dateNow);
                                        DBHelper.AddParam("is_active", true);
                                        DBHelper.AddParam("is_referred", false);
                                        DBHelper.AddParam("is_deleted", false);
                                        DBHelper.ExecuteStoreProcedure("insert_zone");
                                    }
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

        public int UpdateStatusLocation(location entity)
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
                        DBHelper.AddParam("location_id", entity.location_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);

                        DBHelper.ExecuteStoreProcedure("update_status_location");
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

        public int UpdateReferredLocation(location entity)
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
                        DBHelper.AddParam("location_id", entity.location_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);

                        DBHelper.ExecuteStoreProcedure("update_reffered_location");
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
        public int DeleteLocation(location entity)
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
                        DBHelper.AddParam("location_id", entity.location_id);
                        DBHelper.AddParam("modified_by", entity.modified_by);
                        DBHelper.AddParam("modified_date", dateNow);
                        DBHelper.ExecuteStoreProcedure("delete_location");
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
