using dotnet_mongodb.Application.Shared;

namespace dotnet_mongodb.Application.Expense;

public class ExpenseEntity : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public decimal Value { get; set; }
    public List<string> Tags { get; set; } = null!;
    public Guid CreditCardId { get; set; }
}
