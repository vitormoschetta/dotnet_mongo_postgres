using MongoDB.Driver;

namespace dotnet_mongodb.Application.Expense;

public class ExpenseMongoRepository : IExpenseRepository
{
    private readonly MongoDbContext _db;

    public ExpenseMongoRepository(MongoDbContext db)
    {
        _db = db;
    }

    public void Create(ExpenseEntity entity) 
    {
        _db.Expenses.InsertOne(entity);
    }

    public IEnumerable<ExpenseEntity> GetByCreditCardId(Guid creditCardId)
    {
        return _db.Expenses.Find(x => x.CreditCardId == creditCardId).ToList();
    }

    public ExpenseEntity? GetById(Guid id)
    {
        return _db.Expenses.Find(x => x.Id == id).FirstOrDefault();
    }

    public ExpenseEntity? GetByCreditCardIdAndTitle(Guid creditCardId, string title)
    {
        return _db.Expenses.Find(x => x.CreditCardId == creditCardId && x.Title == title).FirstOrDefault();
    }
}
