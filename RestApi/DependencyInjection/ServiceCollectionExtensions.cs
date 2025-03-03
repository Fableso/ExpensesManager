using System.Security.Cryptography;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ExpenseManagement.TransactionEntities.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationServices(
        this IServiceCollection services,
        string securityKey)
    {
        services.AddHttpContextAccessor();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var rsa = RSA.Create();
                rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(securityKey), out _);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new RsaSecurityKey(rsa),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

        return services;
    }
    
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Expenses API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {eyJhbGciOiJSUzI1NiJ9.eyJzdWIiOiJ0ZXN0IiwidXNlcklkIjoyLCJpYXQiOjE3NDA4NTUyNDcsImV4cCI6MTc0MDg1NzA0N30.AWX3qcsVnnmT_OQJO_KQ8Iu6IasU2rn2h6AELD6cIxlSn3g4by2UDaS5jEZpK0rSHPg1NImBVOueVIVRIFYq1F7WPGa-zPcQ15uY6FXbJCotF252j4WMoSOs_wZPzDULjz63S1U3TyN8gZCAKCIw8IsiGSCaZsBBf4Ej4In_pCy6e9pnCkTYTua8vNPxpQt5gRBodQ8ir_NEX0vRKGndPNhrFpTXqUSNI1YJvA_ONjqn_3R5MykM_m_eEmkGKdM9nu6lOrT-vYSbMbevHAAvEQyexIJoShqHIlWTKYe8pKAZDB9qk9lGyJaXqpAoGpt4Rzk8jLaTQyHyaBhKmjqUrQ}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
        });

        return services;
    }
}
