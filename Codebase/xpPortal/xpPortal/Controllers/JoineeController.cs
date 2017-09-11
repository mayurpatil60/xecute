using System;
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
            bcInfo = blObject.GetApplicantDetails("amit1990libra@gmail.com");

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

        public ActionResult SaveBasicInfo(UserDetails basicInfo)
        {
            BusinessLayer blObject = new BusinessLayer();
            blObject.AddApplicantBasicDetailsAndSendMail(basicInfo);
            basicInfo = blObject.GetApplicantDetails("amit1990libra@gmail.com");

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

            details = blObject.GetApplicantDetails("amit1990libra@gmail.com");
            return View("Index", details);
        }



        [HttpPost]
        public ActionResult SubmitQuery(string query, LoginViewModel model)
        {
            BusinessLayer bl = new BusinessLayer();
            bl.SubmitQuery(query, model);
            return View();
        }
    }
}