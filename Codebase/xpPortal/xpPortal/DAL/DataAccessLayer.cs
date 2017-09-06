using HPTools.DataAccess.MSSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace xpPortal.DAL
{
    /// <summary>
    /// Database Access class for Microsoft SQL Server
    /// </summary>
    public class DataAccessLayer
    {
        DBAccess mdb = null;
        private string pDBName = WebConfigurationManager.AppSettings["DBName"];
        private string pDBServer = WebConfigurationManager.AppSettings["DBServer"];

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DataAccessLayer()
        {

            pDBName = WebConfigurationManager.AppSettings["DBName"];
            pDBServer = WebConfigurationManager.AppSettings["DBServer"];
        }
        #region Public Methods


        #endregion
    }
}