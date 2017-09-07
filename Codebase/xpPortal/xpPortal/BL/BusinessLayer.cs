using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using xpPortal.DAL;
using xpPortal.Models;

namespace xpPortal.BL
{
    public class BusinessLayer
    {
        public bool ValidateUser(LoginViewModel model)
        {
            bool isValidUser = false;

            DataAccessLayer dalObject = new DataAccessLayer();

            isValidUser=dalObject.ValidateUser(model);

            return isValidUser;
        }
    }
}