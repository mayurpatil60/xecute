using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xpPortal.Models
{
    public class UserDetails
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Gender { get; set; }
        public DateTime DOB{ get; set; }
        public DateTime JoiningDate { get; set; }
        public int DaysRemainToJoin { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string PassportNo { get; set; }
        public string PassportExpiry{ get; set; }
        public string EmergencyContactName{ get; set; }
        public string EmergencyContactNo { get; set; }
        public string EmergencyContactRelation { get; set; }
        public string BloodGroup { get; set; }
        public bool AllDetailsFlag { get; set; }
        public bool AllDocumentsFlag { get; set; }

    }
}