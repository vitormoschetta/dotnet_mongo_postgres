namespace dotnet_mongodb.Application.Shared;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}
