using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace EIS.BLL
{
    public class Utility
    {
        public static bool SendEmail(string ToEmail,string Subject,string Body)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("youremail@gmail.com", "*****************");

            MailMessage mm = new MailMessage("Admin@EIS.com", ToEmail, Subject, Body);
                        
            //client.Send(mm);
            return true;
        }
    }
}
