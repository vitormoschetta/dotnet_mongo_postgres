using dotnet_mongodb.Data.Postgres;

namespace dotnet_mongodb.Application.CreditCard;

public class CreditCardPostgresRepository : ICreditCardRepository
{
    private readonly PostgresDbContext _db;

    public CreditCardPostgresRepository(PostgresDbContext db)
    {
        _db = db;
    }

    public void Create(CreditCardEntity entity)
    {
        _db.CreditCards.Add(entity);
        _db.SaveChanges();
    }

    public IEnumerable<CreditCardEntity> GetByUserEmail(string userEmail)
    {
        return _db.CreditCards.Where(x => x.UserEmail == userEmail).ToList();
    }

    public CreditCardEntity? GetById(Guid id)
    {
        return _db.CreditCards.Find(id);
    }

    public CreditCardEntity? GetByUserEmailAndTitle(string userEmail, string title)
    {
        return _db.CreditCards.FirstOrDefault(x => x.UserEmail == userEmail && x.Title == title);
    }
}
