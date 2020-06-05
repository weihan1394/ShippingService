# Markono Shipping Service
Markono Shipping Service Rate

## Source code contains

1. [Autofac](https://autofac.org/)
1. [Swagger](https://swagger.io/) + [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle)
1. [EF Core](https://docs.microsoft.com/ef/)
    * [Npgsql](https://github.com/npgsql/npgsql)
1. Tests
    * Integration tests with InMemory database
        * [FluentAssertions]
        * [xUnit]
    * Unit tests
        * [AutoFixture](https://github.com/AutoFixture/AutoFixture)
        * [FluentAssertions]
        * [Moq](https://github.com/moq/moq4)
        * [Moq.AutoMock](https://github.com/moq/Moq.AutoMocker)
        * [xUnit]
    * Load tests
        * [FluentAssertions]
        * [NBomber](https://nbomber.com/)
        * [xUnit]
1. Code quality
    * [EditorConfig](https://editorconfig.org/) ([.editorconfig](.editorconfig))
    * Analizers ([Microsoft.CodeAnalysis.Analyzers](https://github.com/dotnet/roslyn-analyzers), [Microsoft.AspNetCore.Mvc.Api.Analyzers](https://github.com/aspnet/AspNetCore/tree/master/src/Analyzers))
    * [Rules](ShippingService.ruleset)
    * Code coverage
        * [Coverlet](https://github.com/tonerdo/coverlet)
        * [Codecov](https://codecov.io/)
1. Docker
    * [Dockerfile](dockerfile)
    * [Docker-compose](docker-compose.yml)
        * `postgres:12.2` 
        * `netcore-boilerplate:local`
1. [Serilog](https://serilog.net/)
    * Sink: [Async](https://github.com/serilog/serilog-sinks-async)
    * Enrich: [CorrelationId](https://github.com/ekmsystems/serilog-enrichers-correlation-id)
1. [DbUp](http://dbup.github.io/) as a db migration tool

## DB Migrations

[ShippingService.Db](src/ShippingService.Db)

* Console application as a simple db migration tool - [Program.cs](src/ShippingService.Db/Program.cs)
* Sample migration scripts, both `.sql` and `.cs` - [S001_AddCarTable.sql](src/ShippingService.Db/Scripts/Sql/S001_AddCarTable.sql)

## Build the solution

Just execute `dotnet build` in the root directory, it takes `ShippingService.sln` and build everything.
