using dotnet_mongodb.Data;
using dotnet_mongodb.Application.Shared;
using dotnet_mongodb.Application.User;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_mongodb.Application.CreditCard;

[ApiController]
[Authorize]
[Route("v1/credit-card")]
public class CreditCardController : ControllerBase
{
    private readonly CreditCardService _service;
    private readonly MongoDbContext _db;

    public CreditCardController(CreditCardService service, MongoDbContext db)
    {
        _service = service;
        _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CreditCardEntity>> Get()
    {
        var user = HttpContext.Items[AttributeKeys.User] as UserEntity ?? throw new UnauthorizedAccessException("Usuário não encontrado");
        var creditCards = _db.CreditCards.Find(x => x.UserEmail == user.Email).ToList();
        return Ok(creditCards);
    }

    [HttpGet("{id}")]
    public ActionResult<CreditCardEntity> Get([FromRoute] string id)
    {
        if (Guid.TryParse(id, out Guid guidID) && guidID != Guid.Empty)
        {
            return _db.CreditCards.Find(x => x.Id == guidID).FirstOrDefault();
        }   
        return NotFound();
    }

    [HttpPost]
    public ActionResult<Output> Post([FromBody] CreditCardCreateInput input)
    {
        var user = HttpContext.Items[AttributeKeys.User] as UserEntity ?? throw new UnauthorizedAccessException("Usuário não encontrado");
        var output = _service.Execute(input, user);
        var res = ResponseHttp.Build(output, HttpMethod.Post);
        return StatusCode((int)res.Code, res);
    }
}
