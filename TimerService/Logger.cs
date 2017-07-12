using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = @"Log4net.config", Watch = true)]
namespace TimerService
{
    public class SqlLogger
    {
        public static void Log(string msg)
        {
            ILog _log = LogManager.GetLogger("SysLogger");
            _log.Error(msg);
        }

        public static void Log(object msg)
        {
            ILog _log = LogManager.GetLogger("SysLogger");
            _log.Error(msg);
        }

        public static void Log(Exception ex)
        {
            ILog _log = LogManager.GetLogger("SysLogger");
            _log.Error(ex);
        }
    }

    public class FileLogger
    {
        public static void Log(string msg)
        {
            ILog _log = LogManager.GetLogger("mvclog");
            _log.Error(msg);
        }

        public static void Log(object msg)
        {
            ILog _log = LogManager.GetLogger("mvclog");
            _log.Error(msg);
        }

        public static void Log(Exception ex)
        {
            ILog _log = LogManager.GetLogger("mvclog");
            _log.Error(ex);
        }
    }
}
