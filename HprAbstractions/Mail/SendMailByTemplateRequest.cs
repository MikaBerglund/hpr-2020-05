using System;
using System.Collections.Generic;
using System.Text;

namespace HprAbstractions.Mail
{
    public class SendMailByTemplateRequest : SendMailRequestBase
    {

        public string TemplateId { get; set; }

        public Dictionary<string, string> Data { get; set; }

    }
}
