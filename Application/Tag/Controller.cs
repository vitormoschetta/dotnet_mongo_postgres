using dotnet_mongodb.Application.Shared;
using dotnet_mongodb.Application.User;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_mongodb.Application.Tag;

[ApiController]
[Authorize]
[Route("v1/tags")]
public class TagController : ControllerBase
{    
    private readonly ITagRepository _repository;

    public TagController(ITagRepository repository)
    {        
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        var user = HttpContext.Items[AttributeKeys.User] as UserEntity ?? throw new UnauthorizedAccessException("Usuário não encontrado");
        var tags = _repository.GetByUserEmail(user.Email).Select(x => x.Title).ToList();
        return Ok(tags);
    }
}
