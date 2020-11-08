using System;
using System.Collections.Generic;
using System.Text;

namespace DAO
{
    public interface IDAORepository<T>
    {
        List<T> GetDataAll();
        T GetDataByID(Int64 id);
        List<T> GetDataByCondition(T entity);
        List<T> GetDataByCondition(T entity, Int32 Index);
        Int32 InsertData(T entity);
        Int32 UpdateData(T entity);
    }
}
