# Coding Challenge

### Service
Este é um serviço dotnet que irá fazer todas as operações CRUD para um banco de Dados

A implementação foi feita para que o Banco de Dados seja LENTO e simples

Há 2 endpoints que não foram implementados ainda e são `POST /api/v1/Content/{id}/genre` and `DELETE /api/v1/Content/{id}/genre`.


## O Desafio
Dê um Fork neste repositório e tente resolver as seguintes Tasks
(NÃO É PERMITIDO MODIFICAR O ARQUIVO `DatabaseLenta.cs` )

## Task 1

Implement os seguintes endpoints:
 * `POST /api/v1/Content/{id}/genre`
    * Este endpoint deverá adicionar novos gêneros. Não se deve permitir generos duplicados.
 * `DELETE /api/v1/Content/{id}/genre`
    * Este endpoint deverá remover gêneros.


## Task 2
Quando o servico está rodando em produção, não há uma maneira de saber o que está ocorrendo.
Implemente algum tipo de Log ao serviço e adicione as informações que forem cabíveis, mas não sobrecarregue a quantidade informações quando em produção;

## Task 3
Sua equipe tem liberdade para escolher uma novo banco de dados.
Faça as alterações necessárias apra se adaptar a essa nova conexão 
A tecnologia utilizada fica a teu critério( Mongodb, Redis, Cassandra, CouchDB, MySQL, etc...).

## Task 4
Enquanto a nova tecnologia não está pronta para produção, o Banco de Dados é lento(a classe dele não pode ser alterada).
Encontre uma maneira de aumentar a velocidade dos endpoints sem modificar o arquivo `DatabaseLenta.cs`

## Task 5
O rojeto não tem Testes unitários, adicione-os para garantir que tudo funciona como esperado.

## Task 6

Queremos Deprecar o endpoint `GET /api/v1/Content` que sempre retorna todos os conteúdos.
Crie um novo endpoint que deprecia o anterior e implemente uma maneira de filtrar os conteudos por Titulo e/ou Genero
(A condição do filtro pode ser algo simples como uma substring ou ´contains´)

