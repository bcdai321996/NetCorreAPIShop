FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ShopNetCoreApi/ShopNetCoreApi.csproj", "ShopNetCoreApi/"]
COPY ShopNetCoreApi/Setup.sh Setup.sh

RUN dotnet tool install --global dotnet-ef

RUN dotnet restore "ShopNetCoreApi/ShopNetCoreApi.csproj" 
COPY . .
WORKDIR "/src/ShopNetCoreApi"

RUN /root/.dotnet/tools/dotnet-ef migrations add InitialMigrations


RUN chmod +x ./Setup.sh
CMD /bin/bash ./Setup.sh