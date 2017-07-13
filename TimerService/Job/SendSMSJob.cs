using System;
using Quartz;
using System.Configuration;
using Dapper;
using TimerService.SQL;
using TimerService.Business;

namespace TimerService
{
    public class SendSMSJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            SqlLogger.Log("任务：发送短信开始");

            string sql = "select * from smsTask where taskStatus = 0";
            var smsTasks = MySqlHelper.GetConn().Query(sql);

            string taskLogSql = @"insert into smsTaskLog(sysid,sysname,moduleid,modulename,date,phone,success,smschanel,msg) values
                ();";

            foreach (var item in smsTasks)
            {
                SMSResult result = SMSHelper.Send(item.Phone);
                if (result.Success)
                {

                }
            }
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