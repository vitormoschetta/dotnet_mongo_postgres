namespace dotnet_mongodb.Application.User;

public class AuthInput
{
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class AuthResponse : AuthInput
{ }