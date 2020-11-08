using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IServiceRepository<T>
    {
        List<T> GetDataAll();
        T GetDataByID(Int64 id);
        List<T> GetDataByCondition(T entity);
        List<T> GetDataByCondition(T entity, Int32 index);
        Int32 InsertData(T entity);
        Int32 UpdateData(T entity);
    }
}
