using System;
using System.Collections.Generic;
using System.Text;

namespace HrpAbstractions.Mail
{
    public abstract class SendMailRequestBase
    {
        public string To { get; set; }
    }
}
