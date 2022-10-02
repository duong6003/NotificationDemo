using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NotificationTest.Notifications;
using NotificationTest.Services;
using Web.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var configuration = builder.Configuration;

services.AddDbContextPool<ApplicationDbContext>(
    options => options.UseMySql(configuration["DbConnection"],
    ServerVersion.AutoDetect(configuration["DbConnection"])
));
services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
services.AddScoped<IPlayerService, PlayerService>();

services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
}); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
        DbSeeding seeding = new(dbContext!);
        await seeding.Seeding();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.GetBaseException().ToString());
    }
}

// register notification provider
DbNotification.Register(app.Services.GetRequiredService<IServiceScopeFactory>());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
