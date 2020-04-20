using System;
using System.Collections.Generic;
using System.Text;

namespace HprAbstractions.Configuration
{
    public class RootConfigSection
    {

        public string AzureWebJobsStorage { get; set; }

        public HprConfigSection Hpr { get; set; }

    }
}
