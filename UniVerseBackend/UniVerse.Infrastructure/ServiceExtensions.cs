using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniVerse.Core.Entities;
using UniVerse.Core.Interfaces.IRepositories;
using UniVerse.Infrastructure.Data;
using UniVerse.Infrastructure.Repositories;

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

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IFriendshipRepository, FriendshipRepository>();
        services.AddScoped<ILikeRepository, LikeRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<INewsRepository, NewsRepository>();
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<IGroupEventRepository, GroupEventRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();

        services.AddSignalR();
        
        return services;
    }
}