using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace xpPortal.Models
{
    public class NewJoinee
    {

        public DataRowCollection NewJoineeList { get; set; }

        public int SelectedMonth { get; set; }
        public int SelectedYear { get; set; }
        public int SelectedDay { get; set; }
    }
}