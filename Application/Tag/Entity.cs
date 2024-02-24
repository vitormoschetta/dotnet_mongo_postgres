using dotnet_mongodb.Application.Shared;

namespace dotnet_mongodb.Application.Tag;

public class TagEntity : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
}
