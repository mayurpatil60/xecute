using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xpPortal.Models
{
    public class Buddy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AssignedTo { get; set; }
        public string PhoneNo { get; set; }
        public string Rating { get; set; }
        public string Location { get; set; }
    }
}