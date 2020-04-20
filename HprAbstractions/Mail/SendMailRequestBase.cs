using System;
using System.Collections.Generic;
using System.Text;

namespace HprAbstractions.Mail
{
    public abstract class SendMailRequestBase
    {
        public string To { get; set; }
    }
}
