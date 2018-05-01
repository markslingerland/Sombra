FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.Web.UnitTests/Sombra.Web.UnitTests.csproj Sombra.Web.UnitTests/
COPY Sombra.Web/Sombra.Web.csproj Sombra.Web/

COPY . .
WORKDIR /src/Sombra.Web.UnitTests
RUN dotnet build -c Debug -o /app

RUN dotnet test