﻿namespace MeetupPlatformApi.Authentication.Helpers;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MeetupPlatformApi.Authentication.Configuration;
using MeetupPlatformApi.Domain;
using Microsoft.IdentityModel.Tokens;

public class TokenHelper
{
    private readonly AuthenticationConfiguration configuration;
    private readonly JwtSecurityTokenHandler tokenHandler;

    public TokenHelper(AuthenticationConfiguration configuration)
    {
        this.configuration = configuration;
        tokenHandler = new JwtSecurityTokenHandler();
    }

    public string IssueAccessToken(User user) =>
        IssueToken(
            payload: new Dictionary<string, object>
            {
                {ClaimTypes.NameIdentifier, user.Id}
            },
            lifetime: configuration.AccessTokenLifetime);

    private string IssueToken(IDictionary<string, object> payload, TimeSpan lifetime)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Claims = payload,
            Expires = DateTime.UtcNow.Add(lifetime),
            SigningCredentials = configuration.SigningCredentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}