using System;
using System.Collections.Generic;
using System.Text;
using DAO.Backend;
using Entity.Backend;

namespace Service.Backend
{
    public class swBranchService : IServiceRepository<swBranchEntity>
    {
        swBranchDAO swBranchDAO = new swBranchDAO();

        public List<swBranchEntity> GetDataAll()
        {
            return swBranchDAO.GetDataAll();
        }

        public swBranchEntity GetDataByID(long id)
        {
            return swBranchDAO.GetDataByID(id);
        }

        public swBranchEntity GetDataByID(long id, int company_id)
        {
            return swBranchDAO.GetDataByID(id, company_id);
        }

        public List<swBranchEntity> GetDataByCondition(swBranchEntity entity)
        {
            throw new NotImplementedException();
        }
        public List<swBranchEntity> GetDataByCondition(swBranchEntity entity, int index)
        {
            throw new NotImplementedException();
        }

        public List<swBranchEntity> GetDataByCondition(paramSwBranchEntity param)
        {
            return swBranchDAO.GetDataByCondition(param);
        }


        public int InsertData(swBranchEntity entity)
        {
            return swBranchDAO.InsertData(entity);
        }

        public int UpdateData(swBranchEntity entity)
        {
            return swBranchDAO.UpdateData(entity);
        }

        public int UpdateDataStatus(swBranchEntity entity)
        {
            return swBranchDAO.UpdateDataStatus(entity);
        }

        public int DeleteData(swBranchEntity entity)
        {
            return swBranchDAO.DeleteData(entity);
        }
    }
}
