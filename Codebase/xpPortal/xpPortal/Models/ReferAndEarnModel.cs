using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace xpPortal.Models
{
    public class ReferAndEarnModel
    {

        public DataRowCollection JobList { get; set; }
        public string CandidateName { get;set;}
    public string ContactNumber { get;set;}
public string Email { get;set;}
    public string ReferedBy { get;set;}
    }
}