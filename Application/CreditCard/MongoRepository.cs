    using MongoDB.Driver;

namespace dotnet_mongodb.Application.CreditCard;

public class CreditCardMongoRepository : ICreditCardRepository
{
    private readonly MongoDbContext _db;

    public CreditCardMongoRepository(MongoDbContext db)
    {
        _db = db;
    }
    
    public void Create(CreditCardEntity creditCard) 
    {
        _db.CreditCards.InsertOne(creditCard);
    }

    public IEnumerable<CreditCardEntity> GetByUserEmail(string userEmail)
    {
        var find = _db.CreditCards.Find(x => x.UserEmail == userEmail);
        return find.ToList();
    }

    public CreditCardEntity? GetById(Guid id)
    {
        var find = _db.CreditCards.Find(x => x.Id == id);
        return find.FirstOrDefault();
    }

    public CreditCardEntity? GetByUserEmailAndTitle(string userEmail, string title)
    {
        var find = _db.CreditCards.Find(x => x.UserEmail == userEmail && x.Title == title);
        return find.FirstOrDefault();
    }
}
