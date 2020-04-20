using System;
using System.Collections.Generic;
using System.Text;

namespace HrpAbstractions.Configuration
{
    public class RootConfigSection
    {

        public string AzureWebJobsStorage { get; set; }

        public HprConfigSection Hpr { get; set; }

    }
}
