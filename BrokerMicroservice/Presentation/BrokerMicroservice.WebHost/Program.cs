using BrokerMicroservice.Application.Services;
using BrokerMicroservice.Application.Services.Abstractions;
using BrokerMicroservice.Application.Services.Mapping;
using BrokerMicroservice.Domain.Entities;
using BrokerMicroservice.Infrastructure.EntityFramework;
using BrokerMicroservice.Infrastructure.EntityFramework.RepositoriesEF;
using BrokerMicroservice.Repositories.Abstractions;
using BrokerMicroservice.WebHost.Helpers;
using BrokerMicroservice.WebHost.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;


namespace BrokerMicroservice.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()));

            builder.Services.AddControllers()
                .AddJsonOptions(o =>
                    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString(nameof(ApplicationDbContext));

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string for BrokerMicroseviceDbContext is not configured.");
            }

            builder.Services.AddNpgsql<ApplicationDbContext>(connectionString, options =>
            {
                options.MigrationsAssembly("BrokerMicrosevice.Infrastructure.EntityFramework");

            });

            builder.Services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "BrokerMicrosevice API",
                        Description = "API BrokerMicrosevice предоставляет функционал для управления брокерским счётом."
                    });
                });


            builder.Services.AddAutoMapper(cfg => { }, typeof(PresentationProfile), typeof(ApplicationProfile));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString, x =>
                    x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));
            });

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IRepository<Broker, Guid>, EFRepository<Broker, Guid>>();
            builder.Services.AddScoped<IRepository<Client, Guid>, EFRepository<Client, Guid>>();
            builder.Services.AddScoped<IRepository<Asset, Guid>, EFRepository<Asset, Guid>>();
            builder.Services.AddScoped<IRepository<Card, Guid>, EFRepository<Card, Guid>>();
            builder.Services.AddScoped<IRepository<Portfolio, Guid>, EFRepository<Portfolio, Guid>>();
            builder.Services.AddScoped<IRepository<Transaction, Guid>, EFRepository<Transaction, Guid>>();

            builder.Services.AddScoped<IBrokerApplicationService, BrokerApplicationService>();
            builder.Services.AddScoped<IClientApplicationService, ClientApplicationService>();
            builder.Services.AddScoped<IAssetApplicationService, AssetApplicationService>();
            builder.Services.AddScoped<ICardApplicationService, CardApplicationService>();
            builder.Services.AddScoped<IPortfolioApplicationService, PortfolioApplicationService>();
            builder.Services.AddScoped<ITransactionApplicationService, TransactionApplicationService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            // app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.MigrateDatabase<ApplicationDbContext>();

            app.Run();
        }
    }
}