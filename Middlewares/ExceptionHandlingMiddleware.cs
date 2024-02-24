using System.Net;
using dotnet_mongodb.Application.Shared;
using MongoDB.Driver;

namespace dotnet_mongodb.Middlewares
{
    /// <summary>
    /// Classe que implementa um middleware para capturar exceções não tratadas em chamadas HTTP e retornar um JSON com o erro.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {                
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError("An error occurred: {Message}", exception.Message);            

            HttpStatusCode code = exception switch
            {
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                MongoException => HttpStatusCode.InternalServerError,
                _ => HttpStatusCode.InternalServerError,
            };

            var result = JsonManagerSerialize.Serialize(Output.Fail(EDomainCode.InternalError, exception.Message));

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}