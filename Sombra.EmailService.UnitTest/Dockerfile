FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.Web.UnitTests/Sombra.EmailService.UnitTest.csproj Sombra.EmailService.UnitTest/
COPY Sombra.EmailService/Sombra.EmailService.csproj Sombra.EmailService/

COPY . .
WORKDIR /src/Sombra.EmailService.UnitTests
RUN dotnet build -c Release -o /app

RUN dotnet test