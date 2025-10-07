using SlimWaist.Languages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.Helpers
{
    public class EmailSender
    {
        public static async Task SendEmail(string recipientEmail, string subject, string body)
        {
            string senderEmail = "amrnewstory@gmail.com";
            string senderPassword = "eyrg umvb mien kslo";
            int smtpPort = 587;
            string smtpHost = "smtp.gmail.com";

            // Create the email message
            MailMessage message = new MailMessage();
            message.From = new MailAddress(senderEmail);
            message.To.Add(new MailAddress(recipientEmail));
            message.Subject = subject;
            message.Body = body;

            //message.IsBodyHtml = true; // Set to true if the body contains HTML

            // Configure the SMTP client
            SmtpClient smtpClient = new SmtpClient(smtpHost);
            smtpClient.Port = smtpPort;
            smtpClient.UseDefaultCredentials = false; // Set to false to provide credentials manually
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.EnableSsl = true; // Enable SSL for secure connection

            // Send the email
            smtpClient.Send(message);
        }
    }
}
