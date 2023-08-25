using DotNetCore.CAP.Messages;
using EmailService.Consumers;
using EmailService.DataContext;
using EmailService.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);






#region Event Drive - DotnetCap log
var capConnectionString = builder.Configuration.GetConnectionString("CapLogSqlServerConnection");
builder.Services.AddDbContext<DotnetCapDbContext>(options =>
   options.UseSqlServer(capConnectionString,
       builder => builder.MigrationsAssembly(typeof(DotnetCapDbContext).Assembly.FullName)));
#endregion

#region
builder.Services.AddCap(x =>
{
    x.UseEntityFramework<DotnetCapDbContext>();
    x.UseSqlServer(builder.Configuration.GetConnectionString("CapLogSqlServerConnection"));
    x.UseRabbitMQ(options =>
    {
        options.ExchangeName = "InventoryManagement.API";
        options.BasicQosOptions = new DotNetCore.CAP.RabbitMQOptions.BasicQos(3);
        options.ConnectionFactoryOptions = opt =>
        {
            opt.HostName = builder.Configuration.GetSection("RabbitMQ:Host").Value;
            opt.UserName = builder.Configuration.GetSection("RabbitMQ:Username").Value;
            opt.Password = builder.Configuration.GetSection("RabbitMQ:Password").Value;
            opt.Port = Convert.ToInt32(builder.Configuration.GetSection("RabbitMQ:Port").Value);
            opt.CreateConnection();
        };
    });
    //x.UseDashboard(opt => opt.PathMatch = "/cap-dashboard");
    x.FailedRetryCount = 5; //Baþarýsýz deneme sayýsý
    //x.FailedMessageExpiredAfter = 15 * 24 * 3600; //15 gün sonra mesajý otomatik siler
    //x.CollectorCleaningInterval = 5000; //5 saniye sonra otomatik siler
    x.UseDispatchingPerGroup = true;
    x.FailedThresholdCallback = failed =>
    {
        var logger = failed.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError($@"A message of type {failed.MessageType} failed after executing {x.FailedRetryCount} several times, 
                        requiring manual troubleshooting. Message name: {failed.Message.GetName()}");
    };
    x.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
});
#endregion




#region Smtp Servis Bilgisi
var smtpSettings = builder.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
builder.Services.AddSingleton<IMailService>(new MailService(smtpSettings));
#endregion




#region Consumers
builder.Services.AddScoped<CreatedAssignedProductConsumer>();
builder.Services.AddScoped<ProductTransferConsumer>();
#endregion




var app = builder.Build();
app.UseHttpsRedirection();
app.Run();
