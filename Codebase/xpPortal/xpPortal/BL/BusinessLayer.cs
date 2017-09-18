﻿using System;
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

        public int SubmitQuery(Query query, LoginViewModel model)
        {
            DataAccessLayer dalObject = new DataAccessLayer();
            int recordsInserted= dalObject.SubmitQuery(query,model);

            return recordsInserted;
        }

        public List<Query> GetUserSpecificQueriesAndReplies(string userName)
        {
            DataAccessLayer dalObject = new DataAccessLayer();
            DataTable dt = dalObject.GetUserSpecificQueriesAndReplies(userName);

            List<Query> queryList = new List<Query>();

            foreach (DataRow dr in dt.Rows)
            {
                Query queryObj = new Query();
                queryObj.Id = int.Parse(dr["QueryId"].ToString());
                queryObj.Subject = dr["Subject"].ToString();
                queryObj.QueryDetail = dr["Query"].ToString();
                queryObj.CreatedDateTime = DateTime.Parse(dr["CreatedDateTime"].ToString()).ToString();
                queryObj.IsAnswered = bool.Parse(dr["IsAnswered"].ToString());
                queryObj.Reply = dr["Reply"] != DBNull.Value ? (dr["Reply"].ToString()) : "";
                queryObj.ReplyDateTime = dr["ReplyDateTime"] != DBNull.Value ? DateTime.Parse(dr["ReplyDateTime"].ToString()).ToString() : "";
                queryList.Add(queryObj);
            }

            return queryList;
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
                queryObj.CreatedDateTime = DateTime.Parse(dr["CreatedDateTime"].ToString()).ToString();
                queryObj.IsAnswered = bool.Parse(dr["IsAnswered"].ToString());
                queryList.Add(queryObj);
            }
            
            return queryList;
        }

        public List<Buddy> GetBuddyList()
        {
            DataAccessLayer dalObject = new DataAccessLayer();
            DataTable dt = dalObject.GetBuddyList();
            List<Buddy> buddyList = new List<Buddy>();

            foreach (DataRow dr in dt.Rows)
            {
                Buddy buddy = new Buddy();
                buddy.Id = int.Parse(dr["Id"].ToString());
                buddy.Name = dr["Name"].ToString();
                buddy.PhoneNo = dr["Phone"].ToString();
                buddy.Email = dr["Email"].ToString();
                buddy.Rating = dr["Rating"].ToString();
                buddy.Location = dr["Location"].ToString();
                buddyList.Add(buddy);
            }

            return buddyList;
        }

        //public DataTable SaveBuddy()
        //{
        //    return mdb.GetDataTable("spSaveBuddy");
        //}

        public void AddApplicantBasicDetailsAndSendMail(UserDetails userDetails)
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            dalObject.AddApplicantBasicDetails(userDetails);
        }

         public void SaveFeedback(string feedback,string email)
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            dalObject.SaveFeedback(feedback, email);
        }

        public void SaveReferred(ReferAndEarnModel model)
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            dalObject.SaveReferred(model);
        }

        public DataRowCollection GetJobListForReferAndEarn()
        {
            DataAccessLayer dalObject = new DataAccessLayer();
            DataTable da = dalObject.GetJobListForReferAndEarn();
            DataRowCollection drc = da.Rows;
            return drc;
        }       

        public void AddNewJoinee(AddNewJoineeModel userDetails)
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            dalObject.AddNewJoinee(userDetails);
        }

        public void SaveNewJob(NewJobRefer newJobRefer)
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            dalObject.SaveNewJob(newJobRefer);
        }
        public DataRowCollection GetFeedbackList()
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            DataTable da= dalObject.GetFeedbackList();
            DataRowCollection drc = da.Rows;
            return drc;
        }

        public DataRowCollection GetNewJoineeList()
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            DataTable da = dalObject.GetNewJoineeList();
            DataRowCollection drc = da.Rows;
            return drc;
        }
        
        //public List<UserDetails> GetNewJoineeList2()
        //{
        //    DataAccessLayer dalObject = new DataAccessLayer();
        //    List<UserDetails> list = new List<UserDetails>();
        //    DataTable da = dalObject.GetNewJoineeList();

        //    foreach (DataRow dr in da.Rows)
        //    {
        //        list.Add(new UserDetails { FirstName = dr["FirstName"].ToString(), MiddleName = dr["MiddleName"].ToString(), LastName = dr["LastName"].ToString(), Email = dr["Email"].ToString(), JoiningDate = DateTime.Parse(dr["JoiningDate"].ToString()) });
        //    }
        //    return list;
        //}
        public UserDetails GetApplicantBasicDetails(string email)
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            DataTable dt= dalObject.GetApplicantBasicDetails(email);

            UserDetails details = new UserDetails();
            if (dt.Rows.Count > 0)
            {
                details.FirstName = dt.Rows[0]["FirstName"].ToString();
                details.LastName = dt.Rows[0]["LastName"].ToString();
                details.PhoneNo = dt.Rows[0]["PhoneNo"] != null ? dt.Rows[0]["PhoneNo"].ToString() : "";
                details.Email = dt.Rows[0]["EmailId"].ToString();
                if (dt.Rows[0]["JoiningDate"] != DBNull.Value)
                {
                    details.JoiningDate = DateTime.Parse(dt.Rows[0]["JoiningDate"].ToString());
                }
            }
            return details;
        }


        public UserDetails GetApplicantDetails(string email)
        {
            DataAccessLayer dalObject = new DataAccessLayer();
            string emailWithoutSpace = email.Trim();
            DataTable dt = dalObject.GetApplicantDetails(emailWithoutSpace);

            UserDetails details = new UserDetails();
            if (dt.Rows.Count > 0)
            {
                details.FirstName = dt.Rows[0]["FirstName"].ToString() != null ? dt.Rows[0]["FirstName"].ToString() : "";
                details.LastName = dt.Rows[0]["LastName"].ToString() != null ? dt.Rows[0]["LastName"].ToString() : ""; ;
                details.PhoneNo = dt.Rows[0]["PhoneNo"] != null ? dt.Rows[0]["PhoneNo"].ToString() : "";
                details.Email = dt.Rows[0]["EmailId"].ToString();
                details.BloodGroup = dt.Rows[0]["BloodGroup"] != null ? dt.Rows[0]["BloodGroup"].ToString() : "";
                details.EmergencyContactNo = dt.Rows[0]["EmergencyContactNumber"] != null ? dt.Rows[0]["EmergencyContactNumber"].ToString() : "";
                details.CurrentAddress = dt.Rows[0]["CurrentAddress"] != null ? dt.Rows[0]["CurrentAddress"].ToString() : "";
                details.Gender = dt.Rows[0]["Gender"] != null ? dt.Rows[0]["Gender"].ToString() : "";
                details.PassportNo = dt.Rows[0]["PassportNumber"] != null ? dt.Rows[0]["PassportNumber"].ToString() : "";
                if (dt.Rows[0]["DOB"]!=DBNull.Value)
                {
                    details.DOB = DateTime.Parse(dt.Rows[0]["DOB"].ToString());
                }
                
            }
            return details;
        }

        public void VerificationDetail(string EmailId)
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            DataTable dt = dalObject.VerificationDetail(EmailId);

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

        public DataTable GetUserDetails(string username)
        {          
            DataAccessLayer dalObject = new DataAccessLayer();
            DataTable dt = dalObject.GetUserDetails(username);            
            return dt;
        }

        public void InsertIntoTable(string InsertQuery)
        {
            DataAccessLayer dalObject = new DataAccessLayer();
            dalObject.InsertIntoTable(InsertQuery);
        }
        public DataRowCollection GetDocumentListByUser(string emailID)
        {
            DataAccessLayer dalObject = new DataAccessLayer();
            DataSet ds = dalObject.GetDataFromQuery("select doc.DocumentId, doc.documentLink as DocumentName, doc.DocumentType, '../../UploadedDocuments/'+ doc.DocumentLink as DocumentLink, case doc.Status when 0 then 'Not Submitted' when 1 then 'Submitted' when 2 then 'Approved' when 3 then 'Rejected' ELSE 'Not Submitted' END DocStatus, 'Verify' As Approve from Document doc inner join login on doc.UserLoginID = Login.Id where Login.UserName = '" + emailID +"' order by doc.status desc;");            
            if (ds != null && ds.Tables.Count > 0)
            {
                DataRowCollection drc = ds.Tables[0].Rows;
                return drc;
            }            
            return null;
        }

        public int SubmitQueryReply(Query reply, LoginViewModel model)
        {
            DataAccessLayer dalObject = new DataAccessLayer();
            int recordsInserted = dalObject.SubmitQueryReply(reply, model);

            return recordsInserted;
        }

        public List<UserDetails> GetJoineeQueries()
        {
            DataAccessLayer dalObject = new DataAccessLayer();

            DataTable dt = dalObject.GetJoineeQueries();
            List<UserDetails> joineeQueries = new List<UserDetails>();

            foreach (DataRow dr in dt.Rows)
            {
                UserDetails joineeQueryObj = new UserDetails();
                joineeQueryObj.FirstName = dr["FirstName"].ToString();
                joineeQueryObj.LastName = dr["LastName"].ToString();
                if (dr["JoiningDate"] != null)
                {
                    joineeQueryObj.JoiningDate = DateTime.Parse(dr["JoiningDate"].ToString());
                }
                joineeQueryObj.Email = dr["Email"].ToString();

                joineeQueries.Add(joineeQueryObj);
            }
            return joineeQueries;
        }
    }
}