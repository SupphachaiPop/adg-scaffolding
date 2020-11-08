using DAO.Backend.MasterData;
using Entity.Backend;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Backend.MasterData
{
    public class CategoryService
    {

        CategoryDAO CategoryDAO = new CategoryDAO();
        public SwCategoryEntity GetDataByID(int id)
        {
            return CategoryDAO.GetDataByID(id);
        }

    }
}
