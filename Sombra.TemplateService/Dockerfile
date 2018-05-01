FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Sombra.sln ./
COPY Sombra.TemplateService/Sombra.TemplateService.csproj Sombra.TemplateService/
COPY . .
WORKDIR /src/Sombra.TemplateService
RUN dotnet restore -p:RestoreUseSkipNonexistentTargets=false
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Sombra.TemplateService.dll"]
