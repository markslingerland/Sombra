FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.SearchService.UnitTests/Sombra.SearchService.UnitTests.csproj Sombra.SearchService.UnitTests/
COPY . .
WORKDIR /src/Sombra.SearchService.UnitTests

RUN dotnet build -c Debug -o /app

RUN dotnet test