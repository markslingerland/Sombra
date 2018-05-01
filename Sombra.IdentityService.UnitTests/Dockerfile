FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.IdentityService.UnitTests/Sombra.IdentityService.UnitTests.csproj Sombra.IdentityService.UnitTests/
COPY Sombra.IdentityService/Sombra.IdentityService.csproj Sombra.IdentityService/

COPY . .
WORKDIR /src/Sombra.IdentityService.UnitTests
RUN dotnet restore -p:RestoreUseSkipNonexistentTargets=false
RUN dotnet build -c Release -o /app

RUN dotnet test