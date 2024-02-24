using dotnet_mongodb.Application.CreditCard;
using dotnet_mongodb.Application.Shared;
using FluentValidation;

namespace dotnet_mongodb.Application.Expense;

public class ExpenseCreateInput : BaseInput
{
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
    public List<string> Tags { get; set; } = null!;

    public override bool IsInvalid
    {
        get
        {
            ValidationResult = new ExpenseCreateInputValidator().Validate(this);
            return !ValidationResult.IsValid;
        }
    }

    public ExpenseEntity ToEntity(CreditCardEntity creditCard) => new()
    {
        Title = Title,
        Date = Date,
        Value = Value,
        CreditCardId = creditCard.Id,
        Tags = Tags
    };
}

public class ExpenseCreateInputValidator : AbstractValidator<ExpenseCreateInput>
{
    public ExpenseCreateInputValidator()
    {
        // O titulo da despesa não pode ser menor que 3 caracteres e maior que 100
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("O título da despesa não pode ser vazio")
            .MinimumLength(3).WithMessage("O título da despesa deve ter no mínimo 3 caracteres")
            .MaximumLength(100).WithMessage("O título da despesa deve ter no máximo 100 caracteres");

        RuleFor(x => x.Value).GreaterThanOrEqualTo(0)
            .WithMessage("O valor não pode ser negativo");

        RuleFor(x => x.Date).GreaterThanOrEqualTo(DateTime.Now.AddYears(-1))
            .WithMessage("A data não pode ser de mais de um ano atrás");
    }
}