using System;
using Quartz;
using System.Configuration;

namespace TimerService
{
    public class HandleDataJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            SqlLogger.Log("任务：处理数据开始");
        }

        public static ITrigger GetTrigger()
        {            
            TriggerBuilder builder = TriggerBuilder.Create();
            builder.WithDailyTimeIntervalSchedule(x => x.StartingDailyAt(new TimeOfDay(0, 0, 0)).OnEveryDay());
            ITrigger trigger = builder.Build();
            return trigger;
        }
    }
}