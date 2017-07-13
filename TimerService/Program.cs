using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace TimerService
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlLogger.Log("程序运行");

            HostFactory.Run(x =>
            {
                SqlLogger.Log("程序运行 Run");

                x.Service<ServiceRunner>();

                x.RunAsLocalSystem();

                x.SetDescription("论文定时服务");
                x.SetDisplayName("论文定时服务");
                x.SetServiceName("论文定时服务");

                x.EnablePauseAndContinue();

            });
        }
    }
}
