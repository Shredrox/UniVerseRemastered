using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniVerse.Core.Entities;
using UniVerse.Infrastructure.Data;

namespace UniVerse.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<UniVerseDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("UniVerseDb")));
        
        services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);
        services.AddAuthorizationBuilder();

        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<UniVerseDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}