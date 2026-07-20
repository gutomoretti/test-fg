# Serviço de ordenação de livros

Implementação da avaliação técnica da FGV para um serviço de ordenação configurável.

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
- `tests/Fgv.Ordenacao.Tests`: testes automatizados.

## Comandos previstos

```text
dotnet build --configuration Release
dotnet test --configuration Release
```
