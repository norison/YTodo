﻿namespace YTodo.Application.Features.Commands.RegisterUser;

public class RegisterUserCommandHandlerResult
{
    public string Token { get; set; } = String.Empty;
    public DateTime ExpirationDateTime { get; set; }
}