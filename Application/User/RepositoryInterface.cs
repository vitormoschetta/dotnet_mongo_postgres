namespace dotnet_mongodb.Application.User;

public interface IUserRepository
{
    void Create(UserEntity entity);
    UserEntity? GetByEmail(string email);
}
