#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

# HTTP/1 - Restful API
EXPOSE 5000 

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Paradiso.API/Paradiso.API.csproj", "Paradiso.API/"]
COPY ["Paradiso.API.Domain/Paradiso.API.Domain.csproj", "Paradiso.API.Domain/"]
COPY ["Paradiso.API.Service/Paradiso.API.Service.csproj", "Paradiso.API.Service/"]
COPY ["Paradiso.API.Infra/Paradiso.API.Infra.csproj", "Paradiso.API.Infra/"]
RUN dotnet restore "Paradiso.API/Paradiso.API.csproj"
COPY . .
WORKDIR "/src/Paradiso.API"
RUN dotnet build "Paradiso.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Paradiso.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Paradiso.API.dll"]