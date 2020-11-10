#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Carmera.HttpHost/Carmera.HttpHost.csproj", "Carmera.HttpHost/"]
RUN dotnet restore "Carmera.HttpHost/Carmera.HttpHost.csproj"
COPY . .
WORKDIR "/src/Carmera.HttpHost"
RUN dotnet build "Carmera.HttpHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Carmera.HttpHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Carmera.HttpHost.dll"]