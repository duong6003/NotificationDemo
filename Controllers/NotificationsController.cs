using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotificationTest.Notifications;
using NotificationTest.Requests;

namespace NotificationTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationsController : ControllerBase
    {
        [HttpPost]
        public IActionResult SubscribeNotify(SubscibeNotifyRequest request)
        {
            switch(request.NotifySystem)
            {
                case NotifySystem.DbNotify:
                    NotifyHandler.Subscribe(DbNotification.GetDbNotifier());
                    break;
            }
            return Ok();
        }
        [HttpPost]
        public IActionResult UnSubscribeNotify(SubscibeNotifyRequest request)
        {
            switch(request.NotifySystem)
            {
                case NotifySystem.DbNotify:
                    NotifyHandler.UnSubscribe(DbNotification.GetDbNotifier());
                    break;
            }
            return Ok();
        }
    }
}