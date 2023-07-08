using Microsoft.EntityFrameworkCore;
using Newtonsoft;
using Newtonsoft.Json.Serialization;
using WebApplication5.Data;
using WebApplication5.Services;

namespace WebApplication5;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services.AddScoped<IEmployeeService, EmployeeService>();

        //builder.services.adddbcontext<employeedbcontext>
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
}