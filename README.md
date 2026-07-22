# Serviço de ordenação de livros

Implementação da avaliação técnica para um serviço de ordenação configurável.

## Decisões iniciais

- A solução será desenvolvida em .NET 8, uma versão de produção e com suporte de longo prazo.
- A interface pública será pequena e independente da forma como a ordenação é implementada.
- Os critérios de ordenação serão definidos por configuração externa, mantendo a ordem e a direção de cada atributo.
- A implementação utilizará comparadores e coleções padrão do .NET, evitando dependências externas desnecessárias.
- Entradas nulas e coleções vazias serão consideradas inválidas e resultarão em `OrdenacaoException`.
- A comparação de textos será ordinal, ignorando diferenças de maiúsculas e minúsculas, para produzir resultados previsíveis.
- O projeto será dividido entre a biblioteca principal e um projeto independente de testes automatizados.

## Estrutura

- `src/Fgv.Ordenacao`: biblioteca principal do serviço.
- `src/Fgv.Ordenacao.Api`: API HTTP do serviço de ordenação.
- `tests/Fgv.Ordenacao.Tests`: testes automatizados.
- `config/ordering.json`: critérios de ordenação, alteráveis sem recompilar o código.
- `docs`: documentação técnica e diagrama UML exigidos pela avaliação.

## Comandos previstos

```text
dotnet build --configuration Release
dotnet test --configuration Release
dotnet run --project src/Fgv.Ordenacao.Api --configuration Release
dotnet publish src/Fgv.Ordenacao.Api/Fgv.Ordenacao.Api.csproj --configuration Release --output artifacts/publish/api
```

## Endpoint

`POST /books/order` recebe um conjunto de livros em JSON e retorna o conjunto ordenado conforme `config/ordering.json`.

Exemplo de requisição:

```json
{
  "books": [
    {
      "title": "Java How to Program",
      "authorName": "Deitel & Deitel",
      "editionYear": 2007
    }
  ]
}
```

Em ambiente de desenvolvimento, a documentação interativa está disponível em `/swagger`.

Entradas nulas, listas vazias e configurações inválidas retornam `400 Bad Request` na API. Na biblioteca, essas situações são representadas por `OrdenacaoException`.

## Validação

A suíte contém testes unitários da regra de ordenação e testes de integração do endpoint HTTP. A execução atual possui 10 testes aprovados.

## Entrega

Os arquivos técnicos estão em `docs`:

- `documentacao-tecnica.docx`: descrição da arquitetura e das decisões.
- `uml-ordenacao-livros.png`: diagrama de classes UML.

O projeto usa .NET 8 e deve ser executado com o SDK/runtime correspondente instalado. O comando `dotnet publish` gera o pacote da API para execução fora da IDE.
