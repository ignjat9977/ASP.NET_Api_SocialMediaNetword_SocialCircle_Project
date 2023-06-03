using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.Email;

namespace Implementation.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        

        public void Send(EmailSendDto dto)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp-mail.outlook.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("ignjat1122@hotmail.com", "1122334455abc"),
                UseDefaultCredentials = false
            };

            var message = new MailMessage("ignjat1122@hotmail.com", dto.SendTo);
            message.Subject = dto.Subject;
            message.Body = dto.Content;
            message.IsBodyHtml = true;
            smtp.Send(message);
        }
    }
}
