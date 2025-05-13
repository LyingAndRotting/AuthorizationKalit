using Authorization.Application.Features.Login.Queries;
using Authorization.Application.Features.Login.Queries.Login;
using Authorization.Domain.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<IAuthService, AuthService>();
        //services.AddSingleton<IJsonCacheService, JsonCacheService>();
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
                options.LoginPath = "/login";
            });
        services.AddAuthorization();
        services.AddDbContext<AuthorizationContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Db")));
        services.AddMediatR(serviceConfiguration => 
        {
            serviceConfiguration.RegisterServicesFromAssembly(typeof(LoginQuery).Assembly);
        });

        return services;
    }
}