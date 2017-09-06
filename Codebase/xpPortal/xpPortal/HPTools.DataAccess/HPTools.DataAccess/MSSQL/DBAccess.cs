using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;

namespace HPTools.DataAccess.MSSQL
{


    /// <summary>
    /// Database Access class for Microsoft SQL Server
    /// </summary>
    public class DBAccess : IDbManager
    {
        #region Declarations

        string pConnectionString = string.Empty;
        SqlConnection pConnection = null;
        SqlCommand pSqlCommand = null;

        #endregion

        #region  Constructors

        /// <summary>
        /// This Constructor creates the instance of class for connection.
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="dataSource"></param>
        public DBAccess(string serverName, string dataSource)
        {
            DBConnection(serverName, dataSource);
        }

        #endregion

        #region Private Methods
        /// <summary>
        ///  Generates the connection string using the provided servername and database name
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="dataSource"></param>
        private void DBConnection(string serverName, string dataSource)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = serverName;
            builder.InitialCatalog = dataSource;
            builder.IntegratedSecurity = true;
            pConnectionString = builder.ConnectionString;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Method returns dataset by taking Sql Querystring as parameter.
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public DataSet ExecuteQuery(string queryString)
        {
            DataSet dataSet = new DataSet();
            pConnection = new SqlConnection(pConnectionString);
            pConnection.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(queryString, pConnection);
            sqlDataAdapter.Fill(dataSet);
            pConnection.Close();
            pConnection.Dispose();
            return dataSet;
        }

        /// <summary>
        /// Executes the query based on the SQL querystring passed as parameter.
        /// </summary>
        /// <param name="queryString"></param>
        public void ExecuteNonQuery(string queryString)
        {
            pConnection = new SqlConnection(pConnectionString);
            pConnection.Open();
            pSqlCommand = new SqlCommand(queryString, pConnection);
            pSqlCommand.ExecuteNonQuery();
            pConnection.Close();
            pConnection.Dispose();
        }

        /// <summary>
        /// Returns dataset based on the stored procedure name passed
        /// </summary>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        public DataSet GetDataset(string procedureName)
        {
            DataSet dataSet = new DataSet();
            pConnection = new SqlConnection(pConnectionString);
            pConnection.Open();
            pSqlCommand = new SqlCommand(procedureName, pConnection);
            pSqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(pSqlCommand);
            sqlAdapter.Fill(dataSet);
            pConnection.Close();
            pConnection.Dispose();
            return dataSet;
        }

        /// <summary>
        /// Returns datatable based on the stored procedure name passed.
        /// </summary>
        /// <param name="procedurename"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string procedureName)
        {
            DataTable dataTable = new DataTable();
            pConnection = new SqlConnection(pConnectionString);
            pConnection.Open();
            pSqlCommand = new SqlCommand(procedureName, pConnection);
            pSqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(pSqlCommand);
            sqlDataAdapter.Fill(dataTable);
            pConnection.Close();
            pConnection.Dispose();
            return dataTable;
        }

        /// <summary>
        /// Returns nothing based on the stored procedure name passed.
        /// </summary>
        /// <param name="procedurename"></param>
        /// <returns></returns>
        public void ExecuteProcedure(string procedureName)
        {
            pConnection = new SqlConnection(pConnectionString);
            pConnection.Open();
            pSqlCommand = new SqlCommand(procedureName, pConnection);
            pSqlCommand.CommandType = CommandType.StoredProcedure;
            pSqlCommand.ExecuteNonQuery();
            pConnection.Close();
            pConnection.Dispose();

        }

