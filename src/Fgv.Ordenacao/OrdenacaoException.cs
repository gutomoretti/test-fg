namespace Fgv.Ordenacao;

/// <summary>
/// Representa uma falha relacionada à solicitação de ordenação.
/// </summary>
public sealed class OrdenacaoException : Exception
{
    /// <summary>
    /// Cria uma exceção de ordenação com a mensagem informada.
    /// </summary>
    public OrdenacaoException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Cria uma exceção de ordenação com causa original.
    /// </summary>
    public OrdenacaoException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
