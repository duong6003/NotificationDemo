using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationTest.Requests
{
    public enum NotifySystem
    {
        DbNotify = 1
    }
    public class SubscibeNotifyRequest
    {
        public NotifySystem NotifySystem { get; set; }
    }
}