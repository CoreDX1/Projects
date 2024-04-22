﻿namespace Api.Models;

public partial class Tasks
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? DueDate { get; set; }

    public bool Completed { get; set; }

    public DateTime CratedAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public virtual Account User { get; set; } = null!;
}