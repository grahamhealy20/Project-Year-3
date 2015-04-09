using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace TrackingRESTService.Model
{
    public class EMailHelper
    {
        private static MailAddress fromAddress = new MailAddress("3rdyearprojectemail@gmail.com", "Baby Monitor");
        const string fromPassword = "OMITTED! RE-ENTER WHEN WORKING ON PROJECT"; // DO NOT COMMIT WITH PW STILL INCLUDED

        SmtpClient smtp = new SmtpClient(){
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };

        public void sendMessage(MailAddress receiptent, string subject, string body) {
            using (var message = new MailMessage(fromAddress, receiptent)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}