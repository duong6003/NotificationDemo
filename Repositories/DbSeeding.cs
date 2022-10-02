using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificationTest.Entities;

namespace Web.Repositories
{
    public class DbSeeding
    {
        private readonly ApplicationDbContext dbContext;

        public DbSeeding(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Seeding()
        {
            Console.WriteLine("---> Start Seeding");
            dbContext.Database.EnsureCreated();
            if(!await dbContext.Items!.AnyAsync())
            {
                await dbContext.Items!.AddRangeAsync(new List<Item>(){
                    new Item(){ Code = "Item-1", Name = "Hoa Hong" },
                    new Item(){ Code = "Item-2", Name = "Hoa Hue" },
                    new Item(){ Code = "Item-3", Name = "Hoa Lan" },
                    new Item(){ Code = "Item-4", Name = "Hoa Cuc" },
                    new Item(){ Code = "Item-5", Name = "Hoa Nhai" },
                    new Item(){ Code = "Item-6", Name = "Hoa Muoi Gio" },
                    new Item(){ Code = "Item-7", Name = "Hoa Huong Duong" },
                    new Item(){ Code = "Item-8", Name = "Hoa Mai" },
                    new Item(){ Code = "Item-9", Name = "Hoa Tulip" },
                    new Item(){ Code = "Item-10", Name = "Hoa Hai Duong"}
                });
            }
            if(!await dbContext.Players!.AnyAsync())
            {
                await dbContext.Players!.AddRangeAsync(new List<Player>(){
                    new Player(){ Name = "Player-1", Id = Guid.NewGuid() },
                    new Player(){ Name = "Player-2", Id = Guid.NewGuid() },
                    new Player(){ Name = "Player-3", Id = Guid.NewGuid() },
                    new Player(){ Name = "Player-4", Id = Guid.NewGuid() },
                    new Player(){ Name = "Player-5", Id = Guid.NewGuid() },
                    new Player(){ Name = "Player-6", Id = Guid.NewGuid() },
                    new Player(){ Name = "Player-7", Id = Guid.NewGuid() },
                    new Player(){ Name = "Player-8", Id = Guid.NewGuid() },
                    new Player(){ Name = "Player-9", Id = Guid.NewGuid() },
                    new Player(){ Name = "Player-10", Id = Guid.NewGuid() }
                });
            }
            await dbContext.SaveChangesAsync();
            Console.WriteLine("---> Seeding Success");
        }
    }
}