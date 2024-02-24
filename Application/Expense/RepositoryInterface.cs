namespace dotnet_mongodb.Application.Expense;

public interface IExpenseRepository
{
    void Create(ExpenseEntity entity);
    IEnumerable<ExpenseEntity> GetByCreditCardId(Guid creditCardId);
    ExpenseEntity? GetById(Guid id);
    ExpenseEntity? GetByCreditCardIdAndTitle(Guid creditCardId, string title);
}
