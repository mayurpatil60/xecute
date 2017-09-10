using System;
using System.Collections.Generic;
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
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitQuery(string query,LoginViewModel model)
        {
            BusinessLayer bl = new BusinessLayer();
            bl.SubmitQuery(query,model);
            return View();
        }
    }
}