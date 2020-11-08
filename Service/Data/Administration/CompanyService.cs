using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity.Backend;

namespace Service.Backend
{
    public class CompanyService : IServiceRepository<CompanyEntity>
    {
        CompanyDAO CompanyDAO = new CompanyDAO();

        public List<CompanyEntity> GetDataAll()
        {
            return CompanyDAO.GetDataAll();
        }

        public CompanyEntity GetDataByID(long id)
        {
            return CompanyDAO.GetDataByID(id);
        }

        public List<CompanyEntity> GetDataByCondition(CompanyEntity entity)
        {
            throw new NotImplementedException();
        }
        public List<CompanyEntity> GetDataByCondition(CompanyEntity entity, int index)
        {
            throw new NotImplementedException();
        }

        public List<CompanyEntity> GetDataByCondition(paramCompanyEntity param)
        {
            return CompanyDAO.GetDataByCondition(param);
        }


        public int InsertData(CompanyEntity entity)
        {
            return CompanyDAO.InsertData(entity);
        }

        public int UpdateData(CompanyEntity entity)
        {
            return CompanyDAO.UpdateData(entity);
        }

        public int UpdateDataStatus(CompanyEntity entity)
        {
            return CompanyDAO.UpdateDataStatus(entity);
        }

        public int DeleteData(CompanyEntity entity)
        {
            return CompanyDAO.DeleteData(entity);
        }

        public bool CheckIsReferred(CompanyEntity companyEntity)
        {
            return CompanyDAO.CheckIsReferred(companyEntity);
        }
    }
}
