using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotificationTest.Entities;
using Web.Repositories;

namespace NotificationTest.Notifications
{
    public class DbNotification : INotify
    {
        private static DbNotification? Instance;
        private static IServiceScopeFactory _serviceScopeFactory = default!;
        private DbNotification()
        {
        }
        public static void Register(IServiceScopeFactory scopeFactory)
        {
            _serviceScopeFactory = scopeFactory;
        }
        public static DbNotification GetDbNotifier()
        {
            if(Instance == null) Instance = new();
            return Instance;
        }
        public void Send(object arg, NotificationType type)
        {
            if(_serviceScopeFactory == null) throw new InvalidOperationException($"{nameof(DbNotification)} does not register");
            if(arg is Gift gift) Notify(gift, type);
        }
        private async void Notify(Gift gift, NotificationType type)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            IRepositoryWrapper repositoryWrapper = scope.ServiceProvider.GetRequiredService<IRepositoryWrapper>();
            Notification? notify = null;
            switch(type)
            {
                case NotificationType.AcceptGift:
                notify = new()
                {
                    Subject = gift.ReceivePlayer!.Name,
                    To = gift.SendPlayer!.Name,
                    Tittle = Enum.GetName(type) ?? string.Empty,
                    Content = gift.Item!.Name
                };
                break;
                case NotificationType.ReceiveGift:
                notify = new()
                {
                    Subject = gift.ReceivePlayer?.Name ?? (await repositoryWrapper.Repository<Player>().GetByIdAsync(gift.ReceivePlayerId))!.Name,
                    To = gift.SendPlayer?.Name ?? (await repositoryWrapper.Repository<Player>().GetByIdAsync(gift.SendPlayer))!.Name,
                    Tittle = Enum.GetName(type) ?? string.Empty,
                    Content = gift.Item!.Name
                };
                break;
            }
            if(notify != null) await repositoryWrapper.Repository<Notification>().AddAsync(notify);
        }
    }
}