using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fgv.Ordenacao;

/// <summary>
/// Representa a configuração externa do serviço de ordenação.
/// </summary>
public sealed class OrderConfiguration
{
    /// <summary>
    /// Critérios aplicados na ordem de prioridade configurada.
    /// </summary>
    public List<OrderCriterion> Criteria { get; init; } = [];

    /// <summary>
    /// Carrega uma configuração a partir de um arquivo JSON.
    /// </summary>
    public static OrderConfiguration FromJsonFile(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        try
        {
            var json = File.ReadAllText(filePath);
            var configuration = JsonSerializer.Deserialize<OrderConfiguration>(json, JsonOptions);

            return configuration ?? throw new OrdenacaoException("A configuração de ordenação está vazia.");
        }
        catch (OrdenacaoException)
        {
            throw;
        }
        catch (Exception exception) when (exception is IOException or JsonException)
        {
            throw new OrdenacaoException("Não foi possível carregar a configuração de ordenação.", exception);
        }
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };
}

/// <summary>
/// Define um atributo e a direção de sua classificação.
/// </summary>
public sealed class OrderCriterion
{
    /// <summary>
    /// Nome do atributo do livro: Title, AuthorName ou EditionYear.
    /// </summary>
    public string Attribute { get; init; } = string.Empty;

    /// <summary>
    /// Direção da classificação.
    /// </summary>
    public SortDirection Direction { get; init; } = SortDirection.Ascending;
}

/// <summary>
/// Direção de um critério de ordenação.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortDirection
{
    Ascending,
    Descending
}
