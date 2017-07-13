using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topshelf;

namespace TimerService
{
    public class ServiceRunner : ServiceControl
    {
        public bool Start(HostControl hostControl)
        {
            SqlLogger.Log("服务启动");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            SqlLogger.Log("服务停止");
            return true;
        }
    }
}
