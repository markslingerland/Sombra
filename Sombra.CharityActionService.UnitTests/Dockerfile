FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.CharityActionService.UnitTests/Sombra.CharityActionService.UnitTests.csproj Sombra.CharityActionService.UnitTests/
COPY . .
WORKDIR /src/Sombra.CharityActionService.UnitTests
RUN dotnet build -c Release -o /app

RUN dotnet test