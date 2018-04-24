FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.IntegrationTests/Sombra.IntegrationTests.csproj Sombra.IntegrationTests/
COPY . .
WORKDIR /src/Sombra.IntegrationTests
RUN dotnet build -c Release -o /app

RUN dotnet test