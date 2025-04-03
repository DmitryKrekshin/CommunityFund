FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8082
EXPOSE 8083

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CommunityFund.WebApi/CommunityFund.WebApi.csproj", "CommunityFund.WebApi/"]
COPY ["CommunityFund/CommunityFund.csproj", "CommunityFund/"]
COPY ["CommunityFund.Domain/CommunityFund.Domain.csproj", "CommunityFund.Domain/"]
COPY ["CommunityFund.Infrastructure/CommunityFund.Infrastructure.csproj", "CommunityFund.Infrastructure/"]
RUN dotnet restore "CommunityFund.WebApi/CommunityFund.WebApi.csproj"
COPY . .
WORKDIR "/src/CommunityFund.WebApi"
RUN dotnet build "CommunityFund.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CommunityFund.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommunityFund.WebApi.dll"]
