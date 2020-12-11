using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Definitions;
using Entity;

namespace DAO.Backend
{
    public partial class DataDao
    {

        public List<status> GetStatusList()
        {
            List<status> res = new List<status>();

            try
            {
                using (DBHelper.CreateConnection())
                {
                    try
                    {
                        DBHelper.OpenConnection();

                        res = DBHelper.SelectStoreProcedure<status>("select_list_status").ToList();
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

            return res;
        }    
    }
}
