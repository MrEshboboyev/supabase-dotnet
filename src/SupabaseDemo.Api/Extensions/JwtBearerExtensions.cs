using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace SupabaseDemo.Api.Extensions;

public static class JwtBearerExtensions
{
    public static IServiceCollection AddJwtBearerAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET")!;
        var validIssuer = Environment.GetEnvironmentVariable("VALID_ISSUER")!;

        // Decode the base64 JWT secret
        var key = Convert.FromBase64String(jwtSecret); // 👈 Fix here

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key), // Now uses correct bytes
                    ValidIssuer = validIssuer,
                    ValidAudience = "authenticated",
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}