# Desafio C# Easy Level - ConexaLabs 2020

# O desafio

Crie um microsserviço capaz de aceitar solicitações RESTful que recebam como parâmetro o nome da cidade ou as coordenadas (*latitude e longitude*), faça persistência da temperatura da cidade naquele dia em um banco de dados MSSQL e retorne a temperatura atual e se houver, o histórico de temperaturas do último mês.

## Requisitos

1. Ter um endpoint que receba o nome da cidade, faça persistência do resultado e retorne a temperatura atual;
2. Ter um endpoint que receba a latitude e longitude, faça persistência do resultado e retorne a temperatura atual;
3. Tem um endpoint que receba o nome da cidade ou a latitude e longitude, e retorne o histórico de temperaturas para a localidade no último mês;

## Estrutura

O projeto está em uma estrutura DDD (*Domain-Driven Design*) porque é uma estrutura prática, além de facilitar o escalonamento do projeto e a simplificação do acesso ao banco de dados.
Para a manipulação do banco foi utilizado o [EF Core](https://docs.microsoft.com/pt-br/ef/core/) juntamente com o [AutoMapper](https://docs.automapper.org/en/stable/Getting-started.html) para o mapeamento, facilitando o desenvolvimento ao não precisar utilizar consultas e mapeamentos manuais.
O projeto possui IoC (Inversion of Control), foi utilizado o [Autofac](https://autofac.org/) que é um container responsável por facilitar essa inversão.
Por fim, foi utilizado o padrão de design chamado de [injeção de dependência](https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1) contribuindo também com a inversão de controle.

## Inicializando o projeto

### SQL
Primeiramente, para rodar o sql server em docker, será preciso utilizar a imagem `renanfssilva/sql` presente no Docker Hub, o comando para subir o banco é `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Banco123" -p 1433:1433 renanfssilva/sql`. Na imagem está presente uma estrutura simples de banco de dados. Se houver problemas ao utilizar a imagem, a estrutura pode ser montada a partir dos seguintes comandos:

```sql
CREATE DATABASE ClimaAPI
```

Após a criação da base ClimaAPI, rodar os seguintes comandos no contexto da base ClimaAPI:

```sql
CREATE TABLE Cidades (
	CidadeID int NOT NULL,
	Nome varchar(100) NOT NULL,
	Latitude float NULL,
	Longitude float NULL,
	Primary Key (CidadeId)
);

CREATE TABLE Registros (
	RegistroID int IDENTITY(1,1) NOT NULL,
	Temperatura float NULL,
	Data datetime NOT NULL,
	CidadeID int NULL,
	FOREIGN KEY (CidadeID) REFERENCES Cidades(CidadeID)
);

ALTER TABLE Cidades ALTER COLUMN Nome [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AI
```
### API
Para utilizar a API, é possível utilizar o comando `docker-compose up` na pasta que contenha o docker-compose.yml. Após os containers estiverem rodando, é só acessar a url `http://localhost:5000/api/documentation` para ler a documentação gerada pelo Swagger.

O JSON [ClimaAPI.postman_collection.json](https://github.com/renanfssilva/desafio-csharp-easy-level/blob/master/ClimaAPI.postman_collection.json) pode ser utilizado para gerar alguns resultados na API e alimentar o banco.

## Ferramentas utilizadas

* [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/);
* [.NET Core 3.1](https://docs.microsoft.com/pt-br/dotnet/core/introduction);
* [EF Core](https://docs.microsoft.com/pt-br/ef/core/);
* [AutoMapper](https://docs.automapper.org/en/stable/Getting-started.html);
* [Autofac](https://autofac.org/);
* [SQL Server](https://docs.microsoft.com/pt-br/sql/sql-server/?view=sql-server-ver15);
* [Docker](https://www.docker.com/);
* [Swagger](https://swagger.io/);
* [Postman](https://www.postman.com/);
* API [OpenWeatherMaps](https://openweathermap.org)
