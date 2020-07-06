# Markono Shipping Service
Markono Shipping Service Rate

## Source code contains

1. [Autofac](https://autofac.org/)
1. [Swagger](https://swagger.io/) + [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle)
1. [EF Core](https://docs.microsoft.com/ef/)
    * [Npgsql](https://github.com/npgsql/npgsql)
1. Tests
    * Unit tests
        * [AutoFixture](https://github.com/AutoFixture/AutoFixture)
        * [FluentAssertions]
        * [Moq](https://github.com/moq/moq4)
        * [Moq.AutoMock](https://github.com/moq/Moq.AutoMocker)
        * [xUnit]
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

* Console application as a simple db migration tool - [Program.cs](src/ShippingService.Db/Program.cs

## Build the solution

Just execute `dotnet build` in the root directory, it takes `ShippingService.sln` and build everything.
