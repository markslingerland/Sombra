FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.DonateService.UnitTests/Sombra.DonateService.UnitTests.csproj Sombra.DonateService.UnitTests/
COPY . .
WORKDIR /src/Sombra.DonateService.UnitTests

RUN dotnet build -c Debug -o /app

RUN dotnet test