﻿namespace MeetupPlatform.Api.Features.Meetups.LeftRootComment;

using AutoMapper;
using MeetupPlatform.Api.Authentication.Helpers;
using MeetupPlatform.Api.Domain.Comments;
using MeetupPlatform.Api.Persistence.Context;
using MeetupPlatform.Api.Seedwork.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Meetups)]
public class LeftCommentFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public LeftCommentFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <summary>
    /// Left comment to meetup.
    /// </summary>
    /// <response code="404">If needed meetup is null.</response>
    /// <response code="201">Returns the new created item.</response>
    [HttpPost("/api/meetups/{id:guid}/comments")]
    [Authorize(Roles = Roles.PlainUser)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CreatedCommentDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> LeftRootComment([FromRoute] Guid id, [FromBody] CreationCommentDto creationCommentDto)
    {
        var meetup = await context.Meetups
            .Include(meetup => meetup.SignedUpUsers)
            .Where(meetup => meetup.Id == id)
            .SingleOrDefaultAsync();
        if(meetup is null)
        {
            return NotFound();
        }

        var user = await context.PlainUsers.Where(plainUser => plainUser.Id == CurrentUser.UserId).SingleOrDefaultAsync();
        if(user is null)
        {
            return Unauthorized();
        }

        bool isUserSignedUpForMeetup = meetup.SignedUpUsers.Any(plainUser => plainUser.Id == user.Id);
        if(!isUserSignedUpForMeetup)
        {
            // User can't to left comment to meetup that hasn't had he as signed up.
            return Forbid();
        }

        var rootComment = new RootComment
        {
            Text = creationCommentDto.Text,
            MeetupId = id,
            PlainUserId = user.Id
        };
        context.RootComments.Add(rootComment);
        await context.SaveChangesAsync();

        var createdCommentDto = mapper.Map<CreatedCommentDto>(rootComment);
        return Created(createdCommentDto);
    }
}
