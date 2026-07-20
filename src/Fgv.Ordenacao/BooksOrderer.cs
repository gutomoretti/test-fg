namespace Fgv.Ordenacao;

/// <summary>
/// Define o serviço responsável por ordenar livros.
/// </summary>
public interface BooksOrderer
{
    /// <summary>
    /// Ordena os livros conforme os critérios configurados.
    /// </summary>
    IReadOnlyList<Book> Order(IEnumerable<Book> books);
}
