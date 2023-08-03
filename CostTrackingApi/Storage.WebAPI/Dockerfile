#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Storage.WebAPI/Storage.WebAPI.csproj", "Storage.WebAPI/"]
COPY ["Storage.Application/Storage.Application.csproj", "Storage.Application/"]
COPY ["Storage.Domain/Storage.Domain.csproj", "Storage.Domain/"]
COPY ["Storage.Infrastructure.Persistance/Storage.Infrastructure.Persistance.csproj", "Storage.Infrastructure.Persistance/"]
RUN dotnet restore "Storage.WebAPI/Storage.WebAPI.csproj"
COPY . .
WORKDIR "/src/Storage.WebAPI"
RUN dotnet build "Storage.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Storage.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Storage.WebAPI.dll"]