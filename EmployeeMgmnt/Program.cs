using EmployeeMgmnt;
using EmployeeMgmnt.Models;
//using Microsoft.EntityFrameworkCore;
//using Prometheus;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// Register database context
//builder.Services.AddDbContext<EmployeeContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.UseMetricServer();

//app.MapControllers();

//app.Run();


//var builder = WebApplication.CreateBuilder(args);
//var app = Startup.InitializeApp(args);
//app.Run();


using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace EmployeeMgmnt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}