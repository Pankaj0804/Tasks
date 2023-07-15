using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using WebApplication5.Data;
using WebApplication5.Services;

namespace WebApplication5;

public class Program
{
    [Obsolete]
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        //var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        //try
        //{
        //    logger.Debug("Starting application...");
        //    CreateHostBuilder(args).Build().Run();
        //}
        //catch (Exception ex)
        //{
        //    logger.Error(ex, "An error occurred while starting the application.");
        //    throw;
        //}
        //finally
        //{
        //    LogManager.Shutdown();
        //}


        // Add services to the container
        //builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
        builder.Services.AddControllers();

        builder.Services.AddScoped<IEmployeeService, EmployeeService>();

        //builder.Services.AddLogging(loggingBuilder =>
        //{
        //    loggingBuilder.ClearProviders();
        //    loggingBuilder.AddNLog();
        //});
       builder.Host.UseNLog();

        // builder.Services.AddDbContext<EmployeeDbContext>
        //  (options => options.UseInMemoryDatabase("EmployeesDb"));

        builder.Services.AddDbContext<EmployeeDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("EmployeeAppCon")
        ));
        //coming from WebApplication5.Data
        //injecting dbcontext to services inmemory db to talk with database
        //using Microsoft.EntityFrameworkCore which gives option to use inmemory database

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseNLog(); // Add this line to configure NLog
    }
}
