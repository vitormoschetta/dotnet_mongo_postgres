using dotnet_mongodb.Application.Shared;
using FluentValidation;

namespace dotnet_mongodb.Application.Tag;

public class TagCreateInput : BaseInput
{
    public string Title { get; set; } = string.Empty;

    public override bool IsInvalid
    {
        get
        {
            ValidationResult = new TagCreateInputValidator().Validate(this);
            return !ValidationResult.IsValid;
        }
    }

    public TagEntity ToEntity(string userEmail)
    {
        return new TagEntity
        {
            Title = Title,
            UserEmail = userEmail
        };
    }
}

public class TagCreateInputValidator : AbstractValidator<TagCreateInput>
{
    public TagCreateInputValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage("O título deve conter pelo menos 3 caracteres");
    }
}