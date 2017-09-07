using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace XP.DataAccess
{
    /// <summary>
    /// Common interface for database access methods.
    /// </summary>
    public interface IDbManager
    {
        /// <summary>
        /// Retrieves  dataset object from database, based on the stored procedure name passed.
        /// </summary>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        DataSet GetDataset(string procedureName);
        /// <summary>
        /// Executes SQL query passed as parameter.
        /// </summary>
        /// <param name="queryString"></param>
        void ExecuteNonQuery(string queryString);
        /// <summary>
        /// Retrieves dataset object from database, based on the SQL query string passed.
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        DataSet ExecuteQuery(string queryString);
        /// <summary>
        /// Retrieves datatable object from database, based on the stored procedure name passed.
        /// </summary>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        DataTable GetDataTable(string procedureName);
        /// <summary>
        /// Returns Dataset on passing query string and List of parameters
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataSet ExecuteDataset(string queryString, List<DbParameter> parameters);
        /// <summary>
        /// Returns dataset on passing Procedure name and list of parameters as parameters
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataSet GetDataset(string procedureName, List<DbParameter> parameters);
        /// <summary>
        /// Returns single value on passing procedure name and list of parameters as parameters
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object ExecuteScalar(string procedureName, List<DbParameter> parameters);
        /// <summary>
        /// Returns DataTable on passing procedure name and List of parameters as parameters
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataTable GetDataTable(string procedureName, List<DbParameter> parameters);
        /// <summary>
        /// Executes stored procedure on passing procedure name and list pof parameters as arguments
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        void ExecuteStoredProcedure(string procedureName, List<DbParameter> parameters);
        /// <summary>
        /// Executes SQL query on passing query name and list of parameters
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="parameters"></param>
        void ExecuteNonQuery(string queryString, List<DbParameter> parameters);
        /// <summary>
        /// Returns single value on passing procedure name as parameter
        /// </summary>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        object ExecuteScalar(string procedureName);
        /// <summary>
        /// Insert datatable data to table using bulk copy
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        Boolean BulkInsertDataTable(string tableName, DataTable dataTable, List<string> destinationcolumns);

        /// <summary>
        /// Returns nothing based on stored procedure
        /// </summary>
        /// <param name="procedureName"></param>
        void ExecuteProcedure(string procedureName);

    }

}
