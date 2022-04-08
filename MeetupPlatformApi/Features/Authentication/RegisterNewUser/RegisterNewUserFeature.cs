﻿namespace MeetupPlatformApi.Features.Authentication.RegisterNewUser;

using AutoMapper;
using BCrypt.Net;
using MeetupPlatformApi.Authentication.Helpers;
using MeetupPlatformApi.Domain;
using MeetupPlatformApi.Domain.Users;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Authentication)]
public class RegisterNewUserFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;
    private readonly TokenHelper tokenHelper;

    public RegisterNewUserFeature(ApplicationContext context, IMapper mapper, TokenHelper tokenHelper)
    {
        this.context = context;
        this.mapper = mapper;
        this.tokenHelper = tokenHelper;
    }

    /// <summary>
    /// Register new organizer.
    /// </summary>
    /// <response code="201">Returns registration result data.</response>
    /// <response code="400">If provided username is already taken.</response>
    [HttpPost("/api/users/organizer")]
    [ProducesResponseType(typeof(RegistrationResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterNewOrganizer([FromBody] RegistrationDto registrationDto)
    {
        var isUsernameAlreadyTaken = await context.Users.AnyAsync(user => user.Username == registrationDto.Username);
        if (isUsernameAlreadyTaken)
        {
            return BadRequest("Provided username is already taken");
        }

        var user = mapper.Map<Organizer>(registrationDto);
        user.Password = BCrypt.HashPassword(user.Password);
        context.Organizers.Add(user);

        var refreshToken = new RefreshToken()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id    
        };
        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync();

        var tokenPair = tokenHelper.IssueTokenPair(user, refreshToken.Id);
        var registrationResultDto = new RegistrationResultDto
        {
            UserInfo = mapper.Map<RegistrationResultDto.UserInfoDto>(user),
            TokenPair = mapper.Map<TokenPairDto>(tokenPair)
        };
        return Created(registrationResultDto);
    }

    /// <summary>
    /// Register new plain user.
    /// </summary>
    /// <response code="201">Returns registration result data.</response>
    /// <response code="400">If provided username is already taken.</response>
    [HttpPost("/api/users/plain-user")]
    [ProducesResponseType(typeof(RegistrationResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterNewPlainUser([FromBody] RegistrationDto registrationDto)
    {
        var isUsernameAlreadyTaken = await context.Users.AnyAsync(user => user.Username == registrationDto.Username);
        if (isUsernameAlreadyTaken)
        {
            return BadRequest("Provided username is already taken");
        }

        var user = mapper.Map<PlainUser>(registrationDto);
        user.Password = BCrypt.HashPassword(user.Password);
        context.PlainUsers.Add(user);

        var refreshToken = new RefreshToken()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id
        };
        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync();

        var tokenPair = tokenHelper.IssueTokenPair(user, refreshToken.Id);
        var registrationResultDto = new RegistrationResultDto
        {
            UserInfo = mapper.Map<RegistrationResultDto.UserInfoDto>(user),
            TokenPair = mapper.Map<TokenPairDto>(tokenPair)
        };
        return Created(registrationResultDto);
    }
}
