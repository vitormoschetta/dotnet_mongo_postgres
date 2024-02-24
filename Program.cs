using System.Text;
using dotnet_mongodb.Application.CreditCard;
using dotnet_mongodb.Application.Expense;
using dotnet_mongodb.Application.Shared;
using dotnet_mongodb.Application.Tag;
using dotnet_mongodb.Application.User;
using dotnet_mongodb.Data.Postgres;
using dotnet_mongodb.Filters;
using dotnet_mongodb.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(options =>
    {
        options.Filters.Add<AuthorizationFilter>();
    });

builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CreditCardService>();
builder.Services.AddScoped<ExpenseService>();
builder.Services.AddScoped<TagService>();

// eleger entre mongo o postgres
// builder.Services.AddScoped<IUserRepository, UserPostgresRepository>();
// builder.Services.AddScoped<ICreditCardRepository, CreditCardPostgresRepository>();
// builder.Services.AddScoped<IExpenseRepository, ExpensePostgresRepository>();
// builder.Services.AddScoped<ITagRepository, TagPostgresRepository>();
builder.Services.AddScoped<IUserRepository, UserMongoRepository>();
builder.Services.AddScoped<ICreditCardRepository, CreditCardMongoRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseMongoRepository>();
builder.Services.AddScoped<ITagRepository, TagMongoRepository>();


builder.Services.AddScoped<Jwt>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new() { Title = "Pfm.Api", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"
                Bearer {token}",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

                },
                new List<string>()
            }
        });
});

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtKey = builder?.Configuration["JwtConfig:Secret"] ?? throw new Exception("JwtConfig:Secret not found");
        var key = Encoding.ASCII.GetBytes(jwtKey);

        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddDbContext<PostgresDbContext>(options =>
    options.UseNpgsql(builder.Configuration["PostgresDb:ConnectionString"] ?? throw new Exception("PostgresDb:ConnectionString not found")));

var app = builder.Build();

// Apply Postgres migrations
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<PostgresDbContext>();
    if (dataContext.Database.IsRelational() && dataContext.Database.GetPendingMigrations().Any())
        dataContext.Database.Migrate();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();