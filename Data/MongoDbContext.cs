using dotnet_mongodb.Application.CreditCard;
using dotnet_mongodb.Application.Expense;
using dotnet_mongodb.Application.Shared;
using dotnet_mongodb.Application.Tag;
using dotnet_mongodb.Application.User;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace dotnet_mongodb.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly string UserCollectionName = "users";
        private readonly string CreditCardCollectionName = "credit_cards";
        private readonly string ExpenseCollectionName = "expenses";
        private readonly string TagCollectionName = "tags";

        public MongoDbContext(IOptions<AppSettings> appSettings)
        {
            var client = new MongoClient(appSettings.Value.MongoDb.ConnectionString);
            _database = client.GetDatabase(appSettings.Value.MongoDb.DatabaseName);
        }
        
        public IMongoCollection<UserEntity> Users => _database.GetCollection<UserEntity>(UserCollectionName);
        public IMongoCollection<CreditCardEntity> CreditCards => _database.GetCollection<CreditCardEntity>(CreditCardCollectionName);
        public IMongoCollection<ExpenseEntity> Expenses => _database.GetCollection<ExpenseEntity>(ExpenseCollectionName);
        public IMongoCollection<TagEntity> Tags => _database.GetCollection<TagEntity>(TagCollectionName);
    }
}