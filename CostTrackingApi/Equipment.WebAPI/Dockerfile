#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Equipment.WebAPI/Equipment.WebAPI.csproj", "Equipment.WebAPI/"]
COPY ["Equipment.Application/Equipment.Application.csproj", "Equipment.Application/"]
COPY ["Equipment.Domain/Equipment.Domain.csproj", "Equipment.Domain/"]
COPY ["Equipment.Infrastructure.Persistance/Equipment.Infrastructure.Persistance.csproj", "Equipment.Infrastructure.Persistance/"]
RUN dotnet restore "Equipment.WebAPI/Equipment.WebAPI.csproj"
COPY . .
WORKDIR "/src/Equipment.WebAPI"
RUN dotnet build "Equipment.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Equipment.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Equipment.WebAPI.dll"]