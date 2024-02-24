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

    // public Output Execute(CreditCardDeleteInput input, UserEntity user)
    // {
    //     if (input.IsInvalid)
    //         return Output.Fail(EDomainCode.InvalidInput, input.Errors);

    //     var creditCard = _db.GetById(input.Id);

    //     if (creditCard == null)
    //         return Output.Fail(EDomainCode.NotFound, "Cartão de crédito não encontrado");

    //     if (creditCard.UserEmail != user.Email)
    //         return Output.Fail(EDomainCode.Unauthorized, "Você não tem permissão para excluir esse cartão de crédito");

        

    //     _db.Delete(input.Id);

    //     return Output.Ok();
    // }
}
