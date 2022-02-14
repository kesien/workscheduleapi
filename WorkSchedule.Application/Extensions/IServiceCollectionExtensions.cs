using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using WorkSchedule.Application.Helpers;
using WorkSchedule.Application.Persistency;
using WorkSchedule.Application.Services.DropboxService;
using WorkSchedule.Application.Services.EmailService;
using WorkSchedule.Application.Services.FileService;
using WorkSchedule.Application.Services.HolidayService;
using WorkSchedule.Application.Services.RequestService;
using WorkSchedule.Application.Services.ScheduleService;

namespace WorkSchedule.Application.Extensions
{
    public static class IServiceCollectionExtenions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JWTSettings:Secret").Value))
                    };
                });
            return services;
        }

        public static IServiceCollection AddDatabaseConnection(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
            {
               if (isDevelopment) 
                {
                    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                    options.EnableSensitiveDataLogging();
                }
                else 
                {
                    options.UseNpgsql(GetConnectionString());
                }
            });
            return services;
        }

        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailClientSettings>(options =>
            {
                options.ApiKey = configuration["EmailClientSettings:ApiKey"];
                options.FromName = configuration["EmailClientSettings:FromName"];
                options.FromEmail = configuration["EmailClientSettings:FromEmail"];
                options.NewScheduleTemplateId = configuration["EmailClientSettings:NewScheduleTemplateId"];
                options.ScheduleModifiedTemplateId = configuration["EmailClientSettings:ScheduleModifiedTemplateId"];
            });
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IDropboxService, DropboxService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IHolidayService, HolidayService>();
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }

        private static string GetConnectionString()
        {
            string connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var databaseUri = new Uri(connectionUrl);
            string db = databaseUri.LocalPath.TrimStart('/');
            string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);
            var connectionString = $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;Include Error Detail=True;";
            return connectionString;
        }
    }
}
