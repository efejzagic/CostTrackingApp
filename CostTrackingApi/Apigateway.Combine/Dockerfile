#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Apigateway.Combine/Apigateway.Combine.csproj", "Apigateway.Combine/"]
RUN dotnet restore "Apigateway.Combine/Apigateway.Combine.csproj"
COPY . .
WORKDIR "/src/Apigateway.Combine"
RUN dotnet build "Apigateway.Combine.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Apigateway.Combine.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Apigateway.Combine.dll"]