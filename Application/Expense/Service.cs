using dotnet_mongodb.Data;
using dotnet_mongodb.Application.Shared;
using MongoDB.Driver;
using dotnet_mongodb.Application.Tag;

namespace dotnet_mongodb.Application.Expense;

public class ExpenseService
{
    private readonly MongoDbContext _db;
    private readonly TagService _tagService;

    public ExpenseService(MongoDbContext db, TagService tagService)
    {
        _db = db;
        _tagService = tagService;
    }

    public Output Execute(ExpenseCreateInput input, Guid creditCardId, string userEmail)
    {
        if (input.IsInvalid)
            return Output.Fail(EDomainCode.InvalidInput, input.Errors);
        
        var creditCard = _db.CreditCards.Find(x => x.Id == creditCardId).FirstOrDefault();

        if (creditCard == null)
            return Output.Fail(EDomainCode.NotFound, "Cartão de crédito não encontrado");

        var entity = input.ToEntity(creditCard);

        var expense = _db.Expenses.Find(x => x.CreditCardId == creditCardId && x.Title == entity.Title).FirstOrDefault();

        if (expense != null)
            return Output.Fail(EDomainCode.AlreadyExists, "Já existe uma despesa com esse nome");

        _db.Expenses.InsertOne(entity);

        if (entity.Tags.Count > 0)
        {
            foreach (var tag in entity.Tags)
            {
                _tagService.Execute(new TagCreateInput { Title = tag }, userEmail);
            }
        }

        return Output.Ok(entity);
    }
}
