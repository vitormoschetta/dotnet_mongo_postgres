using FluentValidation;

namespace dotnet_mongodb.Application.Shared;

/// <summary>
/// Input generico de deleção
/// </summary>
public class DeleteBaseInput : BaseInput
{
    public Guid Id { get; set; }

    public override bool IsInvalid
    {
        get
        {
            ValidationResult = new DeleteInputValidator().Validate(this);
            return !ValidationResult.IsValid;
        }
    }
}

/// <summary>
/// Validação de input de deleção genérico
/// </summary>
public class DeleteInputValidator : AbstractValidator<DeleteBaseInput>
{
    public DeleteInputValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("O Id não pode ser vazio");
    }
}
