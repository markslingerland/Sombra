FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.IdentityService.UnitTests/Sombra.IdentityService.UnitTests.csproj Sombra.IdentityService.UnitTests/
COPY Sombra.IdentityService/Sombra.IdentityService.csproj Sombra.IdentityService/

COPY . .
WORKDIR /src/Sombra.IdentityService.UnitTests
RUN dotnet build -c Release -o /app

RUN dotnet test