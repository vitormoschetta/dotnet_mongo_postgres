using dotnet_mongodb.Data.Postgres;

namespace dotnet_mongodb.Application.Tag;

public class TagPostgresRepository : ITagRepository
{
    private readonly PostgresDbContext _db;

    public TagPostgresRepository(PostgresDbContext db)
    {
        _db = db;
    }

    public void Create(TagEntity entity)
    {
        _db.Tags.Add(entity);
        _db.SaveChanges();
    }

    public IEnumerable<TagEntity> GetByUserEmail(string email)
    {
        return _db.Tags.Where(x => x.UserEmail == email).ToList();
    }

    public TagEntity? GetByUserEmailAndTitle(string email, string title)
    {
        return _db.Tags.FirstOrDefault(x => x.UserEmail == email && x.Title == title);
    }
}
