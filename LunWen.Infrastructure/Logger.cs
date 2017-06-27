using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = @"Log4net.config", Watch = true)]
namespace LunWen.Infrastructure
{

    //注意：MySql.Data.dll、log4net.dll、log4net.config 必须设定为 始终拷贝，当然如果其它有，也可以
    /*
        CREATE TABLE `logtest` (
          `id` int(11) NOT NULL AUTO_INCREMENT,
          `date` datetime DEFAULT NULL,
          `thread` varchar(45) DEFAULT NULL,
          `level` varchar(45) DEFAULT NULL,
          `logger` varchar(45) DEFAULT NULL,
          `exception` varchar(45) DEFAULT NULL,
          `message` varchar(4000) DEFAULT NULL,
          `userid` varchar(45) DEFAULT NULL,
          PRIMARY KEY (`id`)
        ) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8;
    */
    public class Logger
    {
        public static void Log(string msg)
        {
            SqlLogger.Log(msg);
            //FileLogger.Log(msg);
        }

        public static void Log(Exception ex)
        {
            SqlLogger.Log(ex.Message + ex.StackTrace);
        }
    }

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
