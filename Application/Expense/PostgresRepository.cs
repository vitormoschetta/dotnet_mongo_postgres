using dotnet_mongodb.Data.Postgres;

namespace dotnet_mongodb.Application.Expense;

public class ExpensePostgresRepository : IExpenseRepository
{
    private readonly PostgresDbContext _db;

    public ExpensePostgresRepository(PostgresDbContext db)
    {
        _db = db;
    }

    public void Create(ExpenseEntity entity)
    {
        _db.Expenses.Add(entity);
        _db.SaveChanges();
    }

    public IEnumerable<ExpenseEntity> GetByCreditCardId(Guid creditCardId)
    {
        return _db.Expenses.Where(x => x.CreditCardId == creditCardId).ToList();
    }

    public ExpenseEntity? GetById(Guid id)
    {
        return _db.Expenses.FirstOrDefault(x => x.Id == id);
    }

    public ExpenseEntity? GetByCreditCardIdAndTitle(Guid creditCardId, string title)
    {
        return _db.Expenses.FirstOrDefault(x => x.CreditCardId == creditCardId && x.Title == title);
    }
}
