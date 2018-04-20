FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.UserService.UnitTests/Sombra.UserService.UnitTests.csproj Sombra.UserService.UnitTests/
COPY . .
WORKDIR /src/Sombra.UserService.UnitTests
RUN dotnet build -c Release -o /app

RUN dotnet test