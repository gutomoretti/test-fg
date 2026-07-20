namespace Fgv.Ordenacao;

/// <summary>
/// Representa um livro que pode ser ordenado pelo serviço.
/// </summary>
public sealed class Book
{
    /// <summary>
    /// Cria um livro.
    /// </summary>
    public Book(string title, string authorName, int editionYear)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        AuthorName = authorName ?? throw new ArgumentNullException(nameof(authorName));
        EditionYear = editionYear;
    }

    /// <summary>
    /// Título do livro.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Nome do autor do livro.
    /// </summary>
    public string AuthorName { get; }

    /// <summary>
    /// Ano da edição do livro.
    /// </summary>
    public int EditionYear { get; }
}
