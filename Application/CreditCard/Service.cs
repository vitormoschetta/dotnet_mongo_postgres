using dotnet_mongodb.Data;
using dotnet_mongodb.Application.Shared;
using dotnet_mongodb.Application.User;
using MongoDB.Driver;

namespace dotnet_mongodb.Application.CreditCard;

public class CreditCardService
{
    private readonly MongoDbContext _db;

    public CreditCardService(MongoDbContext db)
    {
        _db = db;
    }

    public Output Execute(CreditCardCreateInput input, UserEntity user)
    {
        if (input.IsInvalid)
            return Output.Fail(EDomainCode.InvalidInput, input.Errors);

        var entity = input.ToEntity(user.Email);

        var creditCard = _db.CreditCards.Find(x => x.UserEmail == user.Email && x.Title == entity.Title).FirstOrDefault();

        if (creditCard != null)
            return Output.Fail(EDomainCode.AlreadyExists, "Já existe um cartão de crédito com esse nome");

       _db.CreditCards.InsertOne(entity);

        return Output.Ok(entity);
    }
}
