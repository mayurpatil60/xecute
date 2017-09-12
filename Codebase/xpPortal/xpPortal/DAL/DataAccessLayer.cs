using XP.DataAccess.MSSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using xpPortal.Models;
using System.Security.Cryptography;
using xpPortal.Helper;

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
            mdb = new DBAccess(pDBServer, pDBName);
        }
        #region Public Methods
        public int SubmitQuery(Query query, LoginViewModel model)
        {
            List<XP.DataAccess.DbParameter> spParameters = new List<XP.DataAccess.DbParameter>();

            spParameters.Add(new XP.DataAccess.DbParameter("Email", model.UserName));
            spParameters.Add(new XP.DataAccess.DbParameter("Query", query.QueryDetail));
            spParameters.Add(new XP.DataAccess.DbParameter("Subject", query.Subject));
            spParameters.Add(new XP.DataAccess.DbParameter("IsAnswered", query.IsAnswered));

            return int.Parse(mdb.ExecuteScalar("spSaveQuery", spParameters).ToString());
        }

        public DataTable GetUserSpecificQueriesAndReplies(string userName)
        {
            List<XP.DataAccess.DbParameter> spParameters = new List<XP.DataAccess.DbParameter>();

            spParameters.Add(new XP.DataAccess.DbParameter("Email", userName));
            
            return mdb.GetDataTable("spGetUserSpecificQueriesAndReplies", spParameters);
        }

        public DataTable GetQueries()
        {
            return mdb.GetDataTable("spGetQueries");
        }

        public void AddApplicantBasicDetails(UserDetails model)
        {
            List<XP.DataAccess.DbParameter> spParameters = new List<XP.DataAccess.DbParameter>();

            spParameters.Add(new XP.DataAccess.DbParameter("Email", model.Email));
            spParameters.Add(new XP.DataAccess.DbParameter("FirstName", model.FirstName));
            spParameters.Add(new XP.DataAccess.DbParameter("MiddleName", model.MiddleName));
            spParameters.Add(new XP.DataAccess.DbParameter("LastName", model.LastName));
            spParameters.Add(new XP.DataAccess.DbParameter("PhoneNo", model.PhoneNo));
            mdb.ExecuteStoredProcedure("spAddApplicantBasicDetails", spParameters);
        }

        public void AddNewJoinee(UserDetails model)
        {
            List<XP.DataAccess.DbParameter> spParameters = new List<XP.DataAccess.DbParameter>();

            spParameters.Add(new XP.DataAccess.DbParameter("Email", model.Email));
            spParameters.Add(new XP.DataAccess.DbParameter("FirstName", model.FirstName));
            spParameters.Add(new XP.DataAccess.DbParameter("MiddleName", model.MiddleName));
            spParameters.Add(new XP.DataAccess.DbParameter("LastName", model.LastName));
            mdb.ExecuteStoredProcedure("spAddNewJoinee", spParameters);
        }

        public void AddApplicantDetailedInfomation(UserDetails model)
        {
            List<XP.DataAccess.DbParameter> spParameters = new List<XP.DataAccess.DbParameter>();

            spParameters.Add(new XP.DataAccess.DbParameter("PassportNo", model.PassportNo));
            spParameters.Add(new XP.DataAccess.DbParameter("CurrentAddress", model.CurrentAddress));
            spParameters.Add(new XP.DataAccess.DbParameter("BloodGroup", model.BloodGroup));
            spParameters.Add(new XP.DataAccess.DbParameter("Gender", model.Gender));
            spParameters.Add(new XP.DataAccess.DbParameter("EmergencyContactNumber", model.EmergencyContactNo));
            spParameters.Add(new XP.DataAccess.DbParameter("DOB", model.DOB));
            spParameters.Add(new XP.DataAccess.DbParameter("Email", model.Email));
            mdb.ExecuteStoredProcedure("spAddApplicantDetailedInfo", spParameters);
        }

        

        public DataTable GetApplicantBasicDetails(string email)
        {
            List<XP.DataAccess.DbParameter> spParameters = new List<XP.DataAccess.DbParameter>();

            spParameters.Add(new XP.DataAccess.DbParameter("Email", email));
            
            return mdb.GetDataTable("spGetApplicantBasicDetails", spParameters);
        }

        public DataTable GetApplicantDetails(string email)
        {
            List<XP.DataAccess.DbParameter> spParameters = new List<XP.DataAccess.DbParameter>();

            spParameters.Add(new XP.DataAccess.DbParameter("Email", email));

            return mdb.GetDataTable("spGetApplicantDetailedInfo", spParameters);
        }
        
        public DataTable GetApplicantDetailedInfo(string email)
        {
            List<XP.DataAccess.DbParameter> spParameters = new List<XP.DataAccess.DbParameter>();

            spParameters.Add(new XP.DataAccess.DbParameter("Email", email));
            
            return mdb.GetDataTable("spGetApplicantDetailedInfo", spParameters);
        }

        public void ResetPassword(LoginViewModel model)
        {
            List<XP.DataAccess.DbParameter> spParameters = new List<XP.DataAccess.DbParameter>();
            
            spParameters.Add(new XP.DataAccess.DbParameter("username", model.UserName));
            spParameters.Add(new XP.DataAccess.DbParameter("password", model.Password));

            mdb.ExecuteStoredProcedure("spResetPassword", spParameters);
        }


      
        public DataTable ValidateUser(LoginViewModel model)
        {
            List<XP.DataAccess.DbParameter> spParameters = new List<XP.DataAccess.DbParameter>();
            
            string salt = new Random().Next().ToString();
            string encryptedPassword=GenericHelper.Encrypt(model.Password,salt);
            spParameters.Add(new XP.DataAccess.DbParameter("username", model.UserName));
            //spParameters.Add(new XP.DataAccess.DbParameter("salt", salt));
            spParameters.Add(new XP.DataAccess.DbParameter("password", model.Password));

            DataTable dt = mdb.GetDataTable("spValidateLoginInfo", spParameters);
            //string decryptPassword=GenericHelper.Decrypt(mdb.ExecuteScalar("spValidateLoginInfo", spParameters).ToString(),salt);
            //bool isValid = decryptPassword.CompareTo(model.Password) == 0 ? true : false;
            return dt;
        }

        //private long int GetSalt()
        //{
        //    List<XP.DataAccess.DbParameter> spParameters = new List<XP.DataAccess.DbParameter>();

        //    spParameters.Add(new XP.DataAccess.DbParameter("username", model.UserName));

        //    mdb.ExecuteScalar("spGetUserSalt", spParameters);
        //}
        public DataTable SaveUser(LoginViewModel model)
        {
            List<XP.DataAccess.DbParameter> spParameters = new List<XP.DataAccess.DbParameter>();
            mdb = new DBAccess(pDBServer, pDBName);
            string salt = new Random().Next().ToString();
            string encryptedPassword = GenericHelper.Encrypt(model.Password, salt);
            spParameters.Add(new XP.DataAccess.DbParameter("username", model.UserName));
            //spParameters.Add(new XP.DataAccess.DbParameter("password", encryptedPassword));
            spParameters.Add(new XP.DataAccess.DbParameter("password", model.Password));

            DataTable dt = mdb.GetDataTable("spSaveNewJoineeInfo", spParameters);

            return dt;
        }

        /// <summary>
        /// Saves User
        /// </summary>
        /// <returns></returns>
        public void AddUser(string user)
        {
            //List<DbParameter> spParameter = new List<DbParameter>();
            //spParameter.Add(new XP.DataAccess.DbParameter("Note", noteContent));
            //spParameter.Add(new XP.DataAccess.DbParameter("User", user));
            //spParameter.Add(new XP.DataAccess.DbParameter("ReleaseId", releaseId));

            //mdb.ExecuteStoredProcedure("spSaveNote", spParameter);
        }

        #endregion

    
    }
}