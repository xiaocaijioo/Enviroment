using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace ProtectUI
{
    public class MailHelper
    {
        public void SendMail(string target,string subject,string body)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient client = new SmtpClient();
            mailMessage.To.Add(new MailAddress(target, target.ToString(), Encoding.UTF8));

            mailMessage.Subject = subject;
            mailMessage.SubjectEncoding = Encoding.UTF8;

            mailMessage.Body = body;
            mailMessage.BodyEncoding = Encoding.UTF8;

            mailMessage.IsBodyHtml = true;

            mailMessage.Priority = MailPriority.Normal;

            client.Send(mailMessage);
        }

    }
}