using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using TopLearn.Convertors;

namespace TopLearn.Senders
{
    public class SendEmail
    {
        public static void Send(string to,string subject,string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("sina121gh@gmail.com","sina");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            //LinkedResource logo = new LinkedResource("E:\\Documents\\VS\\Asp.Net Core\\Advanced\\TopLearn\\TopLearn.Web\\wwwroot\\images\\logo.png");
            //LinkedResource emailActivation = new LinkedResource("E:\\Documents\\VS\\Asp.Net Core\\Advanced\\TopLearn\\TopLearn.Web\\wwwroot\\images\\EmailActivation.png");
            //logo.ContentId = "Logo";
            //emailActivation.ContentId = "EmailActivation";
            //AlternateView htmlView = AlternateView.CreateAlternateViewFromString("", null, "text/html");
            //htmlView.LinkedResources.Add(logo);
            //htmlView.LinkedResources.Add(emailActivation);
            //mail.AlternateViews.Add(htmlView);

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(Environment.GetEnvironmentVariable("SMTP_USERNAME"),
                Environment.GetEnvironmentVariable("SMTP_PASSWORD"));
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
    }
}