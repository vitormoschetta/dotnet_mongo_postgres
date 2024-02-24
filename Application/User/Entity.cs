using dotnet_mongodb.Application.Shared;

namespace dotnet_mongodb.Application.User;

public class UserEntity : BaseEntity
{
    public string Email { get; set; } = string.Empty;
}