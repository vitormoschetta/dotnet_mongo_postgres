using MongoDB.Driver;

namespace dotnet_mongodb.Application.Tag;

public class TagMongoRepository : ITagRepository
{
    private readonly MongoDbContext _db;

    public TagMongoRepository(MongoDbContext db)
    {
        _db = db;
    }

    public void Create(TagEntity entity)
    {
        _db.Tags.InsertOne(entity);
    }

    public IEnumerable<TagEntity> GetByUserEmail(string email)
    {
        return _db.Tags.Find(x => x.UserEmail == email).ToList();
    }

    public TagEntity? GetByUserEmailAndTitle(string email, string title)
    {
        return _db.Tags.Find(x => x.UserEmail == email && x.Title == title).FirstOrDefault();
    }
}
