namespace dotnet_mongodb.Application.Shared;

public class AppSettings
{
    public MongoDbSettings MongoDb { get; set; } = new();
    public JwtConfig JwtConfig { get; set; } = new();
}

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CollectionName { get; set; } = null!;
}

public class JwtConfig
{
    public string Secret { get; set; } = null!;
    public int Expiration { get; set; }
}