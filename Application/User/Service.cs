using dotnet_mongodb.Application.Shared;
using MongoDB.Driver;

namespace dotnet_mongodb.Application.User;

public class UserService
{
    private readonly IUserRepository _repository;
    private readonly Jwt _jwt;
    private static readonly string[] UserRoles = { "user" };

    public UserService(IUserRepository repository, Jwt jwt)
    {
        _repository = repository;
        _jwt = jwt;
    }

    public async Task<Output> Authenticate(AuthInput input)
    {
        var user = _repository.GetByEmail(input.Email);

        if (user == null)
        {
            user = new UserEntity { Email = input.Email };
            _repository.Create(user);
        }

        var token = await _jwt.GenerateToken(
            username: user.Email,
            email: user.Email,
            roles: UserRoles
        );

        var authResponse = new AuthResponse
        {
            Email = user.Email,
            Token = token
        };

        return Output.Ok(authResponse);
    }
}
