#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Finance.WebAPI/Finance.WebAPI.csproj", "Finance.WebAPI/"]
COPY ["Finance.Application/Finance.Application.csproj", "Finance.Application/"]
COPY ["Finance.Domain/Finance.Domain.csproj", "Finance.Domain/"]
COPY ["Finance.Infrastructure.Persistance/Finance.Infrastructure.Persistance.csproj", "Finance.Infrastructure.Persistance/"]
RUN dotnet restore "Finance.WebAPI/Finance.WebAPI.csproj"
COPY . .
WORKDIR "/src/Finance.WebAPI"
RUN dotnet build "Finance.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Finance.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Finance.WebAPI.dll"]