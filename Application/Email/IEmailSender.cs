using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Email
{
    public interface IEmailSender
    {

        void Send(EmailSendDto dto);
    }
    public class EmailSendDto
    {
        public string Subject { get; set; }
        public DateTime Created { get; set; }
        public string Content { get; set; }
        public string SendTo { get; set; }
    }
}
