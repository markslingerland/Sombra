FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Sombra.sln ./
COPY Sombra.DonateService/Sombra.DonateService.csproj Sombra.DonateService/
COPY . .
WORKDIR /src/Sombra.DonateService

RUN dotnet build -c Debug -o /app

FROM build AS publish
RUN dotnet publish -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Sombra.DonateService.dll"]
