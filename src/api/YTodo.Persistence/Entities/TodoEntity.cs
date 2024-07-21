﻿namespace YTodo.Persistence.Entities;

public class TodoEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    
    public int UserId { get; set; }
    public UserEntity? User { get; set; }
}