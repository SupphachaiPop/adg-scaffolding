using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Definitions
{
    public class DBHelper
    {
        private SqlCommand command = null;
        private SqlConnection connection = null;
        private SqlTransaction transaction = null;
        private DynamicParameters dynamicParameters = null;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SqlConnection CreateConnection()
        {
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] == null)
                return null;
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            return connection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public SqlConnection CreateConnection(String ConnectionString)
        {
            if (ConfigurationManager.ConnectionStrings[ConnectionString] == null)
                return null;
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString);
            return connection;
        }

        public DynamicParameters CreateParameters()
        {
            dynamicParameters = new DynamicParameters();
            return dynamicParameters;
        }

        /// <summary>
        /// 
        /// </summary>
        public void OpenConnection()
        {
            if (connection != null)
            {
                connection.Open();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
        }

        public T GetParamOut<T>(String parameterName)
        {
            return dynamicParameters.Get<T>(parameterName);
        }

        public void AddParam(String parameterName, Object value)
        {
            if (dynamicParameters != null)
            {
                if (dynamicParameters.ParameterNames.Contains(parameterName))
                {

                }
                else
                {
                    if (CheckIsNullOrEmpty(value))
                    {
                        dynamicParameters.Add(parameterName, value);
                    }
                    else
                    {
                        dynamicParameters.Add(parameterName, value);
                    }
                }
            }
        }

        public void AddParamOut(String parameterName, Object value)
        {
            if (dynamicParameters != null)
            {
                if (dynamicParameters.ParameterNames.Contains(parameterName))
                {

                }
                else
                {
                    if (CheckIsNullOrEmpty(value))
                    {
                        dynamicParameters.Add(parameterName, value, direction: ParameterDirection.Output);
                    }
                    else
                    {
                        dynamicParameters.Add(parameterName, value, direction: ParameterDirection.Output);
                    }
                }
            }
        }

        private Boolean CheckIsNullOrEmpty(Object value)
        {
            if (value is String && String.IsNullOrEmpty((String)value) || value is null)
                return true;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Int32 ExecuteNonQuery()
        {
            if (command == null) return 0;
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <returns></returns>
        public Int32 ExecuteStoreProcedure(String storeProcedure)
        {
            return ExecuteStoreProcedure(storeProcedure, dynamicParameters, transaction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Int32 ExecuteStoreProcedure(String storeProcedure, DynamicParameters param)
        {
            return ExecuteStoreProcedure(storeProcedure, param, transaction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeProcedure"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Int32 ExecuteStoreProcedure(String storeProcedure, DynamicParameters param, IDbTransaction transaction)
        {
            return connection.Execute(storeProcedure, param, transaction, commandType: CommandType.StoredProcedure, commandTimeout: 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SqlTransaction BeginTransaction()
        {
            transaction = connection.BeginTransaction();
            return transaction;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CommitTransaction()
        {
            if (transaction != null)
            {
                transaction.Commit();
            }
        }

        public IEnumerable<T> Select<T>(String query)
        {
            return Select<T>(query, dynamicParameters);
        }

        public T SelectFirst<T>(String query)
        {
            return SelectFirst<T>(query, dynamicParameters);
        }

        public T SelectFirst<T>(String query, DynamicParameters param)
        {
            IEnumerable<T> list = connection.Query<T>(query, param);
            if (list.Count() == 0) return default(T);
            return list.First<T>();
        }

        public IEnumerable<T> Select<T>(String query, DynamicParameters param)
        {
            return connection.Query<T>(query, param);
        }

        public IEnumerable<T> SelectStoreProcedure<T>(String storeProcedure)
        {
            return SelectStoreProcedure<T>(storeProcedure, dynamicParameters);
        }

        public IEnumerable<T> SelectStoreProcedure<T>(String storeProcedure, DynamicParameters param)
        {
            if (transaction != null)
            {
                return connection.Query<T>(storeProcedure, param, commandType: CommandType.StoredProcedure, transaction: transaction);
            }
            else
            {
                return connection.Query<T>(storeProcedure, param, commandType: CommandType.StoredProcedure);
            }

        }

        public T SelectStoreProcedureFirst<T>(String storeProcedure)
        {
            return SelectStoreProcedureFirst<T>(storeProcedure, dynamicParameters);
        }

        public T SelectStoreProcedureFirst<T>(String storeProcedure, DynamicParameters param)
        {
            IEnumerable<T> list = null;

            if (transaction != null)
            {
                list = connection.Query<T>(storeProcedure, param, commandType: CommandType.StoredProcedure, transaction: transaction);
            }
            else
            {
                list = connection.Query<T>(storeProcedure, param, commandType: CommandType.StoredProcedure);
            }

            if (list.Count() == 0) return default(T);
            return list.First<T>();
        }


        //public T3 SelectStoreProcedureM<T1, T2, T3>(string storeProcedure, DynamicParameters param)
        //{
        //    IEnumerable<T3> list = connection.Query<T1, T2, T3>(storeProcedure, param, commandType: CommandType.StoredProcedure);
        //    if (list.Count() == 0) return default(T3);
        //    return list.First<T3>();
        //    throw new NotImplementedException();
        //}
    }
}
