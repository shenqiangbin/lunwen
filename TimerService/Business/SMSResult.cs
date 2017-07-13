using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimerService.Business
{
    public class SMSResult
    {
        public bool Success { get; set; }
        public string Channel { get; set; }
        public string Msg { get; set; }

        public SMSResult(bool success,string channel,string msg)
        {
            this.Success = success;
            this.Channel = channel;
            this.Msg = msg;
        }
    }
}
