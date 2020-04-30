using QuiZone.DataAccess.Models.Const;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace QuiZone.BusinessLogic.Utils.Email
{
    /// <summary>
    /// Class for send email (uses SmtpClient)
    /// </summary>
    public sealed class EmailSender
    {

        private static string Host => AppSettings.EmailHost;
     
        private static int Port => AppSettings.EmailPort;

        private static string SenderAddress => AppSettings.EmailAccount;

        private static string SenderPassword => AppSettings.EmailAccountPassword;

        private static string SenderName => "Administrator's QuiZone";

        private string ToAddress { get; set; }
        private string RecipientName { get; set; }

        private string SubjectMessage { get; set; }
        private string BodyMessage { get; }
        

        public EmailSender(string bodyMessage)
        {
            BodyMessage = bodyMessage;
        }
      

        /// <summary>
        /// A method for sending a message to a specified mail
        /// </summary>
        /// <param name="subjectMessage">Subject Message</param>
        /// <param name="toAddress">Recipient's Adress </param>
        /// <param name="recipientName">Recipient's Name</param>
        /// <returns>Status Code</returns>
        public async Task SendAsync(string subjectMessage, string toAddress, string recipientName)
        {
            SubjectMessage = subjectMessage;
            ToAddress = toAddress;
            RecipientName = recipientName;

            var fromAddressData = new MailAddress(SenderAddress, SenderName);
            var toAddressData = new MailAddress(ToAddress, RecipientName);

            var smtp = new SmtpClient
            {
                Host = Host,
                Port = Port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddressData.Address, SenderPassword)
            };

            using var message = new MailMessage(fromAddressData, toAddressData)
            {
                Subject = SubjectMessage,
                Body = BodyMessage,
                IsBodyHtml = true
            };

            {
                await smtp.SendMailAsync(message);
            }
        }
    }
}
