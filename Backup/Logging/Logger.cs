using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Backup.Logging
{
    internal class InternalLogger
    {
        public static void Temp()
        {
            LogManager.GetCurrentClassLogger();
        }

    }
}
