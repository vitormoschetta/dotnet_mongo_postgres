using System.Text.Json.Serialization;
using dotnet_mongodb.Application.Expense;
using dotnet_mongodb.Application.Shared;
using dotnet_mongodb.Application.User;

namespace dotnet_mongodb.Application.CreditCard;

public class CreditCardEntity : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    [JsonIgnore]
    public UserEntity User { get; set; } = null!;
    public string UserEmail { get; set; } = string.Empty;

    [JsonIgnore]
    public List<ExpenseEntity> Expenses { get; set; } = new List<ExpenseEntity>();
}
