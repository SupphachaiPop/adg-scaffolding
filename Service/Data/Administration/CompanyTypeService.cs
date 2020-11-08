using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity.Backend;

namespace Service.Backend
{
    public class CompanyTypeService : IServiceRepository<CompanyTypeEntity>
    {
        CompanyTypeDAO companyTypeDAO = new CompanyTypeDAO();

        public List<CompanyTypeEntity> GetDataAll()
        {
            return companyTypeDAO.GetDataAll();
        }

        public List<CompanyTypeEntity> GetDataByCondition(CompanyTypeEntity entity)
        {
            throw new NotImplementedException();
        }

        public List<CompanyTypeEntity> GetDataByCondition(CompanyTypeEntity entity, int index)
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
