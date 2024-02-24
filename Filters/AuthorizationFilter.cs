using dotnet_mongodb.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Driver;

namespace dotnet_mongodb.Filters;

public class AuthorizationFilter : IAsyncAuthorizationFilter
{
    private readonly MongoDbContext _db;
    private readonly ILogger<AuthorizationFilter> _logger;

    public AuthorizationFilter(MongoDbContext db, ILogger<AuthorizationFilter> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        // Verifica se a rota é pública (AllowAnonymous). Neste caso, não precisamos enriquecer o contexto.
        var endpoint = context.HttpContext.GetEndpoint();
        var allowAnonymousAttributes = endpoint?.Metadata.GetOrderedMetadata<AllowAnonymousAttribute>() ?? Array.Empty<AllowAnonymousAttribute>();
        if (allowAnonymousAttributes.Any())
        {
            await Task.CompletedTask;
            return;
        }

        var userEmail = context.HttpContext.User?.Identity?.Name ?? throw new UnauthorizedAccessException("Usuário não autenticado");
        var user = await _db.Users.Find(x => x.Email == userEmail).FirstOrDefaultAsync();
        if (user == null)
        {
            _logger.LogError("Usuário autenticado porém não encontrado no banco de dados. Email: {userEmail}", userEmail);
            throw new Exception("Usuário não encontrado");
        }

        context.HttpContext.Items.Add(AttributeKeys.User, user);
        await Task.CompletedTask;
    }
}