# .NET with MongoDB

## Getting Started

### Prerequisites

- .NET 8.0
- Docker

### Running the Application

1. Clone the repository
2. Run the following command to start the MongoDB container:
```bash
docker-compose up -d mongodb
docker-compose up -d postgres
```
3. Run the following command to start the application:
```bash
dotnet run
```
4. Open the following URL in your browser:
```bash
http://localhost:5000/swagger/index.html
```


## MongoDB 

Sample queries path: `Data/Samples`


## Postgres

Run Migration
```bash
dotnet ef migrations add initial -s dotnet_mongodb.csproj -o ./Data/Postgres/Migrations
dotnet ef database update -s dotnet_mongodb.csproj
```


## Performance Tests

Containers Up
```bash
docker-compose up -d dotnet-mongo
docker-compose up -d dotnet-postgres
```

Install Jq
```bash
brew install jq
```

Install Bombardier
```bash
brew install bombardier
```

Run Tests
```bash
sh ./Tests/Performance/creditcard-get-mongo-test.sh
sh ./Tests/Performance/creditcard-get-postgres-test.sh
```

