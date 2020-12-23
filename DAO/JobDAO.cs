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
        public List<job> GetJobList(int pZoneId)
        {
            List<job> res = new List<job>();

            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("zone_id", pZoneId);
                        res = DBHelper.SelectStoreProcedure<job>("select_list_job_zone").ToList();
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

        public List<result_search_job_zone> SearchJobZoneList(param_search_job_zone param)
        {
            List<result_search_job_zone> res = null;

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
                        DBHelper.AddParam("location_id", param.location_id);
                        res = DBHelper.SelectStoreProcedure<result_search_job_zone>("select_search_job_zone").ToList();
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

        public result_info_job_zone GetJobZoneInfo(int zoneId, int statusId)
        {
            result_info_job_zone res = new result_info_job_zone();
            res.items = new List<result_info_job_zone_item>();

            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("zone_id", zoneId);
                        DBHelper.AddParam("status_id", statusId);
                        res = DBHelper.SelectStoreProcedureFirst<result_info_job_zone>("select_info_job_zone");
                        if (res != null)
                        {
                            res.items = DBHelper.SelectStoreProcedure<result_info_job_zone_item>("select_info_job_zone_item").ToList();
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

        public int UpdateJob(List<param_create_job> entities)
        {
            Int32 res = 0;

            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();

                        entities.ForEach(i =>
                        {
                            if (i.job_id != 0)
                            {
                                DBHelper.CreateParameters();
                                DBHelper.AddParamOut("success_row", res);
                                DBHelper.AddParam("job_id", i.job_id);
                                DBHelper.AddParam("zone_id", i.zone_id);
                                DBHelper.AddParam("product_id", i.product_id);
                                DBHelper.AddParam("amount", i.amount);
                                DBHelper.AddParam("status_id", i.status_id);
                                DBHelper.AddParam("comment", i.comment);
                                DBHelper.AddParam("is_active", true);
                                DBHelper.AddParam("is_deleted", i.is_deleted);
                                DBHelper.AddParam("modified_by", i.modified_by);
                                DBHelper.AddParam("modified_date", dateNow);
                                DBHelper.ExecuteStoreProcedure("update_job");
                                res = DBHelper.GetParamOut<Int32>("success_row");

                                DBHelper.CreateParameters();
                                DBHelper.AddParamOut("job_history_id", res);
                                DBHelper.AddParam("job_id", i.job_id);
                                DBHelper.AddParam("amount", i.amount);
                                DBHelper.AddParam("status_id", i.status_id);
                                DBHelper.AddParam("parent_job_id", null);
                                DBHelper.AddParam("comment", i.comment);
                                DBHelper.AddParam("is_active", true);
                                DBHelper.AddParam("is_deleted", i.is_deleted);
                                DBHelper.AddParam("created_by", i.created_by);
                                DBHelper.AddParam("created_date", dateNow);
                                DBHelper.AddParam("modified_by", i.modified_by);
                                DBHelper.AddParam("modified_date", dateNow);
                                DBHelper.ExecuteStoreProcedure("insert_job_history");
                            }
                            else
                            {
                                if (!i.is_deleted.Value)
                                {
                                    DBHelper.CreateParameters();
                                    DBHelper.AddParamOut("job_id", i.job_id);
                                    DBHelper.AddParam("zone_id", i.zone_id);
                                    DBHelper.AddParam("product_id", i.product_id);
                                    DBHelper.AddParam("amount", i.amount);
                                    DBHelper.AddParam("status_id", i.status_id);
                                    DBHelper.AddParam("comment", i.comment);
                                    DBHelper.AddParam("is_active", true);
                                    DBHelper.AddParam("is_deleted", false);
                                    DBHelper.AddParam("created_by", i.created_by);
                                    DBHelper.AddParam("created_date", dateNow);
                                    DBHelper.AddParam("modified_by", i.modified_by);
                                    DBHelper.AddParam("modified_date", dateNow);
                                    DBHelper.ExecuteStoreProcedure("insert_job");
                                    res = DBHelper.GetParamOut<Int32>("job_id");

                                    DBHelper.CreateParameters();
                                    DBHelper.AddParamOut("job_history_id", res);
                                    DBHelper.AddParam("job_id", res);
                                    DBHelper.AddParam("amount", i.amount);
                                    DBHelper.AddParam("status_id", i.status_id);
                                    DBHelper.AddParam("parent_job_id", null);
                                    DBHelper.AddParam("comment", i.comment);
                                    DBHelper.AddParam("is_active", true);
                                    DBHelper.AddParam("is_deleted", false);
                                    DBHelper.AddParam("created_by", i.created_by);
                                    DBHelper.AddParam("created_date", dateNow);
                                    DBHelper.AddParam("modified_by", i.modified_by);
                                    DBHelper.AddParam("modified_date", dateNow);
                                    DBHelper.ExecuteStoreProcedure("insert_job_history");
                                }
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
            return res;
        }

        public int UpdateAndCreateJobTransfer(List<param_create_job_transfer> entities)
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
                        DBHelper.AddParam("zone_id", entities.FirstOrDefault().zone_id);
                        var jobListSelect = DBHelper.SelectStoreProcedure<job>("select_list_job_zone").ToList();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("zone_id", entities.FirstOrDefault().new_zone_id);
                        var newjobList = DBHelper.SelectStoreProcedure<job>("select_list_job_zone").ToList();

                        var jobListSelectIds = entities.Where(i => i.is_deleted == false).Select(s => s.job_id).ToList();
                        jobListSelect = jobListSelect.Where(i => jobListSelectIds.Any(a => i.job_id == a)).ToList();



                        entities.ForEach(i =>
                        {
                            if (!i.is_deleted.Value)
                            {
                                var jobSelect = jobListSelect.Where(j => j.job_id == i.job_id).FirstOrDefault();
                                if (jobSelect != null)
                                {
                                    var balanceAmount = jobSelect.amount - i.amount;
                                    var productId = jobSelect.product_id;
                                    var setIsDeleted = balanceAmount > 0 ? jobSelect.is_deleted : true;
                                    DBHelper.CreateParameters();
                                    DBHelper.AddParamOut("success_row", res);
                                    DBHelper.AddParam("job_id", jobSelect.job_id);
                                    DBHelper.AddParam("zone_id", jobSelect.zone_id);
                                    DBHelper.AddParam("product_id", productId);
                                    DBHelper.AddParam("amount", balanceAmount);
                                    DBHelper.AddParam("status_id", jobSelect.status_id);
                                    DBHelper.AddParam("comment", jobSelect.comment);
                                    DBHelper.AddParam("is_active", true);
                                    DBHelper.AddParam("is_deleted", setIsDeleted);
                                    DBHelper.AddParam("modified_by", jobSelect.modified_by);
                                    DBHelper.AddParam("modified_date", dateNow);
                                    DBHelper.ExecuteStoreProcedure("update_job");
                                    res = DBHelper.GetParamOut<Int32>("success_row");


                                    var newJobSelect = newjobList.Where(j => j.product_id == productId && j.is_deleted == false).FirstOrDefault();
                                    if (newJobSelect != null)
                                    {
                                        var setNewAmount = newJobSelect.amount + i.amount;
                                        DBHelper.CreateParameters();
                                        DBHelper.AddParamOut("success_row", res);
                                        DBHelper.AddParam("job_id", newJobSelect.job_id);
                                        DBHelper.AddParam("zone_id", newJobSelect.zone_id);
                                        DBHelper.AddParam("product_id", productId);
                                        DBHelper.AddParam("amount", setNewAmount);
                                        DBHelper.AddParam("status_id", newJobSelect.status_id);
                                        DBHelper.AddParam("comment", newJobSelect.comment);
                                        DBHelper.AddParam("is_active", true);
                                        DBHelper.AddParam("is_deleted", newJobSelect.is_deleted);
                                        DBHelper.AddParam("modified_by", newJobSelect.modified_by);
                                        DBHelper.AddParam("modified_date", dateNow);
                                        DBHelper.ExecuteStoreProcedure("update_job");
                                        res = DBHelper.GetParamOut<Int32>("success_row");
                                    }
                                    else
                                    {
                                        DBHelper.CreateParameters();
                                        DBHelper.AddParamOut("job_id", res);
                                        DBHelper.AddParam("zone_id", i.new_zone_id);
                                        DBHelper.AddParam("product_id", productId);
                                        DBHelper.AddParam("amount", i.amount);
                                        DBHelper.AddParam("status_id", i.status_id);
                                        DBHelper.AddParam("comment", i.comment);
                                        DBHelper.AddParam("is_active", true);
                                        DBHelper.AddParam("is_deleted", false);
                                        DBHelper.AddParam("created_by", i.created_by);
                                        DBHelper.AddParam("created_date", dateNow);
                                        DBHelper.AddParam("modified_by", i.modified_by);
                                        DBHelper.AddParam("modified_date", dateNow);
                                        DBHelper.ExecuteStoreProcedure("insert_job");
                                        res = DBHelper.GetParamOut<Int32>("job_id");
                                    }
                                }
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
            return res;
        }
    }
}
