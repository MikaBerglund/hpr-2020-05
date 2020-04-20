using System;
using System.Collections.Generic;
using System.Text;

namespace HprAbstractions.Configuration
{
    public class SendGridConfigSection
    {

        public string ApiKey { get; set; }

        public string FromName { get; set; }

        public string FromAddress { get; set; }

    }
}
