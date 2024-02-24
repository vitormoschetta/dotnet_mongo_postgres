namespace dotnet_mongodb.Application.Shared;

public class Output
{
    public Output()
    {
        Code = EDomainCode.Success;
        Errors = new List<string>();
    }

    public Output(EDomainCode code, IEnumerable<string> errors)
    {
        Code = code;
        Errors = errors;
    }

    public Output(EDomainCode code, string error)
    {
        Code = code;
        Errors = new List<string> { error };
    }

    /// <summary>
    /// Lista de erros de validação do comando.
    /// </summary>
    public IEnumerable<string> Errors { get; set; }

    /// <summary>
    /// Retorna true se não houver erros de validação.
    /// </summary>
    public EDomainCode Code { get; set; } = EDomainCode.Success;

    /// <summary>
    /// Dados retornados pela operação.
    /// </summary>
    public object Data { get; set; } = null!;

    public static Output Ok() => new();

    public static Output Ok(object data) => new() { Data = data };

    public static Output Fail(EDomainCode code, IEnumerable<string> errors, object data = null) => new(code, errors) { Data = data };

    public static Output Fail(EDomainCode code, string error, object data = null) => new(code, error) { Data = data };
}
