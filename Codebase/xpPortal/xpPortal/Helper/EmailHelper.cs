using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using xpPortal.Models;

namespace xpPortal.Helper
{
    public static class EmailHelper
    {
        /// <summary>
        /// This method is used to send Email
        /// </summary>
        /// <returns></returns>
        //public static string SendEmail(EmailData objEmail, string smtpHost, int smtpPort, HttpContext current)
        public static string SendEmail(EmailData objEmail, string smtpHost, int smtpPort)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(current.Request.ApplicationPath);
            SmtpSection settings =
                (SmtpSection)config.GetSection("system.net/mailSettings/smtp");
            if (settings == null)
            {
                return "Please configure the mail setting in .config file";
            }
            else
            {
                MailMessage mailMessage = new MailMessage();
                foreach (string toEmailAddress in objEmail.ToEmailDdresses)
                {
                    mailMessage.To.Add(toEmailAddress);
                }


                mailMessage.Subject = objEmail.EmailSubject;
                mailMessage.IsBodyHtml = true;

                mailMessage.Body = objEmail.EmailBody;

                mailMessage.From = new System.Net.Mail.MailAddress(objEmail.FromEmailAddress);
                SmtpClient smtpClient = new SmtpClient();


                smtpClient.Host = smtpHost;
                smtpClient.Port = smtpPort;
                smtpClient.UseDefaultCredentials = true;
                //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtpClient.Send(mailMessage);
            }
            return string.Empty;
        }
    }
}