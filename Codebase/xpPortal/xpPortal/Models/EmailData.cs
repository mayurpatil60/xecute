using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xpPortal.Models
{
    /// <summary>
    /// This class holds the email content.
    /// </summary>
    public class EmailData
    {
        /// <summary>
        /// List of addresses to which email has to send.
        /// </summary>
        public List<string> ToEmailDdresses { get; set; }

        /// <summary>
        /// Holds content of email body.
        /// </summary>
        public string EmailBody { get; set; }

        /// <summary>
        /// Holds Email subject.
        /// </summary>
        public string EmailSubject { get; set; }


        /// <summary>
        /// Holds from email address
        /// </summary>
        public string FromEmailAddress { get; set; }

    }
}