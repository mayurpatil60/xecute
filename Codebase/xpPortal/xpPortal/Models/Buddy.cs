using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xpPortal.Models
{
    public class Buddy
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int Email { get; set; }
        public string AssignedTo { get; set; }
        public string PhoneNo { get; set; }
    }
}