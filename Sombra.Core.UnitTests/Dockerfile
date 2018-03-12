FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.Core.UnitTests/Sombra.Core.UnitTests.csproj Sombra.Core.UnitTests/
RUN dotnet restore
COPY . .
WORKDIR /src/Sombra.Core.UnitTests
RUN dotnet build -c Release -o /app

RUN dotnet test