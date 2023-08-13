#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ConstructionSite.WebAPI/ConstructionSite.WebAPI.csproj", "ConstructionSite.WebAPI/"]
COPY ["ConstructionSite.Application/ConstructionSite.Application.csproj", "ConstructionSite.Application/"]
COPY ["ConstructionSite.Domain/ConstructionSite.Domain.csproj", "ConstructionSite.Domain/"]
COPY ["ConstructionSite.Infrastructure.Persistance/ConstructionSite.Infrastructure.Persistance.csproj", "ConstructionSite.Infrastructure.Persistance/"]
RUN dotnet restore "ConstructionSite.WebAPI/ConstructionSite.WebAPI.csproj"
COPY . .
WORKDIR "/src/ConstructionSite.WebAPI"
RUN dotnet build "ConstructionSite.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConstructionSite.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConstructionSite.WebAPI.dll"]