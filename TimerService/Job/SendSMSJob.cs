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

            try
            {
                string sql = "select * from smsTask where status = 0";
                var smsTasks = MySqlHelper.GetConn().Query(sql);

                string taskLogSql = @"insert into smsTaskLog(sysid,sysname,moduleid,modulename,date,phone,text,success,smschannel,msg) values
                (@sysid,@sysname,@moduleid,@modulename,@date,@phone,@text,@success,@smschannel,@msg);";

                var time = DateTime.Now;
                foreach (var item in smsTasks)
                {
                    SMSResult result = SMSHelper.Send(item.phone);

                    MySqlHelper.GetConn().Execute("update smsTask set status = @status where id = @id",
                        new { id = item.id, status = result.Success ? 1 : 2 });

                    MySqlHelper.GetConn().Execute(taskLogSql, new
                    {
                        sysid = item.sysid,
                        sysname = item.sysname,
                        moduleid = item.moduleid,
                        modulename = item.modulename,
                        date = time,
                        phone = item.phone,
                        text = item.text,
                        success = result.Success ? 1 : 0,
                        smschannel = result.Channel,
                        msg = result.Msg
                    });
                }
            }
            catch (Exception ex)
            {
                SqlLogger.Log(ex);
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