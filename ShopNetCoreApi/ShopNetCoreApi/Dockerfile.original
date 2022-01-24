#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE DOCKER_BUILDKIT=0
EXPOSE COMPOSE_DOCKER_CLI_BUILD=0
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ShopNetCoreApi/ShopNetCoreApi.csproj", "ShopNetCoreApi/"]
RUN dotnet restore "ShopNetCoreApi/ShopNetCoreApi.csproj"
COPY . .
WORKDIR "/src/ShopNetCoreApi"
RUN dotnet build "ShopNetCoreApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopNetCoreApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopNetCoreApi.dll"]