        /// <summary>
        /// Returns dataset on passing Sql query and list of parameters
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string queryString, List<DbParameter> parameters)
        {
            pConnection = new SqlConnection(pConnectionString);
            DataSet dataSet = new DataSet();
            pSqlCommand = new SqlCommand(queryString, pConnection);
            pSqlCommand.CommandType = CommandType.Text;
            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            for (int i = 0; i < parameters.Count; i++)
            {
                sqlParameter.Add(new SqlParameter("@" + parameters[i].Name, parameters[i].Value));
            }
            pSqlCommand.Parameters.AddRange(sqlParameter.ToArray());
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(pSqlCommand))
            {
                sqlDataAdapter.Fill(dataSet);
            }
            pConnection.Close();
            return dataSet;
        }

        /// <summary>
        /// Returns single value on passing procedure name and list of parameters as arguments
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string procedureName, List<DbParameter> parameters)
        {
            object resultObject = null;
            pConnection = new SqlConnection(pConnectionString);
            pSqlCommand = new SqlCommand(procedureName, pConnection);
            pSqlCommand.CommandType = CommandType.StoredProcedure;
            pConnection.Open();
            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            for (int i = 0; i < parameters.Count; i++)
            {
                sqlParameter.Add(new SqlParameter("@" + parameters[i].Name, parameters[i].Value));
            }
            pSqlCommand.Parameters.AddRange(sqlParameter.ToArray());
            resultObject = pSqlCommand.ExecuteScalar();
            pConnection.Close();
            return resultObject;
        }

        /// <summary>
        /// Returns Datatable on passing procedure name and list of parameters 
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string procedureName, List<DbParameter> parameters)
        {
            DataTable dataTable = new DataTable();
            pConnection = new SqlConnection(pConnectionString);

            pSqlCommand = new SqlCommand(procedureName, pConnection);
            pSqlCommand.CommandType = CommandType.StoredProcedure;
            pConnection.Open();
            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            for (int i = 0; i < parameters.Count; i++)
            {
                sqlParameter.Add(new SqlParameter("@" + parameters[i].Name, parameters[i].Value));
            }
            pSqlCommand.Parameters.AddRange(sqlParameter.ToArray());
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(pSqlCommand);
            pSqlCommand.CommandTimeout = 120;
            sqlDataAdapter.Fill(dataTable);
            pConnection.Close();
            return dataTable;
        }

        /// <summary>
        /// Executes procedure on passing the procedure name and list of paramters
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        public void ExecuteStoredProcedure(string procedureName, List<DbParameter> parameters)
        {
            pConnection = new SqlConnection(pConnectionString);
            pSqlCommand = new SqlCommand(procedureName, pConnection);
            pSqlCommand.CommandType = CommandType.StoredProcedure;
            pConnection.Open();
            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            for (int i = 0; i < parameters.Count; i++)
            {
                sqlParameter.Add(new SqlParameter("@" + parameters[i].Name, parameters[i].Value));
            }
            pSqlCommand.Parameters.AddRange(sqlParameter.ToArray());
            pSqlCommand.CommandTimeout = 1800;
            pSqlCommand.ExecuteNonQuery();
            pConnection.Close();
        }

        /// <summary>
        /// Executes query on passing SQL query and list of parameters
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="parameters"></param>
        public void ExecuteNonQuery(string queryString, List<DbParameter> parameters)
        {
            pConnection = new SqlConnection(pConnectionString);
            pSqlCommand = new SqlCommand(queryString, pConnection);
            pSqlCommand.CommandType = CommandType.Text;
            pConnection.Open();
            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            for (int i = 0; i < parameters.Count; i++)
            {
                sqlParameter.Add(new SqlParameter("@" + parameters[i].Name, parameters[i].Value));
            }
            pSqlCommand.Parameters.AddRange(sqlParameter.ToArray());
            pSqlCommand.ExecuteNonQuery();
            pConnection.Close();
        }

        /// <summary>
        /// Returns single value on passing procedure name as argument
        /// </summary>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        public object ExecuteScalar(string procedureName)
        {
            object resultObject = null;
            pConnection = new SqlConnection(pConnectionString);
            SqlCommand cmd = new SqlCommand(procedureName, pConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            pConnection.Open();
            cmd.CommandTimeout = 300;
            resultObject = cmd.ExecuteScalar();
            pConnection.Close();
            return resultObject;
        }

        /// <summary>
        /// Returns dataset on passing procedure name and list of parameters
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet GetDataset(string procedureName, List<DbParameter> parameters)
        {
            pConnection = new SqlConnection(pConnectionString);
            DataSet dataSet = new DataSet();
            pSqlCommand = new SqlCommand(procedureName, pConnection);
            pSqlCommand.CommandType = CommandType.StoredProcedure;
            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            for (int i = 0; i < parameters.Count; i++)
            {
                sqlParameter.Add(new SqlParameter("@" + parameters[i].Name, parameters[i].Value));
            }
            pSqlCommand.Parameters.AddRange(sqlParameter.ToArray());

            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(pSqlCommand))
            {
                sqlDataAdapter.Fill(dataSet);
            }
            return dataSet;
        }

        /// <summary>
        /// Bulk insert datatable rows to database table on passing Database table name,
        /// Datatable with data to insert 
        /// And List of destination column names
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataTable"></param>
        /// <param name="destinationColumns"></param>
        /// <returns></returns>
        public bool BulkInsertDataTable(string tableName, DataTable dataTable, List<string> destinationColumns)
        {
            bool pIsDataImported = false;
            pConnection = new SqlConnection(pConnectionString);
            pConnection.Open();
            using (SqlBulkCopy pCopy = new SqlBulkCopy(pConnection))
            {
                foreach (string destinationColumn in destinationColumns)
                {
                    pCopy.ColumnMappings.Add(destinationColumn, destinationColumn);
                }
                pCopy.DestinationTableName = tableName;
                pCopy.WriteToServer(dataTable);
            }
            pConnection.Close();
            pConnection.Dispose();
            pIsDataImported = true;
            return pIsDataImported;
        }
        #endregion
    }
}
