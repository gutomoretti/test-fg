using Fgv.Ordenacao;

namespace Fgv.Ordenacao.Tests;

public sealed class OrderConfigurationTests
{
    [Fact]
    public void DeveCarregarCriteriosDeArquivoJson()
    {
        var filePath = Path.GetTempFileName();

        try
        {
            File.WriteAllText(filePath, """
                {
                  "criteria": [
                    { "attribute": "EditionYear", "direction": "Descending" },
                    { "attribute": "Title", "direction": "Ascending" }
                  ]
                }
                """);

            var configuration = OrderConfiguration.FromJsonFile(filePath);

            Assert.Collection(
                configuration.Criteria,
                first =>
                {
                    Assert.Equal("EditionYear", first.Attribute);
                    Assert.Equal(SortDirection.Descending, first.Direction);
                },
                second =>
                {
                    Assert.Equal("Title", second.Attribute);
                    Assert.Equal(SortDirection.Ascending, second.Direction);
                });
        }
        finally
        {
            File.Delete(filePath);
        }
    }
}
