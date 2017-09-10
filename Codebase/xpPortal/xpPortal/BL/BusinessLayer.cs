using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using xpPortal.DAL;
using xpPortal.Models;

namespace xpPortal.BL
{
    public class BusinessLayer
    {
        public bool ValidateUser(LoginViewModel model, out bool isPasswordSet)
        {
            bool isValidUser = false;
            isPasswordSet = false;

            DataAccessLayer dalObject = new DataAccessLayer();

            DataTable dt= dalObject.ValidateUser(model);
            isValidUser = int.Parse(dt.Rows[0][0].ToString())>0?true:false;
            isPasswordSet = bool.Parse(dt.Rows[0]["IsPasswordSet"].ToString());

            return isValidUser;
        }

        public void SubmitQuery(string query, LoginViewModel model)
        {
            DataAccessLayer dalObject = new DataAccessLayer();
            dalObject.SubmitQuery(query,model);
        }

        public List<Query> GetQueries()
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            DataTable dt = dalObject.GetQuery();
            List<Query> queryList = new List<Query>();

            foreach (DataRow dr in dt.Rows)
            {
                Query queryObj = new Query();
                queryObj.Id = int.Parse(dr["QueryId"].ToString());
                queryObj.Subject = dr["Subject"].ToString();
                queryObj.DateCreated = DateTime.Parse(dr["DateCreated"].ToString()).ToShortDateString();
                queryObj.IsAnswered = bool.Parse(dr["IsAnswered"].ToString());
                queryList.Add(queryObj);
            }
            
            return queryList;
        }

        public void AddApplicantBasicDetailsAndSendMail(UserDetails userDetails)
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            dalObject.AddApplicantBasicDetails(userDetails);
        }

        public UserDetails GetApplicantBasicDetails(string email)
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            DataTable dt= dalObject.GetApplicantBasicDetails(email);

            UserDetails details = new UserDetails();

            details.FirstName = dt.Rows[0]["FirstName"].ToString();
            details.LastName = dt.Rows[0]["LastName"].ToString();
            details.PhoneNo = dt.Rows[0]["PhoneNo"]!=null?dt.Rows[0]["PhoneNo"].ToString():"";
            details.Email = dt.Rows[0]["EmailId"].ToString();
            return details;
        }


        public UserDetails GetApplicantDetails(string email)
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            DataTable dt = dalObject.GetApplicantDetails(email);

            UserDetails details = new UserDetails();

            details.FirstName = dt.Rows[0]["FirstName"].ToString();
            details.LastName = dt.Rows[0]["LastName"].ToString();
            details.PhoneNo = dt.Rows[0]["PhoneNo"] != null ? dt.Rows[0]["PhoneNo"].ToString() : "";
            details.Email = dt.Rows[0]["EmailId"].ToString();
            return details;
        }

        public void AddApplicantDetailedInfomation(UserDetails userDetails)
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            dalObject.AddApplicantDetailedInfomation(userDetails);
        }

        public void SendMailToApplicant(UserDetails userDetails)
        {
            
        }
        
        public void ResetPassword(LoginViewModel model)
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            dalObject.ResetPassword(model);
        }
    }
}