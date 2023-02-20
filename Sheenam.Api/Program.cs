// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Brokers.Tokens;
using Sheenam.Api.Services.Foundations.Guests;
using Sheenam.Api.Services.Foundations.Hosts;
using Sheenam.Api.Services.Foundations.Security.GuestSecurity;
using Sheenam.Api.Services.Foundations.Security.HostSecurity;
using Sheenam.Api.Services.Foundations.Security.PasswordHash;
using Sheenam.Api.Services.Processings.GuestProcessing;
using Sheenam.Api.Services.Processings.HostProcessing;
using Sheenam.Api.Services.Processings.SecurityProcessing.GuestSecurityPocessing;
using Sheenam.Api.Services.Processings.SecurityProcessing.HostSecurityProcessing;

namespace Sheenam.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<StorageBroker>();
            AddBrokers(builder);
            AddFoundationServices(builder);
            AddConfigurationServices(builder);
            AddProcessingServices(builder);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddRouting(options =>
                options.LowercaseUrls = true);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sheenam"));

            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }

        private static void AddConfigurationServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwt =>
                    {
                        string? key = builder.Configuration.GetSection("Jwt").GetValue<string>("Key");
                        byte[] convertKeyToBytes = Encoding.ASCII.GetBytes(key);

                        jwt.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuerSigningKey = true,

                            IssuerSigningKey =
                                new SymmetricSecurityKey(convertKeyToBytes),

                            ValidateIssuer = false,
                            ValidateAudience = false,
                            RequireExpirationTime = true,
                            ValidateLifetime = true
                        };
                    });
        }

        private static void AddBrokers(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IStorageBroker, StorageBroker>();
            builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
            builder.Services.AddSingleton<ITokenBroker, TokenBroker>();
        }

        private static void AddFoundationServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IGuestService, GuestService>();
            builder.Services.AddTransient<IHostService, HostService>();
            builder.Services.AddScoped<IPasswordHashServise, PasswordHashServise>();
            builder.Services.AddTransient<IGuestSecurityService, GuestSecurityService>();
            builder.Services.AddTransient<IHostSecurityService, HostSecurityService>();
        }

        private static void AddProcessingServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IGuestSecurityProcessingService, GuestSecurityProcessingService>();
            builder.Services.AddTransient<IHostSecurityProcessingService, HostSecurityProcessingService>();
            builder.Services.AddTransient<IGuestProcessingService, GuestProcessingService>();
            builder.Services.AddTransient<IHostProcessingService, HostProcessingService>();
        }
    }
}