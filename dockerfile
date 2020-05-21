FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /work
COPY src .

FROM build AS publish
WORKDIR /work/HappyCode.NetCoreBoilerplate.Api
RUN dotnet publish -c Release -o /app

LABEL maintainer="Lukasz Kurzyniec (lkurzyniec@gmail.com)"

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HappyCode.NetCoreBoilerplate.Api.dll"]
