using System.Text.Json;

namespace dotnet_mongodb.Application.Shared;

/// <summary>
/// Classe que implementa métodos para serializar e desserializar objetos em JSON, centralizando a configuração do JsonSerializer.
/// </summary>
public class JsonManagerSerialize
{
    public static string Serialize<T>(T obj)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        return JsonSerializer.Serialize(obj, options);
    }

    public static T? Deserialize<T>(string json)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        return JsonSerializer.Deserialize<T>(json, options);
    }
}
