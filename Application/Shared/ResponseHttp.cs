using System.Net;
using System.Text.Json.Serialization;

namespace dotnet_mongodb.Application.Shared;

public class ResponseHttp
{
    public IEnumerable<string> Errors { get; set; } = null!;

    public object Data { get; set; } = null!;

    [JsonIgnore]
    public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;

    public static ResponseHttp Build(Output output, HttpMethod verb)
    {
        switch (output.Code)
        {
            case EDomainCode.Success:
                if (verb == HttpMethod.Get)
                {
                    return new ResponseHttp
                    {
                        Data = output.Data,
                        Code = HttpStatusCode.OK
                    };
                }
                if (verb == HttpMethod.Post)
                {
                    return new ResponseHttp
                    {
                        Data = output.Data,
                        Code = HttpStatusCode.Created
                    };
                }
                if (verb == HttpMethod.Put)
                {
                    return new ResponseHttp
                    {
                        Data = output.Data,
                        Code = HttpStatusCode.OK
                    };
                }
                if (verb == HttpMethod.Delete)
                {
                    return new ResponseHttp
                    {
                        Data = output.Data,
                        Code = HttpStatusCode.NoContent
                    };
                }
                return new ResponseHttp
                {
                    Errors = output.Errors,
                    Code = HttpStatusCode.InternalServerError
                };
            case EDomainCode.NotFound:
                return new ResponseHttp
                {
                    Errors = output.Errors,
                    Code = HttpStatusCode.NotFound
                };
            case EDomainCode.InvalidInput:
                return new ResponseHttp
                {
                    Errors = output.Errors,
                    Code = HttpStatusCode.BadRequest
                };
            case EDomainCode.AlreadyExists:
                return new ResponseHttp
                {
                    Errors = output.Errors,
                    Code = HttpStatusCode.Conflict
                };
            case EDomainCode.InternalError:
                return new ResponseHttp
                {
                    Errors = output.Errors,
                    Code = HttpStatusCode.InternalServerError
                };
            default:
                return new ResponseHttp
                {
                    Errors = output.Errors,
                    Code = HttpStatusCode.InternalServerError
                };
        }
    }

}