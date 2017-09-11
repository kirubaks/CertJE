using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;

namespace CertificationAutomation.Utilities
{
    public class Logger
    {
        public Logger()
        {
            BasicConfigurator.Configure();
        }
    }
}
