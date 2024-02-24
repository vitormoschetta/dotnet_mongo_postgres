using dotnet_mongodb.Data;
using dotnet_mongodb.Application.Shared;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;
using dotnet_mongodb.Application.User;

namespace dotnet_mongodb.Application.Expense;

[ApiController]
[Authorize]
[Route("v1/credit-card/{creditCardId}/expense")]
public class ExpenseController : ControllerBase
{
    private readonly ExpenseService _service;
    private readonly MongoDbContext _db;

    public ExpenseController(ExpenseService service, MongoDbContext db)
    {
        _service = service;
        _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ExpenseEntity>> Get([FromRoute] Guid creditCardId, [FromQuery] string? tag)
    {
        var expenses = _db.Expenses.Find(x => x.CreditCardId == creditCardId).ToList();
        if (!string.IsNullOrEmpty(tag))
        {
            expenses = expenses.Where(x => x.Tags.Contains(tag)).ToList();
        }
        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public ActionResult<ExpenseEntity> Get([FromRoute] string id)
    {
        if (Guid.TryParse(id, out Guid guidID) && guidID != Guid.Empty)
        {
            return _db.Expenses.Find(x => x.Id == guidID).FirstOrDefault();
        }
        return NotFound();
    }

    [HttpPost]
    public ActionResult<Output> Post([FromRoute] Guid creditCardId, [FromBody] ExpenseCreateInput input)
    {
        var user = HttpContext.Items[AttributeKeys.User] as UserEntity ?? throw new UnauthorizedAccessException("Usuário não encontrado");
        var output = _service.Execute(input, creditCardId, user.Email);
        var res = ResponseHttp.Build(output, HttpMethod.Post);
        return StatusCode((int)res.Code, res);
    }
}
