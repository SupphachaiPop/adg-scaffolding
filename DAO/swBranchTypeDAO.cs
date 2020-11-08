using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Definitions;
using Entity.Backend;

namespace DAO.Backend
{
    public class swBranchTypeDAO : IDAORepository<swBranchTypeEntity>
    {
        DBHelper DBHelper = null;
        string conn = "ConnectionStringBackend";

        public swBranchTypeDAO()
        {
            DBHelper = new DBHelper();
        }

        public List<swBranchTypeEntity> GetDataAll()
        {
            List<swBranchTypeEntity> swBranchTypeEntities = new List<swBranchTypeEntity>();

            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        swBranchTypeEntities = DBHelper.SelectStoreProcedure<swBranchTypeEntity>("select_sw_branch_type").ToList();
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

            return swBranchTypeEntities;
        }

        public List<swBranchTypeEntity> GetDataByCondition(swBranchTypeEntity entity)
        {
            throw new NotImplementedException();
        }

        public List<swBranchTypeEntity> GetDataByCondition(swBranchTypeEntity entity, int Index)
        {
            throw new NotImplementedException();
        }

        public swBranchTypeEntity GetDataByID(long id)
        {
            throw new NotImplementedException();
        }

        public int InsertData(swBranchTypeEntity entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateData(swBranchTypeEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
