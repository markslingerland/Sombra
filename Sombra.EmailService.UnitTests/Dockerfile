﻿FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.EmailService.UnitTests/Sombra.EmailService.UnitTests.csproj Sombra.EmailService.UnitTests/

COPY . .
WORKDIR /src/Sombra.EmailService.UnitTests

RUN dotnet build -c Debug -o /app

RUN dotnet test