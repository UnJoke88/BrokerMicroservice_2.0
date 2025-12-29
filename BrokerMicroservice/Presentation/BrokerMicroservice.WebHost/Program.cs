using BrokerMicroservice.Infrastructure.EntityFramework;
using BrokerMicroservice.WebHost.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BrokerMicroservice.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString(nameof(ApplicationDbContext));

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string for BrokerMicroserviceDbContext is not configured.");
            }

            builder.Services.AddNpgsql<ApplicationDbContext>(connectionString, options =>
            {
                options.MigrationsAssembly("BrokerMicroservice.Infrastructure.EntityFramework");

            });

            builder.Services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Train station API",
                        Description = "The Train station API provides endpoints for auction management. This API allows you to put lots up for bidding and participate in an auction."
                    });
                });
            builder.Services.AddDbContext<ApplicationDbContext>(
                            options =>
                            {
                                options.UseNpgsql(connectionString);
                            });

            builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter()));

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

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MigrateDatabase<ApplicationDbContext>();

            app.Run();
        }
    }
}