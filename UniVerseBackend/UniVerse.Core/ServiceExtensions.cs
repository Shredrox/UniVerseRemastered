﻿using Microsoft.Extensions.DependencyInjection;
using UniVerse.Core.Interfaces.IServices;
using UniVerse.Core.Services;

namespace UniVerse.Core;

public static class ServiceExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IFriendshipService, FriendshipService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ILikeService, LikeService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<INewsService, NewsServices>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IGroupEventService, GroupEventService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<INotificationService, NotificationService>();
        
        return services;
    }
}