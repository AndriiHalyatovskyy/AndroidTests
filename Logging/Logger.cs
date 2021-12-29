using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace ClassLibrary1.Logging
{
    public static class Logger
    {
        public static ILog GetLogger { get; } = LogManager.GetLogger("Log4Net");
    }
}
