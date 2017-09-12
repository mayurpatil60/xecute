using System;
using System.Collections.Generic;
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

            if (model.UserName != null)
            {
                details = bl.GetApplicantBasicDetails(model.UserName);
                SetSessionVariables(details);
            }
            if (model.UserName == null && Session["userName"] != null)
            {
                model.UserName = Session["userName"].ToString();
            }
            else if(model.UserName == null && Session["userName"] == null)
                return RedirectToActionPermanent("Index", "Login");
                

            return View("Dashboard", details);
        }

        private void SetSessionVariables(UserDetails model)
        {
            Session["firstName"] = model.FirstName;
        }
    }
}