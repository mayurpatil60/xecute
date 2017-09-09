using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using xpPortal.BL;
using xpPortal.Models;

namespace xpPortal.Controllers
{
    //[Authorize]
    public class DocumentController : Controller
    {
       
        
        public DocumentController()
        {

        }

        // GET: Login
        public ActionResult UploadDocument()
        {
            return View("UploadDocument");
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        //public ActionResult Login(LoginViewModel model)
        //{
        //    BusinessLayer blObject = new BusinessLayer();
        //    bool isValiduser=blObject.ValidateUser(model);

        //    if(isValiduser)
        //        return RedirectToAction("Index","Home"); 
        //    else
        //        ModelState.AddModelError("", "Invalid username or password.");
            
        //    return View("Index");
        //}
    }


}