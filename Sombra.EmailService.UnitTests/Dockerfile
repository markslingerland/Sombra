FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.EmailService.UnitTests/Sombra.EmailService.UnitTests.csproj Sombra.EmailService.UnitTests/

COPY . .
WORKDIR /src/Sombra.EmailService.UnitTests
RUN dotnet build -c Release -o /app

RUN dotnet test