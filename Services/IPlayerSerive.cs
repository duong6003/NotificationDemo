using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificationTest.Entities;
using NotificationTest.Notifications;
using NotificationTest.Reponses;
using NotificationTest.Requests;
using Web.Repositories;

namespace NotificationTest.Services
{
    public interface IPlayerService
    {
        Task SendGiftAsync(SendGiftRequest request);
        Task<string?> ReceiveGiftAsync(ReceiveGiftRequest request);
        Task<string?> RejectGiftAsync(RejectGiftRequest request);
        IEnumerable<ReceiveGiftResponse> GetReceiveGift(Guid playerId);
    }
    public class PlayerService : IPlayerService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public PlayerService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public IEnumerable<ReceiveGiftResponse> GetReceiveGift(Guid playerId)
        {
            return _repositoryWrapper.Repository<Gift>()
                .Find(x => x.ReceivePlayerId == playerId && x.GiftStatus == GiftStatus.Accepted).GroupBy(x => x.Item)
                .Select(x => new ReceiveGiftResponse(){
                    Item = x.Key!,
                    Quantity = x.Sum(c => c.Quantity)
                });
        }

        public async Task<string?> ReceiveGiftAsync(ReceiveGiftRequest request)
        {
            Gift? gift = await _repositoryWrapper.Repository<Gift>()
                .Find(x => 
                    x.ItemCode == request.ItemCode 
                    && x.ReceivePlayerId == request.ReceivePlayerId 
                    && x.GiftStatus == GiftStatus.Pending)
                .Include(x => x.ReceivePlayer)
                .Include(x => x.SendPlayer)
                .Include(x => x.Item)
                .FirstOrDefaultAsync();
            if(gift == null) return "DoesNotHaveGift";
            gift.GiftStatus = GiftStatus.Accepted;
            await _repositoryWrapper.Repository<Gift>().UpdateAsync(gift);
            await gift.NotifyAsync(NotificationType.AcceptGift);
            return null;
        }

        public async Task<string?> RejectGiftAsync(RejectGiftRequest request)
        {
            Gift? gift = await _repositoryWrapper.Repository<Gift>()
                .Find(x => 
                    x.ItemCode == request.ItemCode 
                    && x.ReceivePlayerId == request.ReceivePlayerId 
                    && x.GiftStatus == GiftStatus.Pending)
                .FirstOrDefaultAsync();
            if(gift == null) return "DoesNotHaveGift";
            gift.GiftStatus = GiftStatus.Rejected;
            await _repositoryWrapper.Repository<Gift>().UpdateAsync(gift);
            return null;
        }

        public async Task SendGiftAsync(SendGiftRequest request)
        {
            Gift? gift = await _repositoryWrapper.Repository<Gift>()
                .Find(x => 
                    x.ItemCode == request.ItemCode 
                    && x.ReceivePlayerId == request.ReceivePlayerId 
                    && x.GiftStatus == GiftStatus.Pending)
                .FirstOrDefaultAsync();
            if(gift == null)
            {
                gift = new(request);
                await _repositoryWrapper.Repository<Gift>().AddAsync(gift);
            }
            else
            {
                gift.Quantity += request.Quantity;
                if(gift.Quantity > 0)
                {
                    await _repositoryWrapper.Repository<Gift>().UpdateAsync(gift);
                }
                else
                {
                    await _repositoryWrapper.Repository<Gift>().DeleteAsync(gift);
                }
            }
            await gift.NotifyAsync(NotificationType.ReceiveGift);
        }
        
    }
}