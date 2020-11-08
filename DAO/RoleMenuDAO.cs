using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Definitions;
using Entity.Backend;

namespace DAO.Backend
{
    public class RoleMenuDAO : IDAORepository<RoleMenuEntity>
    {
        DBHelper DBHelper = null;
        string conn = "ConnectionStringBackend";
        DateTime dateNow = DateTime.Now;


        public RoleMenuDAO()
        {
            DBHelper = new DBHelper();
        }

        public List<RoleMenuEntity> GetDataAll()
        {
            List<RoleMenuEntity> sw_RoleMenuEntities = new List<RoleMenuEntity>();

            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();

                        sw_RoleMenuEntities = DBHelper.SelectStoreProcedure<RoleMenuEntity>("select_sw_RoleMenu").ToList();
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

            return sw_RoleMenuEntities;
        }

        public List<RoleMenuEntity> GetDataByCondition(int role_id, int company_id)
        {
            List<RoleMenuEntity> RoleMenuListEntities = null;

            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("role_id", role_id);
                        DBHelper.AddParam("company_id", company_id);
                        RoleMenuListEntities = DBHelper.SelectStoreProcedure<RoleMenuEntity>("select_sw_role_item").ToList();
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
            return RoleMenuListEntities;
        }
        
        public List<RoleMenuMasterEntity> GetMenuByRole(int role_id)
        {
            List<RoleMenuMasterEntity> roleMenuMasters = null;

            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        DBHelper.CreateParameters();
                        DBHelper.AddParam("role_id", role_id);
                        roleMenuMasters = DBHelper.SelectStoreProcedure<RoleMenuMasterEntity>("select_sw_menu_by_role").ToList();
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
            return roleMenuMasters;
        }

        public bool CheckIsReferred(RoleMenuEntity RoleMenuEntity)
        {
            throw new NotImplementedException();
        }

        public RoleMenuEntity GetDataByID(long id)
        {
            throw new NotImplementedException();
        }

        public int InsertData(RoleMenuEntity entity)
        {
            throw new NotImplementedException();
        }
        public int UpdateData(RoleMenuEntity entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateDataStatus(RoleMenuEntity entity)
        {
            throw new NotImplementedException();
        }
        public int DeleteData(RoleMenuEntity entity)
        {
            throw new NotImplementedException();
        }
        public List<RoleMenuEntity> GetDataByCondition(RoleMenuEntity entity)
        {
            throw new NotImplementedException();
        }

        public List<RoleMenuEntity> GetDataByCondition(RoleMenuEntity entity, int Index)
        {
            throw new NotImplementedException();
        }
    }
}
