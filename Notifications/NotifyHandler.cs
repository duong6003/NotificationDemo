using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationTest.Notifications
{
    public class NotifyHandler
    {
        private static List<INotify> notifies = new();
        public static void Subscribe(INotify notify)
        {
            notifies.Add(notify);
        }
        public static void UnSubscribe(INotify notify)
        {
            notifies.Remove(notify);
        }
        public async Task NotifyAsync(NotificationType type)
        {
            IEnumerable<Task> tasks = notifies.Select(notify => new Task(() => {
                notify.Send(this, type);
            }));
            await Task.WhenAll(tasks);
        }
    }
}