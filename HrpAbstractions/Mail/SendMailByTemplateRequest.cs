using System;
using System.Collections.Generic;
using System.Text;

namespace HrpAbstractions.Mail
{
    public class SendMailByTemplateRequest : SendMailRequestBase
    {

        public string TemplateId { get; set; }

        public Dictionary<string, string> Data { get; set; }

    }
}
