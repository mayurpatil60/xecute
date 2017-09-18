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
    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Index(LoginViewModel model)
        {
            BusinessLayer bl = new BusinessLayer();

            UserDetails details = new UserDetails();
            string roleName = string.Empty;
            if (model.UserName != null)
            {
                details = bl.GetApplicantBasicDetails(model.UserName);
                SetSessionVariables(details);
                roleName = Session["RoleName"].ToString();
            }
            if (model.UserName == null && Session["userName"] != null)
            {
                roleName = Session["RoleName"].ToString();
                model.UserName = Session["userName"].ToString();
            }
            else if (model.UserName == null && Session["userName"] == null)
                return RedirectToActionPermanent("Index", "Login");

            if (roleName == Models.Enum.Roles.Recruiter.ToString())
            {
                NewJoinee collection = new NewJoinee();
                collection.NewJoineeList = bl.GetNewJoineeList();
                return View("RecruiterDashboard", collection);
            }
            else
            {
                NewJoinee collection = new NewJoinee();
                collection.NewJoineeList = bl.GetDocumentListByUser(Session["userName"].ToString());
                return View("Dashboard", collection);
            }
                
        }

        private void SetSessionVariables(UserDetails model)
        {
            Session["firstName"] = model.FirstName;
            Session["userName"] = model.Email;
            Session["DaysToJoin"] = ((int)(model.JoiningDate.Subtract(DateTime.Now)).TotalDays).ToString();
        }
    }
}