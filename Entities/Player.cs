using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NotificationTest.Entities
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public ICollection<Gift>? SendGifts { get; set; }
        public ICollection<Gift>? ReceiveGifts { get; set; }
    }
     public class PlayerConfigurations : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
        }
    }
}