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

            UserDetails details = bl.GetApplicantBasicDetails(model.UserName);
            return View("Dashboard", details);
        }
    }
}