FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.Web.UnitTests/Sombra.Web.UnitTests.csproj Sombra.Web.UnitTests/
COPY Sombra.Web/Sombra.Web.csproj Sombra.Web/
RUN dotnet restore
COPY . .
WORKDIR /src/Sombra.Web.UnitTests
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Sombra.Web.UnitTests.dll"]
