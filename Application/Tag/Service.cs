using dotnet_mongodb.Data;
using dotnet_mongodb.Application.Shared;
using dotnet_mongodb.Application.User;
using MongoDB.Driver;

namespace dotnet_mongodb.Application.Tag;

public class TagService
{
    private readonly MongoDbContext _db;

    public TagService(MongoDbContext db)
    {
        _db = db;
    }

    public Output Execute(TagCreateInput input, string userEmail)
    {
        if (input.IsInvalid)
            return Output.Fail(EDomainCode.InvalidInput, input.Errors);

        var entity = input.ToEntity(userEmail);

        var recorded = _db.Tags.Find(x => x.Title == entity.Title && x.UserEmail == entity.UserEmail).FirstOrDefault();

        if (recorded != null)
            return Output.Fail(EDomainCode.AlreadyExists, "Já existe uma tag com esse título");

        _db.Tags.InsertOne(entity);

        return Output.Ok(entity);
    }
}
