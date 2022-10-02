using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationTest.Notifications
{
    public enum NotificationType
    {
        ReceiveGift,
        AcceptGift,
        RejectGift
    }
    public interface INotify
    {
        void Send(object arg, NotificationType type);
    }
}