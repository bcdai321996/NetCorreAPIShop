FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ShopNetCoreApi/ShopNetCoreApi.csproj", "ShopNetCoreApi/"]
COPY ShopNetCoreApi/SetupData.sh SetupData.sh
RUN dotnet tool install --global dotnet-ef
RUN dotnet restore "ShopNetCoreApi/ShopNetCoreApi.csproj" 
COPY . .
WORKDIR "/src/ShopNetCoreApi"
RUN /root/.dotnet/tools/dotnet-ef migrations add InitialMigrations
RUN chmod +x ./SetupData.sh
CMD /bin/bash ./SetupData.sh