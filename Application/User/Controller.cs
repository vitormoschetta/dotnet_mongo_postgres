using dotnet_mongodb.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_mongodb.Application.User;

[ApiController]
[Route("v1/auth")]
public class AuthController : ControllerBase
{
    private readonly UserService _service;

    public AuthController(UserService service)
    {
        _service = service;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Authenticate(AuthInput input)
    {
        var output = await _service.Authenticate(input);
        var res = ResponseHttp.Build(output, HttpMethod.Post);
        return StatusCode((int)res.Code, res);
    }
}
