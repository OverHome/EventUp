namespace EventUp.Application.Dto.User;

public class RegisterUserDto
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string RepeatPassword { get; set; } = string.Empty;
}