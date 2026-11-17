FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Marketplace.API/Marketplace.API.csproj", "Marketplace.API/"]
COPY ["Marketplace.Application/Marketplace.Application.csproj", "Marketplace.Application/"]
COPY ["Marketplace.Domain/Marketplace.Domain.csproj", "Marketplace.Domain/"]
COPY ["Marketplace.Infrastructure/Marketplace.Infrastructure.csproj", "Marketplace.Infrastructure/"]
RUN dotnet restore "Marketplace.API/Marketplace.API.csproj"
COPY . .
WORKDIR "/src/Marketplace.API"
RUN dotnet build "./Marketplace.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Marketplace.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Marketplace.API.dll"]
