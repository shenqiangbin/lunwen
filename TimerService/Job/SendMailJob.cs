using System;
using Quartz;
using System.Configuration;

namespace TimerService
{
    public class SendMailJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            SqlLogger.Log("任务：发送邮件开始");
        }

        public static ITrigger GetTrigger()
        {
            string interval = ConfigurationManager.AppSettings["IntervalInSeconds"].ToString();

            TriggerBuilder builder = TriggerBuilder.Create();
            builder.WithSimpleSchedule(x => x.WithIntervalInSeconds(int.Parse(interval)).RepeatForever());
            ITrigger trigger = builder.Build();
            return trigger;
        }
    }
}