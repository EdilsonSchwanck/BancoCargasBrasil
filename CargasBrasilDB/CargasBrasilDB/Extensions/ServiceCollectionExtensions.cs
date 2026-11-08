using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;
using CargasBrasilDB.Common;
using CargasBrasilDB.Data.Context;
using CargasBrasilDB.Repositories;
using CargasBrasilDB.Repositories.Interfaces;
using CargasBrasilDB.Security;
using CargasBrasilDB.Services;
using CargasBrasilDB.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CargasBrasilDB.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException(
                    "ConnectionStrings:DefaultConnection não configurada. Use 'dotnet user-secrets' ou a variável de ambiente ConnectionStrings__DefaultConnection.");

            var serverVersion = ServerVersion.Parse(configuration["Database:ServerVersion"] ?? "8.0.0-mysql");
            services.AddDbContext<DataContext>(o => o.UseMySql(connectionString, serverVersion));
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDriverRepository, DriverRepository>();

            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBackofficeService, BackofficeService>();

            services.AddSingleton<IPasswordHasher, Pbkdf2PasswordHasher>();
            services.AddSingleton<ITokenService, JwtTokenService>();
            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            services.AddOptions<JwtSettings>()
                .Bind(configuration.GetSection(JwtSettings.SectionName))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.Configure<AdminSettings>(configuration.GetSection(AdminSettings.SectionName));

            var jwt = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>() ?? new JwtSettings();
            if (string.IsNullOrWhiteSpace(jwt.Key) || jwt.Key.Length < 32)
                throw new InvalidOperationException(
                    "Jwt:Key não configurada ou com menos de 32 caracteres. Use 'dotnet user-secrets' ou a variável de ambiente Jwt__Key.");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = !env.IsDevelopment();
                    options.MapInboundClaims = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwt.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwt.Audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
                        RoleClaimType = ClaimTypes.Role,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });

            services.AddAuthorization();
            return services;
        }

        public static IServiceCollection AddApiControllers(this IServiceCollection services)
        {
            services.AddControllers()
                .ConfigureApiBehaviorOptions(o =>
                {
                    // Erros de validação no mesmo formato { sucesso, mensagem, data }.
                    o.InvalidModelStateResponseFactory = ctx =>
                    {
                        var errors = ctx.ModelState
                            .Where(e => e.Value?.Errors.Count > 0)
                            .ToDictionary(e => e.Key, e => e.Value!.Errors.Select(x => x.ErrorMessage).ToArray());

                        return new BadRequestObjectResult(new ApiResponse(false, "Dados inválidos.", errors));
                    };
                });
            return services;
        }

        public static IServiceCollection AddAuthRateLimiting(this IServiceCollection services)
        {
            services.AddRateLimiter(o =>
            {
                o.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                o.AddPolicy(RateLimitPolicies.Auth, http =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        http.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        _ => new FixedWindowRateLimiterOptions { PermitLimit = 10, Window = TimeSpan.FromMinutes(1) }));
            });
            return services;
        }

        public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CargasBrasil", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Autenticação via JWT. Informe apenas o token.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
                        Array.Empty<string>()
                    }
                });
            });
            return services;
        }
    }
}
