﻿using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.Entities;

public class UserEntity
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public List<RefreshTokenEntity> RefreshTokens { get; set; }
}
