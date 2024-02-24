using MongoDB.Driver;

namespace dotnet_mongodb.Application.User;

public class UserMongoRepository : IUserRepository
{
    private readonly MongoDbContext _db;

    public UserMongoRepository(MongoDbContext db)
    {
        _db = db;
    }

    public void Create(UserEntity entity)
    {
        _db.Users.InsertOne(entity);
    }

    public UserEntity? GetByEmail(string email)
    {
        return _db.Users.Find(x => x.Email == email).FirstOrDefault();
    }
}
