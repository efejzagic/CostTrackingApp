#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Auth.WebAPI/Auth.WebAPI.csproj", "Auth.WebAPI/"]
COPY ["Auth.Application/Auth.Application.csproj", "Auth.Application/"]
COPY ["Auth.Domain/Auth.Domain.csproj", "Auth.Domain/"]
COPY ["Auth.Infrastructure.Persistance/Auth.Infrastructure.Persistance.csproj", "Auth.Infrastructure.Persistance/"]
COPY ["JwtAuthenticationManager/JwtAuthenticationManager.csproj", "JwtAuthenticationManager/"]
RUN dotnet restore "Auth.WebAPI/Auth.WebAPI.csproj"
COPY . .
WORKDIR "/src/Auth.WebAPI"
RUN dotnet build "Auth.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Auth.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.WebAPI.dll"]