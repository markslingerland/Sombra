FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.CharityService.UnitTests/Sombra.CharityService.UnitTests.csproj Sombra.CharityService.UnitTests/
COPY . .
WORKDIR /src/Sombra.CharityService.UnitTests
RUN dotnet build -c Release -o /app

RUN dotnet test