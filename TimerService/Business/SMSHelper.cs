using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimerService.Business
{
    public class SMSHelper
    {
        public static SMSResult Send(string cellpone)
        {
            return new SMSResult(true, "阿里通道", "发送成功");
        }
    }
}
