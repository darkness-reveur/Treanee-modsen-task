﻿namespace MeetupPlatform.Api.Authentication.DependencyInjection;

using MeetupPlatform.Api.Authentication.Configuration;
using MeetupPlatform.Api.Authentication.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;

public static class InjectionExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration applicationConfiguration)
    {
        var authenticationConfiguration = new AuthenticationConfiguration(applicationConfiguration);
        
        services
            .AddSingleton(authenticationConfiguration)
            .AddSingleton<TokenHelper>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = authenticationConfiguration.ValidationParameters;
            });
    }
}
