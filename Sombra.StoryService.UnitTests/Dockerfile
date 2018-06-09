FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.StoryService.UnitTests/Sombra.StoryService.UnitTests.csproj Sombra.StoryService.UnitTests/
COPY . .
WORKDIR /src/Sombra.StoryService.UnitTests

RUN dotnet build -c Debug -o /app

RUN dotnet test