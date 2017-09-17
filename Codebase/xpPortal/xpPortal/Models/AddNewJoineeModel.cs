using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xpPortal.Models
{
    public class AddNewJoineeModel
    {
        public string FullName { get; set; }

        public string Email { get;set;}

        public string Buddy { get; set; }
        public string contactName { get; set; }

        public int SelectedMonth { get; set; }
        public int SelectedYear { get; set; }
        public int SelectedDay { get; set; }

        public string DateOfJoining { get; set; }

    }
}