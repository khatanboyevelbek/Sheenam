// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Services.Foundations.Guests;
using Sheenam.Api.Services.Foundations.Hosts;

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
                        var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

                        jwt.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuerSigningKey = true,

                            IssuerSigningKey =
                                new SymmetricSecurityKey(key),

                            ValidateIssuer = true,
                            ValidateAudience = true,
                            RequireExpirationTime = true,
                            ValidateLifetime = true
                        };
                    });
        }

        private static void AddBrokers(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IStorageBroker, StorageBroker>();
            builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
        }

        private static void AddFoundationServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IGuestService, GuestService>();
            builder.Services.AddTransient<IHostService, HostService>();
        }
    }
}