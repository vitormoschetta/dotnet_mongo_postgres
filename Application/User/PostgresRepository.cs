using dotnet_mongodb.Data.Postgres;

namespace dotnet_mongodb.Application.User;

public class UserPostgresRepository : IUserRepository
{
    private readonly PostgresDbContext _db;

    public UserPostgresRepository(PostgresDbContext db)
    {
        _db = db;
    }

    public void Create(UserEntity entity)
    {
        _db.Users.Add(entity);
        _db.SaveChanges();
    }

    public UserEntity? GetByEmail(string email)
    {
        return _db.Users.FirstOrDefault(x => x.Email == email);
    }
}
