using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity.Backend;

namespace Service.Backend
{
    public class swBranchTypeService : IServiceRepository<swBranchTypeEntity>
    {
        swBranchTypeDAO swBranchTypeDAO = new swBranchTypeDAO();

        public List<swBranchTypeEntity> GetDataAll()
        {
            return swBranchTypeDAO.GetDataAll();
        }

        public List<swBranchTypeEntity> GetDataByCondition(swBranchTypeEntity entity)
        {
            throw new NotImplementedException();
        }

        public List<swBranchTypeEntity> GetDataByCondition(swBranchTypeEntity entity, int index)
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
