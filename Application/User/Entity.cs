using System.Text.Json.Serialization;
using dotnet_mongodb.Application.CreditCard;
using dotnet_mongodb.Application.Shared;
using dotnet_mongodb.Application.Tag;

namespace dotnet_mongodb.Application.User;

public class UserEntity : BaseEntity
{
    public string Email { get; set; } = string.Empty;

    [JsonIgnore]
    public List<CreditCardEntity> CreditCards { get; set; } = new List<CreditCardEntity>();

    [JsonIgnore]
    public List<TagEntity> Tags { get; set; } = new List<TagEntity>();

}