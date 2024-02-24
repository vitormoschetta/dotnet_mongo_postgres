using dotnet_mongodb.Application.Shared;
using dotnet_mongodb.Application.User;

namespace dotnet_mongodb.Application.CreditCard;

public class CreditCardService
{
    private readonly ICreditCardRepository _db;

    public CreditCardService(ICreditCardRepository db)
    {
        _db = db;
    }

    public Output Execute(CreditCardCreateInput input, UserEntity user)
    {
        if (input.IsInvalid)
            return Output.Fail(EDomainCode.InvalidInput, input.Errors);

        var entity = input.ToEntity(user.Email);

        var creditCard = _db.GetByUserEmailAndTitle(user.Email, input.Title);

        if (creditCard != null)
            return Output.Fail(EDomainCode.AlreadyExists, "Já existe um cartão de crédito com esse nome");

        _db.Create(entity);

        return Output.Ok(entity);
    }
}
