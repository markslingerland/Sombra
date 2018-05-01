FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY Sombra.UserService.UnitTests/Sombra.UserService.UnitTests.csproj Sombra.UserService.UnitTests/
COPY . .
WORKDIR /src/Sombra.UserService.UnitTests
RUN dotnet restore -nowarn:msb3202,nu1503 -p:RestoreUseSkipNonexistentTargets=false
RUN dotnet build -c Release -o /app

RUN dotnet test