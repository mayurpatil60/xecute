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
            if (dt != null && dt.Rows.Count != 0 && int.Parse(dt.Rows[0][0].ToString()) != 0)
            {
                isValidUser = int.Parse(dt.Rows[0][0].ToString()) > 0 ? true : false;
                isPasswordSet = bool.Parse(dt.Rows[0]["IsPasswordSet"].ToString());
            }
            return isValidUser;
        }

        public void SubmitQuery(Query query, LoginViewModel model)
        {
            DataAccessLayer dalObject = new DataAccessLayer();
            dalObject.SubmitQuery(query,model);
        }

        public List<Query> GetQueries()
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            DataTable dt = dalObject.GetQueries();
            List<Query> queryList = new List<Query>();

            foreach (DataRow dr in dt.Rows)
            {
                Query queryObj = new Query();
                queryObj.Id = int.Parse(dr["QueryId"].ToString());
                queryObj.Subject = dr["Subject"].ToString();
                queryObj.QueryDetail= dr["Query"].ToString();
                queryObj.DateCreated = DateTime.Parse(dr["DateCreated"].ToString()).ToString();
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

        public void AddNewJoinee(UserDetails userDetails)
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            dalObject.AddNewJoinee(userDetails);
        }

        public DataRowCollection GetNewJoineeList()
        {
            DataAccessLayer dalObject = new DataAccessLayer();

           DataTable da= dalObject.GetNewJoineeList();
            DataRowCollection drc = da.Rows;
            return drc;
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
            details.BloodGroup = dt.Rows[0]["BloodGroup"] != null ? dt.Rows[0]["BloodGroup"].ToString():"";
            details.EmergencyContactNo = dt.Rows[0]["EmergencyContactNumber"] !=null ? dt.Rows[0]["EmergencyContactNumber"].ToString():"";
            details.CurrentAddress = dt.Rows[0]["CurrentAddress"] !=null? dt.Rows[0]["CurrentAddress"].ToString():"";
            details.Gender = dt.Rows[0]["Gender"] !=null? dt.Rows[0]["Gender"].ToString():"";
            details.PassportNo = dt.Rows[0]["PassportNumber"] !=null? dt.Rows[0]["PassportNumber"].ToString():"";
            if (dt.Rows[0]["DOB"].ToString() != "")
            {
                details.DOB = DateTime.Parse(dt.Rows[0]["DOB"].ToString());
            }
            else
            {
                details.DOB = DateTime.Now;
            }
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