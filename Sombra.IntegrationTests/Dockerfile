FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.IntegrationTests/Sombra.IntegrationTests.csproj Sombra.IntegrationTests/
COPY . .
WORKDIR /src/Sombra.IntegrationTests
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet vstest", "Sombra.IntegrationTests.dll"]
