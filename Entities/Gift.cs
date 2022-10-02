using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationTest.Notifications;
using NotificationTest.Requests;

namespace NotificationTest.Entities
{
    public class Gift : NotifyHandler
    {
        public Gift(SendGiftRequest request)
        {
            SendPlayerId = request.SendPlayerId;
            ReceivePlayerId = request.ReceivePlayerId;
            GiftStatus = GiftStatus.Pending;
            ItemCode = request.ItemCode;
            Quantity = request.Quantity;
        }

        public Guid Id { get; set; }
        public Guid SendPlayerId { get; set; }
        public Player? SendPlayer { get; set; }
        public Guid ReceivePlayerId { get; set; }
        public Player? ReceivePlayer { get; set; }
        public GiftStatus GiftStatus { get; set; }
        public string ItemCode { get; set; } = default!;
        public Item? Item { get; set; }
        public int Quantity { get; set; }
    }
    public enum GiftStatus
    {
        Pending = 1,
        Accepted = 2,
        Rejected = 3
    }
    public class GiftConfigurations : IEntityTypeConfiguration<Gift>
    {
        public void Configure(EntityTypeBuilder<Gift> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
        }
    }

}