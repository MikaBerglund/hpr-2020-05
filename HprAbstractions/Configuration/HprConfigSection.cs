using System;
using System.Collections.Generic;
using System.Text;

namespace HprAbstractions.Configuration
{
    public class HprConfigSection
    {

        public SendGridConfigSection SendGrid { get; set; }

        public ComputerVisionConfigSection ComputerVision { get; set; }

    }
}
