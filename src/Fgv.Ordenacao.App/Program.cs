using Fgv.Ordenacao;

var configurationPath = Path.Combine(AppContext.BaseDirectory, "ordering.json");
var configuration = OrderConfiguration.FromJsonFile(configurationPath);
var orderer = new ConfiguredBooksOrderer(configuration);

var books = new[]
{
    new Book("Java How to Program", "Deitel & Deitel", 2007),
    new Book("Patterns of Enterprise Application Architecture", "Martin Fowler", 2002),
    new Book("Head First Design Patterns", "Elisabeth Freeman", 2004),
    new Book("Internet & World Wide Web: How to Program", "Deitel & Deitel", 2007)
};

foreach (var book in orderer.Order(books))
{
    Console.WriteLine($"{book.Title} - {book.AuthorName} ({book.EditionYear})");
}
