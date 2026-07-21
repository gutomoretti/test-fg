using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Fgv.Ordenacao.Tests;

public sealed class BooksOrderApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public BooksOrderApiTests(WebApplicationFactory<Program> factory)
    {
        client = factory.CreateClient();
    }

    [Fact]
    public async Task DeveRetornarLivrosOrdenados()
    {
        var response = await client.PostAsJsonAsync("/books/order", new
        {
            books = new[]
            {
                new { title = "Java How to Program", authorName = "Deitel & Deitel", editionYear = 2007 },
                new { title = "Patterns of Enterprise Application Architecture", authorName = "Martin Fowler", editionYear = 2002 },
                new { title = "Head First Design Patterns", authorName = "Elisabeth Freeman", editionYear = 2004 },
                new { title = "Internet & World Wide Web: How to Program", authorName = "Deitel & Deitel", editionYear = 2007 }
            }
        });

        response.EnsureSuccessStatusCode();
        var books = await response.Content.ReadFromJsonAsync<List<BookResponse>>();

        Assert.NotNull(books);
        Assert.Equal(
            ["Head First Design Patterns", "Internet & World Wide Web: How to Program", "Java How to Program", "Patterns of Enterprise Application Architecture"],
            books!.Select(book => book.Title));
    }

    [Fact]
    public async Task DeveRetornarBadRequestParaLivrosNulos()
    {
        var response = await client.PostAsJsonAsync("/books/order", new { books = (object?)null });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeveRetornarBadRequestParaListaVazia()
    {
        var response = await client.PostAsJsonAsync("/books/order", new { books = Array.Empty<object>() });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    private sealed record BookResponse(string Title, string AuthorName, int EditionYear);
}
