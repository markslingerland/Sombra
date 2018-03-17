FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY Sombra.sln ./
COPY ../Sombra.EmailService/Sombra.EmailService.csproj ../Sombra.EmailService/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/../Sombra.EmailService
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Sombra.EmailService.dll"]
