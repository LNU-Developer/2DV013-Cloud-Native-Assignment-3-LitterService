using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using Newtonsoft.Json;
using LitterService.Application.Exceptions;

namespace LitterService.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            var issuer = Environment.GetEnvironmentVariable("JWTISSUER") ?? "AuthService";
            var audience = Environment.GetEnvironmentVariable("JWTAUDIENCE") ?? "LitterService";
            var pubKey = Environment.GetEnvironmentVariable("JWTPUBLICKEY") ?? "test";

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var publicKey = pubKey;
                var rsa = RSA.Create();
                rsa.ImportFromPem(publicKey.ToCharArray());

                options.IncludeErrorDetails = false;
                options.RequireHttpsMetadata = false;
                // Configure the actual Bearer validation
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new RsaSecurityKey(rsa),
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    RequireSignedTokens = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true
                };

                options.Events = new JwtBearerEvents()
                {
                    OnChallenge = async c =>
                    {
                        c.HandleResponse();
                        var message = JsonConvert.SerializeObject(new ErrorMessage
                        {
                            StatusCode = 401,
                            Message = "Access token invalid or not provided."
                        });
                        c.Response.ContentType = "application/json";
                        c.Response.StatusCode = 401;
                        await c.Response.WriteAsync(message);
                        return;
                    }
                };
            });
            return services;
        }
    }
}
