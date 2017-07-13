using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Topshelf;

namespace TimerService
{
    public class ServiceRunner : ServiceControl, ServiceSuspend
    {
        private readonly IScheduler _scheduler;

        public ServiceRunner()
        {
            _scheduler = StdSchedulerFactory.GetDefaultScheduler();

            _scheduler.ScheduleJob(JobBuilder.Create<SendMailJob>().Build(), SendMailJob.GetTrigger());
            _scheduler.ScheduleJob(JobBuilder.Create<SendSMSJob>().Build(), SendSMSJob.GetTrigger());
            _scheduler.ScheduleJob(JobBuilder.Create<HandleDataJob>().Build(), HandleDataJob.GetTrigger());
        }

        public bool Continue(HostControl hostControl)
        {
            SqlLogger.Log("服务继续");
            _scheduler.ResumeAll();
            return true;
        }

        public bool Pause(HostControl hostControl)
        {
            SqlLogger.Log("服务暂停");
            _scheduler.PauseAll();
            return true;
        }

        public bool Start(HostControl hostControl)
        {
            SqlLogger.Log("服务启动");
            _scheduler.Start();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            SqlLogger.Log("服务停止");
            _scheduler.Shutdown();
            return true;
        }
    }
}
