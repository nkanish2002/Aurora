using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace Aurora.Helpers
{
    public enum MailTypes { 
        RegistrationRequest, 
        RegistrationSuccessful, 
        AccountConnected,
        RegisteredForPlacements,
        RequestPlacementRegistration,
        Notification,
        TestMail
    };

    public class Mailer
    {
        static string host = ConfigurationManager.AppSettings["EmailHost"];
        static string username = ConfigurationManager.AppSettings["Username"];
        static string password = ConfigurationManager.AppSettings["Password"];
        static int port = int.Parse(ConfigurationManager.AppSettings["Port"]);

        public static string LoadTemplate(MailTypes Type, Dictionary<string, string> Values)
        {
            string url = "";

            switch (Type)
            {
                case MailTypes.RegistrationRequest:
                    url = "EmailTemplates\\RegistrationRequest.html"; break;

                case MailTypes.RegistrationSuccessful:
                    url = "EmailTemplates\\RegistrationSuccessful.html"; break;

                case MailTypes.AccountConnected:
                    url = "EmailTemplates\\AccountConnected.html"; break;

                case MailTypes.RegisteredForPlacements:
                    url = "EmailTemplates\\RegisteredForPlacements.html"; break;

                case MailTypes.RequestPlacementRegistration:
                    url = "EmailTemplates\\RequestPlacementRegistration.html"; break;

                case MailTypes.Notification:
                    url = "EmailTemplates\\Notification.html"; break;

                case MailTypes.TestMail:
                    url = "EmailTemplates\\Test.html"; break;
            }

            var body = File.ReadAllText(url);
            foreach (var key in Values.Keys)
            {
                body = body.Replace("{{" + key + "}}", Values[key]);
            }

            return body;
        }

        public static void SendMail(string[] Recipients, string body, MailTypes Type)
        {
            var message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(username, "Support@CIR");
            message.Body = body;
            message.Subject = "No-Reply: " + Type.ToString();
            
            foreach (var recipient in Recipients)
            {
                message.To.Add(new MailAddress(recipient));    
            }

            var mailer = new SmtpClient(host, port);
            mailer.EnableSsl = true;
            mailer.Credentials = new NetworkCredential(username, password);
            mailer.Send(message);
        }
    }
}