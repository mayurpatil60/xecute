﻿using System;
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
    public class LoginController : Controller
    {
       
        
        public LoginController()
        {

        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Login(LoginViewModel model)
        {
            BusinessLayer blObject = new BusinessLayer();
            bool isPasswordSet = false;
            bool isValiduser=blObject.ValidateUser(model,out isPasswordSet);

            if (isValiduser && isPasswordSet)
                return RedirectToAction("Index","Home"); 
            else if (isValiduser && !isPasswordSet)
                return RedirectToAction("ResetPassword",model);
            else
                ModelState.AddModelError("", "Invalid username or password.");
            
            return View("Index");
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Login2(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //AuthenticationServiceReference.AuthenticationServiceClient ProxyServiceobject = new AuthenticationServiceReference.AuthenticationServiceClient();
                //bool Isvalidated = ProxyServiceobject.AuthenticateDomainUser(model.UserName, model.Password, "Dashboard");

            //    if (Isvalidated)
            //    {
            //        TempData["Authenticate"] = "Authenticated";
            //        string pUserName = ProxyServiceobject.GetSAMAccountFullName(model.UserName, "Dashboard");

            //        Repository pRepository = new Repository();
            //        bool pAutomationAccess = false;
            //        string userRole = pRepository.GetGroupName(ProxyServiceobject, model.UserName, out pAutomationAccess);

            //        Session["AutomationAccess"] = pAutomationAccess.ToString();
            //        Session["AdminRole"] = userRole;

            //        Session["UserName"] = pUserName;
            //        Session["UserEmail"] = model.UserName;


            //        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
            //        bool isCookiePersistent = true;//chkPersist.Checked;

            //        // Setting cookie expiration as default date time value so the cookie expires when the user closes the browser
            //        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, pUserName,
            //        DateTime.Now, DateTime.MinValue, isCookiePersistent, string.Empty); // we can later pass user roles in last parameter

            //        //Encrypt the ticket.
            //        String encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            //        //Create a cookie, and then add the encrypted ticket to the cookie as data.
            //        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            //        if (true == isCookiePersistent)
            //            authCookie.Expires = authTicket.Expiration;

            //        //Add the cookie to the outgoing cookies collection.
            //        Response.Cookies.Add(authCookie);

            //        //You can redirect now.
            //        //Response.Redirect(FormsAuthentication.GetRedirectUrl(model.UserName, false));
            //        return RedirectToAction("Synchronize", "Admin");

            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "Invalid username or password.");
            //    }
            }

            ////If we got this far, something failed, redisplay form
            //return View(model);
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("SignIn", "Login");
        }

        public ActionResult ResetPassword(LoginViewModel model)
        {
            return View(model);
        }
        [HttpPost]
        public ActionResult SetPassword(LoginViewModel model)
        {

            BusinessLayer blObject = new BusinessLayer();
            blObject.ResetPassword(model);

            return RedirectToAction("Index", "Home");
        }



    }


}