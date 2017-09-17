﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xpPortal.BL;
using xpPortal.Models;

namespace xpPortal.Controllers
{
    public class JoineeController : Controller
    {
        // GET: Joinee
      
        // GET: UserDetailedInfo
        public ActionResult Index()
        {
            UserDetails bcInfo = new UserDetails();
            BusinessLayer blObject = new BusinessLayer();
            string email = Session["userName"].ToString();
            bcInfo = blObject.GetApplicantBasicDetails(email);

            #region yeardata
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x =>
             new SelectListItem()
             {
                 Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1],
                 Value = x.ToString()
             }), "Value", "Text");

            ViewBag.Years = new SelectList(Enumerable.Range(1980, 2000).Select(x =>
               new SelectListItem()
               {
                   Text = x.ToString(),
                   Value = x.ToString()
               }), "Value", "Text");

            ViewBag.Days = new SelectList(Enumerable.Range(1, 31).Select(x =>
              new SelectListItem()
              {
                  Text = x.ToString(),
                  Value = x.ToString()
              }), "Value", "Text");
            #endregion

            return View("Index", bcInfo);
        }

        public ActionResult Profile()
        {

            return View("");
        }

        public ActionResult Buddy()
        {

            return View("");
        }
        public ActionResult RelocationAssistant()
        {
            return View();
        }
      
        public ActionResult Events()
        {

            return View("");
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult SaveBasicInfo(UserDetails basicInfo)
        {
            BusinessLayer blObject = new BusinessLayer();
            blObject.AddApplicantBasicDetailsAndSendMail(basicInfo);
          
            basicInfo = blObject.GetApplicantDetails(basicInfo.Email);

            #region yeardata
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x =>
         new SelectListItem()
         {
             Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1],
             Value = x.ToString()
         }), "Value", "Text");

            ViewBag.Years = new SelectList(Enumerable.Range(1980, 2000).Select(x =>
               new SelectListItem()
               {
                   Text = x.ToString(),
                   Value = x.ToString()
               }), "Value", "Text");

            ViewBag.Days = new SelectList(Enumerable.Range(1, 31).Select(x =>
              new SelectListItem()
              {
                  Text = x.ToString(),
                  Value = x.ToString()
              }), "Value", "Text");
            #endregion
            basicInfo.SelectedMonth = basicInfo.DOB.Month;
            basicInfo.SelectedYear = basicInfo.DOB.Year;
            basicInfo.SelectedDay = basicInfo.DOB.Day;
            return View("Index", basicInfo);
        }

        public ActionResult SaveDetailInfo(UserDetails details)
        {
            BusinessLayer blObject = new BusinessLayer();
            DateTime expiry = new DateTime(Convert.ToInt32(details.SelectedYear),
                               Convert.ToInt32(details.SelectedMonth),
                                Convert.ToInt32(details.SelectedDay));
            details.DOB = expiry;

            blObject.AddApplicantDetailedInfomation(details);
            #region yeardata
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x =>
             new SelectListItem()
             {
                 Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1],
                 Value = x.ToString()
             }), "Value", "Text");

            ViewBag.Years = new SelectList(Enumerable.Range(1980, 2000).Select(x =>
               new SelectListItem()
               {
                   Text = x.ToString(),
                   Value = x.ToString()
               }), "Value", "Text");

            ViewBag.Days = new SelectList(Enumerable.Range(1, 31).Select(x =>
              new SelectListItem()
              {
                  Text = x.ToString(),
                  Value = x.ToString()
              }), "Value", "Text");
            #endregion
            string email = Session["userName"].ToString();
            details = blObject.GetApplicantDetails(email);
            details.SelectedMonth = details.DOB.Month;
            details.SelectedYear = details.DOB.Year;
            details.SelectedDay = details.DOB.Day;
            return View("Index", details);
        }

        public ActionResult GetJoineeInfo(string EmailId)
        {
            #region yeardata
            BusinessLayer blObject = new BusinessLayer();
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x =>
             new SelectListItem()
             {
                 Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1],
                 Value = x.ToString()
             }), "Value", "Text");

            ViewBag.Years = new SelectList(Enumerable.Range(1980, 2000).Select(x =>
               new SelectListItem()
               {
                   Text = x.ToString(),
                   Value = x.ToString()
               }), "Value", "Text");

            ViewBag.Days = new SelectList(Enumerable.Range(1, 31).Select(x =>
              new SelectListItem()
              {
                  Text = x.ToString(),
                  Value = x.ToString()
              }), "Value", "Text");
            #endregion
            UserDetails details = new UserDetails();
          //  details = blObject.GetApplicantDetails(EmailId);
            //details.SelectedMonth = details.DOB.Month;
            //details.SelectedYear = details.DOB.Year;
            //details.SelectedDay = details.DOB.Day;
            return View("NewJoineeDetail", details);

        }

        //[HttpPost]
        //public ActionResult SubmitQuery(Query query)
        //{
        //    LoginViewModel model = new LoginViewModel();
        //    model.UserName = Session["userName"].ToString();
        //    BusinessLayer bl = new BusinessLayer();
        //    bl.SubmitQuery(query,model);
        //    return View();
        //}

        [HttpPost]
        public ActionResult SubmitQuery(Query query)
        {
            LoginViewModel model = new LoginViewModel();
            model.UserName = Session["userName"].ToString();
            BusinessLayer bl = new BusinessLayer();
            int recordsAffected = bl.SubmitQuery(query, model);
            return Json(recordsAffected, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetUserSpecificQueriesAndReplies(string email)
        {
            string userName = string.Empty;

            if (userName == "" && Session["userName"] != null)
            {
                userName = Session["userName"].ToString();
                if (!string.IsNullOrEmpty(email))
                {
                    userName = email;
                }
            }
            else if (userName == null && Session["userName"] == null)
                return RedirectToActionPermanent("Index", "Login");
            
            BusinessLayer bl = new BusinessLayer();
            List<Query> queriesWithReplyList = bl.GetUserSpecificQueriesAndReplies(userName);
            
            return Json(queriesWithReplyList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInterviewFeedbackForm()
        { 
            return View("InterviewFeedbackJoinee");
        }

        public ActionResult SaveInterviewFeedbackForm(string feedback)
        {
            string email = Session["userName"].ToString();
            BusinessLayer bl = new BusinessLayer();
            bl.SaveFeedback(feedback, email);
            return View("InterviewFeedbackJoinee");
        }

        public ActionResult ReferAndEarn()
        {
            BusinessLayer bl = new BusinessLayer();
            ReferAndEarnModel model = new ReferAndEarnModel();
            model.JobList = bl.GetJobListForReferAndEarn();
            return View("ReferAndEarn", model);
        }
        public ActionResult SubmitQuery()
        {
                                      
            return View();
        }

        public void SaveReferAndEarn(ReferAndEarnModel model)
        {
            model.ReferedBy = Session["userName"].ToString();
            BusinessLayer bl = new BusinessLayer();
            bl.SaveReferred(model);

        }
    }
}