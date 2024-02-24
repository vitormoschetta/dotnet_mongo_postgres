using dotnet_mongodb.Data;
using dotnet_mongodb.Application.Shared;
using dotnet_mongodb.Application.User;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;
using dotnet_mongodb.Application.Expense;

namespace dotnet_mongodb.Application.Tag;

[ApiController]
[Authorize]
[Route("v1/tags")]
public class TagController : ControllerBase
{    
    private readonly MongoDbContext _db;

    public TagController(MongoDbContext db)
    {        
        _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        var user = HttpContext.Items[AttributeKeys.User] as UserEntity ?? throw new UnauthorizedAccessException("Usuário não encontrado");
        var tags = _db.Tags.Find(x => x.UserEmail == user.Email).ToList().Select(x => x.Title).ToList();
        return Ok(tags);
    }
}
