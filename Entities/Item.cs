using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NotificationTest.Entities
{
    public class Item
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
    public class ItemConfigurations : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Code);
        }
    }
}