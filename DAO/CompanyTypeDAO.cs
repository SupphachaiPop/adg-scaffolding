using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Definitions;
using Entity.Backend;

namespace DAO.Backend
{
    public class CompanyTypeDAO : IDAORepository<CompanyTypeEntity>
    {
        DBHelper DBHelper = null;
        string conn = "ConnectionStringBackend";

        public CompanyTypeDAO()
        {
            DBHelper = new DBHelper();
        }

        public List<CompanyTypeEntity> GetDataAll()
        {
            List<CompanyTypeEntity> sw_CompanyTypeEntities = new List<CompanyTypeEntity>();

            try
            {
                using (DBHelper.CreateConnection(conn))
                {
                    try
                    {
                        DBHelper.OpenConnection();
                        sw_CompanyTypeEntities = DBHelper.SelectStoreProcedure<CompanyTypeEntity>("select_sw_company_type").ToList();
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

            return sw_CompanyTypeEntities;
        }

        public List<CompanyTypeEntity> GetDataByCondition(CompanyTypeEntity entity)
        {
            throw new NotImplementedException();
        }

        public List<CompanyTypeEntity> GetDataByCondition(CompanyTypeEntity entity, int Index)
        {
            throw new NotImplementedException();
        }

        public CompanyTypeEntity GetDataByID(long id)
        {
            throw new NotImplementedException();
        }

        public int InsertData(CompanyTypeEntity entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateData(CompanyTypeEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
