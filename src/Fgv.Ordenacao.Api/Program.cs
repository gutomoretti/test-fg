using Fgv.Ordenacao;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

var configurationPath = Path.Combine(builder.Environment.ContentRootPath, "ordering.json");
var orderConfiguration = OrderConfiguration.FromJsonFile(configurationPath);
builder.Services.AddSingleton<BooksOrderer>(new ConfiguredBooksOrderer(orderConfiguration));

var app = builder.Build();

app.UseExceptionHandler();

app.MapPost("/books/order", (OrderBooksRequest? request, BooksOrderer orderer) =>
{
    if (request?.Books is null)
    {
        return Results.BadRequest(new { error = "O conjunto de livros não pode ser nulo." });
    }

    try
    {
        var orderedBooks = orderer.Order(request.Books.Select(book => book.ToDomain()));
        return Results.Ok(orderedBooks);
    }
    catch (OrdenacaoException exception)
    {
        return Results.BadRequest(new { error = exception.Message });
    }
})
.WithName("OrderBooks")
.WithOpenApi();

app.Run();

public sealed record OrderBooksRequest(IReadOnlyList<BookRequest>? Books);

public sealed record BookRequest(string Title, string AuthorName, int EditionYear)
{
    public Book ToDomain() => new(Title, AuthorName, EditionYear);
}

public partial class Program;
