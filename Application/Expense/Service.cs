using dotnet_mongodb.Application.Shared;
using dotnet_mongodb.Application.Tag;
using dotnet_mongodb.Application.CreditCard;

namespace dotnet_mongodb.Application.Expense;

public class ExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ICreditCardRepository _creditCardRepository;
    private readonly TagService _tagService;

    public ExpenseService(IExpenseRepository expenseRepository, ICreditCardRepository creditCardRepository, TagService tagService)
    {
        _expenseRepository = expenseRepository;
        _creditCardRepository = creditCardRepository;
        _tagService = tagService;
    }

    public Output Execute(ExpenseCreateInput input, Guid creditCardId, string userEmail)
    {
        if (input.IsInvalid)
            return Output.Fail(EDomainCode.InvalidInput, input.Errors);
        
        var creditCard = _creditCardRepository.GetById(creditCardId);

        if (creditCard == null)
            return Output.Fail(EDomainCode.NotFound, "Cartão de crédito não encontrado");

        var entity = input.ToEntity(creditCard);

        var expense = _expenseRepository.GetByCreditCardIdAndTitle(creditCardId, input.Title);

        if (expense != null)
            return Output.Fail(EDomainCode.AlreadyExists, "Já existe uma despesa com esse nome");

        _expenseRepository.Create(entity);

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
