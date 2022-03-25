﻿namespace MeetupPlatformApi.DataTransferObjects;

public class MeetupInputDto
{
    public string Name { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Description { get; set; }
}

