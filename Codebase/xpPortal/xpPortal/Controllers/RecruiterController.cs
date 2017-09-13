using System;
using System.Collections.Generic;
using System.Data;
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

        public void GetNewJoineeList()
        {
            UserDetails model = new UserDetails();
            BusinessLayer blObject = new BusinessLayer();
            DataRowCollection drc = blObject.GetNewJoineeList();
        }

        public ActionResult SaveNewJoinee(UserDetails basicInfo)
        {
            UserDetails model = new UserDetails();
            BusinessLayer blObject = new BusinessLayer();
            GetNewJoineeList();
            blObject.AddNewJoinee(basicInfo);
            return View("AddNewJoinee", model);
        }
    }
}