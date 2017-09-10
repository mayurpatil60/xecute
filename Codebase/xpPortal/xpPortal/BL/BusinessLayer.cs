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

        public void SubmitQuery(string query)
        {
            DataAccessLayer dalObject = new DataAccessLayer();
            dalObject.SubmitQuery(query);
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

            return details;
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