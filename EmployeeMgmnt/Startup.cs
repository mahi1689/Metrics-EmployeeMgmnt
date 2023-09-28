using EmployeeMgmnt.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Identity.Client;
//using Prometheus;

//namespace EmployeeMgmnt
//{
//    public class Startup
//    {
//        public static WebApplication InitializeApp(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);
//            ConfigureServices(builder);
//            var app = builder.Build();
//            Configure(app);
//            return app;
//        }



//        public static void ConfigureServices(WebApplicationBuilder builder)
//        {
//            _ = builder.Services.AddCors(options =>
//            {
//                options.AddPolicy(name: "AllowOrigin", builder =>
//                {
//                    builder.WithOrigins("http://localhost:4200").AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
//                });
//            });


//            var metricServer = new MetricServer(hostname:"localhost", port: 9090);


//            //builder.Services.AddDbContext<EmployeeContext>(
//            //    options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyDBConnection")));
//            //builder.Services.AddDbContext<Applicationcontext>(
//            //   options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyDBConnection")));


//            // Register database context
//            builder.Services.AddDbContext<EmployeeContext>(options =>
//                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));



//            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();
//            builder.Services.AddControllers();







//        }

//        public static void Configure(WebApplication app)
//        {
//            var metricServer = new MetricServer(hostname: "localhost", port: 9090);
//            metricServer.Start();

//            app.UseMetricServer("/metrics");

//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();
//            app.UseAuthentication();
//            app.UseRouting();
//            app.UseAuthorization();



//            // app.UseSession();
//            app.MapControllers();



//            app.UseCors();


//            //app.UseMetricServer();

//            app.UseHttpMetrics();

//            #pragma warning disable
//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllers();
//            });




//        }
//    }
//}


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Prometheus;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMgmnt
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add CORS policy if needed
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });



            // Register database context
            services.AddDbContext<EmployeeContext>(options =>
            {


                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
                       .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()));

                
            });

            // Configure JSON serialization
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger", Version = "v1" });
            });

           
            services.AddMetrics();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable Swagger for API documentation (optional)
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Enable CORS (optional)
            //app.UseCors("AllowOrigin");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowOrigin");
            app.UseAuthorization();

            app.UseMetricServer();
            app.UseHttpMetrics();
            //app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
