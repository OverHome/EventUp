﻿namespace EventUp.Domain.Models;

public class JwtToken
{
    public string UserName { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}