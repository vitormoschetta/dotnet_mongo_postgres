namespace dotnet_mongodb.Application.CreditCard;

public interface ICreditCardRepository
{
    void Create(CreditCardEntity creditCard);
    IEnumerable<CreditCardEntity> GetByUserEmail(string userEmail);
    CreditCardEntity? GetById(Guid id);
    CreditCardEntity? GetByUserEmailAndTitle(string userEmail, string title);
    // void Delete(Guid id);
}
