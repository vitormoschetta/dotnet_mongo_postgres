using dotnet_mongodb.Application.Shared;
using dotnet_mongodb.Data;
using MongoDB.Driver;

namespace dotnet_mongodb.Application.User;

public class UserService
{
    private readonly MongoDbContext _db;
    private readonly Jwt _jwt;
    private static readonly string[] UserRoles = { "user" };

    public UserService(MongoDbContext db, Jwt jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    public async Task<Output> Authenticate(AuthInput input)
    {
        var user = await _db.Users.Find(x => x.Email == input.Email).FirstOrDefaultAsync();

        if (user == null)
        {
            user = new UserEntity { Email = input.Email };
            await _db.Users.InsertOneAsync(user);
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
