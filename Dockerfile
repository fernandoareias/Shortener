#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Encurtador.API/Shortener.API.csproj", "src/Encurtador.API/"]
RUN dotnet restore "src/Encurtador.API/Shortener.API.csproj"
COPY . .
WORKDIR "/src/src/Encurtador.API"
RUN dotnet build "Shortener.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shortener.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shortener.API.dll"]
