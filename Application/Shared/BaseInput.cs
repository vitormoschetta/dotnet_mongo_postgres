using System.Text.Json.Serialization;
using FluentValidation.Results;

namespace dotnet_mongodb.Application.Shared;

/// <summary>
/// As classes de input devem herdar essa classe base.
/// </summary>
public abstract class BaseInput
{
    [JsonIgnore]
    public ValidationResult? ValidationResult { get; set; }

    [JsonIgnore]
    /// <summary>
    /// Método que deve ser sobrescrito com a lógica de validação do comando.
    /// Exemplo de sobrescrita na classe DeleteCommand (comando de deleção genérico).
    /// </summary>
    public abstract bool IsInvalid
    {
        get;
    }

    [JsonIgnore]
    /// <summary>
    /// Método retorna uma lista de string com os erros de validação do comando (ErrorMessage do FluentValidation).
    /// </summary>
    public IEnumerable<string> Errors
    {
        get
        {
            if (ValidationResult == null) return new List<string>();

            return ValidationResult.Errors.Select(e => e.ErrorMessage);
        }
    }
}
