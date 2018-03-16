using System.Net.Mail;
using System.Net;
using System.Configuration;
using System;

namespace Coopers.BusinessLayer.Services.Services
{
    public class EmailApplicationService : IEmailApplicationService
    {


        #region PRIVATE MEMBERS
      

        #endregion


        #region CONSTRUCTOR

        public EmailApplicationService()
        {
         
        }

        #endregion


        #region PUBLIC MEMBERS     

        public void SendAnEmail(string Template,string To,string Subject)
        {
            var from = new MailAddress(ConfigurationManager.AppSettings["SMTPFromAddress"], ConfigurationManager.AppSettings["SMTPFromName"]);
            var to = new MailAddress(To);
            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = Subject;
            mailMessage.CC.Add(new MailAddress("amar@trivediat.com"));
            mailMessage.Body = Template;
            mailMessage.IsBodyHtml = true;
            
            var client = new SmtpClient(ConfigurationManager.AppSettings["SMTPHost"], Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]))
            {
                Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUserName"], ConfigurationManager.AppSettings["SMTPPassword"]),
                EnableSsl = true
            };
            client.Send(mailMessage);
        }

        #endregion


        #region PRIVATE MEMBERS     



        #endregion


    }
}
