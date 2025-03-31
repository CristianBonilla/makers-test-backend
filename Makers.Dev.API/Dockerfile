#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

ARG DOTNET_RUNTIME=mcr.microsoft.com/dotnet/aspnet:8.0
ARG DOTNET_SDK=mcr.microsoft.com/dotnet/sdk:8.0

FROM $DOTNET_RUNTIME AS base
USER $APP_UID
WORKDIR /app
EXPOSE 408
EXPOSE 386

FROM $DOTNET_SDK AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Makers.Dev.API/Makers.Dev.API.csproj", "Makers.Dev.API/"]
COPY ["Makers.Dev.Contracts/Makers.Dev.Contracts.csproj", "Makers.Dev.Contracts/"]
COPY ["Makers.Dev.Domain.Entities/Makers.Dev.Domain.Entities.csproj", "Makers.Dev.Domain.Entities/"]
COPY ["Makers.Dev.Domain.Helpers/Makers.Dev.Domain.Helpers.csproj", "Makers.Dev.Domain.Helpers/"]
COPY ["Makers.Dev.Domain/Makers.Dev.Domain.csproj", "Makers.Dev.Domain/"]
COPY ["Makers.Dev.Infrastructure/Makers.Dev.Infrastructure.csproj", "Makers.Dev.Infrastructure/"]

COPY ["Makers.Dev.Tests/Makers.Dev.Tests.csproj", "Makers.Dev.Tests/"]

RUN dotnet restore "./Makers.Dev.API/Makers.Dev.API.csproj"
COPY . .
WORKDIR "/src/Makers.Dev.API"
RUN dotnet build "./Makers.Dev.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Makers.Dev.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Makers.Dev.API.dll"]
