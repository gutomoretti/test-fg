using Fgv.Ordenacao;

namespace Fgv.Ordenacao.Tests;

public sealed class ConfiguredBooksOrdererTests
{
    private static readonly Book[] Books =
    [
        new("Java How to Program", "Deitel & Deitel", 2007),
        new("Patterns of Enterprise Application Architecture", "Martin Fowler", 2002),
        new("Head First Design Patterns", "Elisabeth Freeman", 2004),
        new("Internet & World Wide Web: How to Program", "Deitel & Deitel", 2007)
    ];

    [Fact]
    public void DeveOrdenarPorTituloAscendente()
    {
        var orderer = CreateOrderer(("Title", SortDirection.Ascending));

        var result = orderer.Order(Books);

        Assert.Equal(
            ["Head First Design Patterns", "Internet & World Wide Web: How to Program", "Java How to Program", "Patterns of Enterprise Application Architecture"],
            result.Select(book => book.Title));
    }

    [Fact]
    public void DeveOrdenarPorAutorAscendenteETituloDescendente()
    {
        var orderer = CreateOrderer(
            ("AuthorName", SortDirection.Ascending),
            ("Title", SortDirection.Descending));

        var result = orderer.Order(Books);

        Assert.Equal([Books[0], Books[3], Books[2], Books[1]], result);
    }

    [Fact]
    public void DeveOrdenarPorEdicaoAutorETitulo()
    {
        var orderer = CreateOrderer(
            ("EditionYear", SortDirection.Descending),
            ("AuthorName", SortDirection.Descending),
            ("Title", SortDirection.Ascending));

        var result = orderer.Order(Books);

        Assert.Equal([Books[3], Books[0], Books[2], Books[1]], result);
    }

    [Fact]
    public void DeveLancarExcecaoParaConjuntoNulo()
    {
        var orderer = CreateOrderer(("Title", SortDirection.Ascending));

        Assert.Throws<OrdenacaoException>(() => orderer.Order(null!));
    }

    [Fact]
    public void DeveLancarExcecaoParaConjuntoVazio()
    {
        var orderer = CreateOrderer(("Title", SortDirection.Ascending));

        Assert.Throws<OrdenacaoException>(() => orderer.Order([]));
    }

    [Fact]
    public void DeveLancarExcecaoParaAtributoInvalido()
    {
        var configuration = new OrderConfiguration
        {
            Criteria = [new OrderCriterion { Attribute = "Unknown", Direction = SortDirection.Ascending }]
        };

        Assert.Throws<OrdenacaoException>(() => new ConfiguredBooksOrderer(configuration));
    }

    private static ConfiguredBooksOrderer CreateOrderer(params (string Attribute, SortDirection Direction)[] criteria)
    {
        return new ConfiguredBooksOrderer(new OrderConfiguration
        {
            Criteria = criteria
                .Select(criterion => new OrderCriterion
                {
                    Attribute = criterion.Attribute,
                    Direction = criterion.Direction
                })
                .ToList()
        });
    }
}
