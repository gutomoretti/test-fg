namespace Fgv.Ordenacao;

/// <summary>
/// Implementa a ordenação de livros com base em critérios configurados.
/// </summary>
public sealed class ConfiguredBooksOrderer : BooksOrderer
{
    private readonly IReadOnlyList<Func<Book, Book, int>> comparers;

    /// <summary>
    /// Cria o serviço com a configuração de critérios informada.
    /// </summary>
    public ConfiguredBooksOrderer(OrderConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        if (configuration.Criteria.Count == 0)
        {
            throw new OrdenacaoException("A ordenação deve possuir pelo menos um critério.");
        }

        comparers = configuration.Criteria.Select(CreateComparer).ToArray();
    }

    /// <inheritdoc />
    public IReadOnlyList<Book> Order(IEnumerable<Book> books)
    {
        if (books is null)
        {
            throw new OrdenacaoException("O conjunto de livros não pode ser nulo.");
        }

        var materializedBooks = books.ToList();
        if (materializedBooks.Count == 0)
        {
            throw new OrdenacaoException("O conjunto de livros não pode ser vazio.");
        }

        return materializedBooks
            .Order(Comparer<Book>.Create(CompareBooks))
            .ToArray();
    }

    private int CompareBooks(Book first, Book second)
    {
        foreach (var comparer in comparers)
        {
            var result = comparer(first, second);
            if (result != 0)
            {
                return result;
            }
        }

        return 0;
    }

    private static Func<Book, Book, int> CreateComparer(OrderCriterion criterion)
    {
        ArgumentNullException.ThrowIfNull(criterion);

        Func<Book, Book, int> comparer = criterion.Attribute.Trim().ToLowerInvariant() switch
        {
            "title" => (first, second) => StringComparer.OrdinalIgnoreCase.Compare(first.Title, second.Title),
            "authorname" => (first, second) => StringComparer.OrdinalIgnoreCase.Compare(first.AuthorName, second.AuthorName),
            "editionyear" => (first, second) => first.EditionYear.CompareTo(second.EditionYear),
            _ => throw new OrdenacaoException($"Atributo de ordenação inválido: '{criterion.Attribute}'.")
        };

        return criterion.Direction == SortDirection.Descending
            ? (first, second) => comparer(second, first)
            : comparer;
    }
}
