# Documentação técnica

## 1. Objetivo

A solução implementa um serviço .NET para ordenar livros por um ou mais atributos, com direção ascendente ou descendente definida por configuração externa.

## 2. Arquitetura

A solução é organizada em três projetos:

- `Fgv.Ordenacao`: biblioteca com o domínio, o contrato público e a regra de ordenação.
- `Fgv.Ordenacao.Api`: host ASP.NET Core que expõe o serviço por HTTP.
- `Fgv.Ordenacao.Tests`: testes unitários e testes de integração da API.

A API recebe livros em JSON e delega a ordenação para `BooksOrderer`. A implementação concreta é criada com a configuração de `ordering.json`, sem alterar código para trocar os critérios.

## 3. Decisões de projeto

### Contrato público pequeno

`BooksOrderer` expõe somente `Order(IEnumerable<Book>)`. A interface não conhece HTTP, JSON, Swagger ou a forma como os critérios são armazenados. Isso mantém a biblioteca reutilizável e reduz o acoplamento.

### Ordenação composta

`ConfiguredBooksOrderer` cria um comparador para cada critério configurado e os avalia na ordem declarada. Quando um critério encontra diferença, seu resultado é retornado; caso contrário, o próximo critério é avaliado. A direção descendente é aplicada invertendo os argumentos da comparação.

### Configuração externa

O arquivo `ordering.json` possui uma lista ordenada de critérios. Os atributos válidos são `Title`, `AuthorName` e `EditionYear`. A configuração é lida por `OrderConfiguration.FromJsonFile`.

### Validação

Entrada nula, coleção vazia, configuração sem critérios e atributo desconhecido são tratados como erros de domínio por meio de `OrdenacaoException`. Na API, esses erros são convertidos em `400 Bad Request`.

### Uso de recursos padrão

A implementação usa LINQ, `Comparer<T>`, `StringComparer.OrdinalIgnoreCase` e `System.Text.Json`, evitando dependências externas na biblioteca principal. A única dependência adicional é `Microsoft.AspNetCore.Mvc.Testing`, usada exclusivamente nos testes de integração.

## 4. API

Endpoint:

```http
POST /books/order
Content-Type: application/json
```

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

## 5. Testes

Os testes cobrem os três critérios de ordenação fornecidos pela avaliação, entradas nulas e vazias, atributos inválidos, leitura do JSON e o fluxo HTTP completo. Para executar:

```text
dotnet test Fgv.Ordenacao.sln --configuration Release
```

## 6. Execução

```text
dotnet run --project src/Fgv.Ordenacao.Api --configuration Release
```

O diagrama de classes está em `docs/uml-ordenacao-livros.png`.
