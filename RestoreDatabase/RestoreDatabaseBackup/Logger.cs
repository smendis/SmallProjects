using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoreDatabaseBackup
{
    public class Logger
    {
        private static Logger logger = null;

        private Logger()
        {
        }

        public static Logger CreateLoggerInstance()
        {
            if (logger == null)
            {
                logger = new Logger();
            }

            return logger;
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
