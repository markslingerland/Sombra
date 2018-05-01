FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Sombra.sln ./
COPY Sombra.IdentityService/Sombra.IdentityService.csproj Sombra.IdentityService/
COPY . .
WORKDIR /src/Sombra.IdentityService

RUN dotnet build -c Debug -o /app

FROM build AS publish
RUN dotnet publish -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Sombra.IdentityService.dll"]
