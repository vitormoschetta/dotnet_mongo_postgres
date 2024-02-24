using dotnet_mongodb.Application.Shared;
using dotnet_mongodb.Application.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_mongodb.Application.CreditCard;

[ApiController]
[Authorize]
[Route("v1/credit-card")]
public class CreditCardController : ControllerBase
{
    private readonly CreditCardService _service;
    private readonly ICreditCardRepository _repository;

    public CreditCardController(CreditCardService service, ICreditCardRepository repository)
    {
        _service = service;
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CreditCardEntity>> Get()
    {
        var user = HttpContext.Items[AttributeKeys.User] as UserEntity ?? throw new UnauthorizedAccessException("Usuário não encontrado");
        var creditCards = _repository.GetByUserEmail(user.Email);
        return Ok(creditCards);
    }

    [HttpGet("{id}")]
    public ActionResult<CreditCardEntity> Get([FromRoute] string id)
    {
        if (Guid.TryParse(id, out Guid guidID) && guidID != Guid.Empty)
        {
            return _repository.GetById(guidID);
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

    // [HttpDelete("{id}")]
    // public ActionResult<Output> Delete([FromRoute] string id)
    // {
    //     if (Guid.TryParse(id, out Guid guidID) && guidID != Guid.Empty)
    //     {
    //         var user = HttpContext.Items[AttributeKeys.User] as UserEntity ?? throw new UnauthorizedAccessException("Usuário não encontrado");
    //         var input = new CreditCardDeleteInput
    //         {
    //             Id = guidID
    //         };
    //         var output = _service.Execute(input, user);
    //         var res = ResponseHttp.Build(output, HttpMethod.Delete);
    //         return StatusCode((int)res.Code, res);
    //     }
    //     return NotFound();
    // }
}
