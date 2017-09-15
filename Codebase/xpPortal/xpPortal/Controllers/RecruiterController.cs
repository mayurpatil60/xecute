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

        public ActionResult AddApplicantAndSendMail(UserDetails userDetails)
        {
            BusinessLayer bl = new BusinessLayer();

            bl.AddApplicantBasicDetailsAndSendMail(userDetails);
            return View();
        }

        public ActionResult GetQueries()
        {
            BusinessLayer bl = new BusinessLayer();
            
            return View("Queries",bl.GetQueries());
        }


        public ActionResult AddNewJoinee()
        {
            UserDetails model = new UserDetails();
            return View("AddNewJoinee",model);
        }

        //public ActionResult GetNewJoineeList()
        //{
        //    NewJoinee model = new NewJoinee();
        //    BusinessLayer blObject = new BusinessLayer();
        //    model.NewJoineeList = blObject.GetNewJoineeList();
        //    return View("NewJoineeList", model);
        //}


        public ActionResult GetDetailInfo(string EmailId)
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
            details = blObject.GetApplicantDetails(EmailId);
            details.SelectedMonth = details.DOB.Month;
            details.SelectedYear = details.DOB.Year;
            details.SelectedDay = details.DOB.Day;
            return View("NewJoineeDetail", details);

        }

        public ActionResult SaveNewJoinee(UserDetails basicInfo)
        {
            UserDetails model = new UserDetails();
            BusinessLayer blObject = new BusinessLayer();
            //GetNewJoineeList();
            blObject.AddNewJoinee(basicInfo);
            return View("AddNewJoinee", model);
        }
    }
}