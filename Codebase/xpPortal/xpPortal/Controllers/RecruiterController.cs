using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xpPortal.BL;
using xpPortal.Models;

namespace xpPortal.Controllers
{
    public class RecruiterController : Controller
    {
        // GET: Recruiter
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Buddy()
        {
            List<Buddy> buddyList = new List<Buddy>();
            BusinessLayer blObject = new BusinessLayer();
            buddyList = blObject.GetBuddyList();
            return View(buddyList);
        }
        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult ManageJoinees()
        {
            NewJoinee model = new NewJoinee();
            BusinessLayer blObject = new BusinessLayer();
            model.NewJoineeList = blObject.GetNewJoineeList();
            #region yeardata
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x =>
             new SelectListItem()
             {
                 Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1],
                 Value = x.ToString()
             }), "Value", "Text");

            ViewBag.Years = new SelectList(Enumerable.Range(2017, 1).Select(x =>
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
            return View("ManageJoinees", model);
        }

        public ActionResult RelocationAssistant()
        {
            return View();
        }
        public ActionResult Refer()
        {
            return View();
        }
        public ActionResult Events()
        {
            return View();
        }
        public ActionResult ManageRecruiter()
        {
            List<Buddy> buddyList = new List<Buddy>();
            BusinessLayer blObject = new BusinessLayer();
            buddyList = blObject.GetBuddyList().OrderBy(x=>x.Name).Take(2).ToList();
            return View(buddyList);
        }

        public ActionResult AddApplicantAndSendMail(UserDetails userDetails)
        {
            BusinessLayer bl = new BusinessLayer();

            bl.AddApplicantBasicDetailsAndSendMail(userDetails);
            return View();
        }

        public ActionResult GetQueries()
        {
            BusinessLayer bl = new BusinessLayer();

            return View("Queries", bl.GetJoineeQueries());
        }

        public ActionResult GetAllQueriesCount()
        {
            BusinessLayer bl = new BusinessLayer();
            List<UserDetails> list= bl.GetJoineeQueries();
            int count = list.Count;
            return Json(count, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddNewJoinee()
        {
            UserDetails model = new UserDetails();
            return View("AddNewJoinee", model);
        }

        public ActionResult GetNewJoineeList()
        {
            NewJoinee model = new NewJoinee();
            BusinessLayer blObject = new BusinessLayer();
            model.NewJoineeList = blObject.GetNewJoineeList();
            return View("NewJoineeList", model);
        }
        

        public ActionResult GetDetailInfo(string EmailId)
        {
            EmailId = EmailId.Trim();
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
            details = blObject.GetApplicantDetails(EmailId);
            if (details.DOB.Year != 1)
            {
                details.SelectedMonth = details.DOB.Month;
                details.SelectedYear = details.DOB.Year;
                details.SelectedDay = details.DOB.Day;
            }
            return View("NewJoineeDetail", details);

        }

        public void VerificationDetail()
        {
            BusinessLayer blObject = new BusinessLayer();
            string email = Session["userName"].ToString();
            blObject.VerificationDetail(email);

        }

        public void SaveNewJoinee(AddNewJoineeModel basicInfo)
        {
            BusinessLayer blObject = new BusinessLayer();
            DateTime joining = new DateTime(Convert.ToInt32(basicInfo.SelectedYear),
                              Convert.ToInt32(basicInfo.SelectedMonth),
                               Convert.ToInt32(basicInfo.SelectedDay));
            basicInfo.DateOfJoining = joining.ToString();
            blObject.AddNewJoinee(basicInfo);
        }

        public ActionResult GetAllFeedback()
        {
            BusinessLayer blObject = new BusinessLayer();
            FeedbackModel model = new FeedbackModel();
            model.FeedbackList = blObject.GetFeedbackList();
            return View("FeedbackList", model);
        }


        public ActionResult JobListForReferAndEarn(string feedback)
        {
            BusinessLayer bl = new BusinessLayer();
            ReferAndEarnModel model = new ReferAndEarnModel();
            model.JobList = bl.GetJobListForReferAndEarn();
            model.ReferralList = bl.GetReferralList();
            return View("ReferAndEarn", model);
        }

        public ActionResult SaveNewJob(NewJobRefer model)
        {
            Random random = new Random();
            int num = random.Next();
            model.JobId = num;
            BusinessLayer bl = new BusinessLayer();
            bl.SaveNewJob(model);
            return RedirectToAction("JobListForReferAndEarn");
        }

        [HttpPost]
        public ActionResult SubmitQueryReply(string replyToEmail,Query query)
        {
            LoginViewModel model = new LoginViewModel();
            model.UserName = replyToEmail;
            BusinessLayer bl = new BusinessLayer();
            int recordsAffected = bl.SubmitQueryReply(query, model);
            return Json(recordsAffected, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveBuddy(Buddy buddy)
        {
            BusinessLayer bl = new BusinessLayer();

            bl.SaveBuddy(buddy);

            return RedirectToAction("Buddy");
        }
    }
}