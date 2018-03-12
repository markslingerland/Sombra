FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.Web/Sombra.Web.csproj Sombra.Web/
RUN dotnet restore
COPY . .
WORKDIR /src/Sombra.Web
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

ENV APSNETCORE_ENVIRONMENT docker

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Sombra.Web.dll"]