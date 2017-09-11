using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xpPortal.Models
{
    public class Query
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string QueryDetail { get; set; }
        public bool IsAnswered { get; set; }
        public string DateCreated { get; set; }
        public LoginViewModel login { get; set; }
    }   
}