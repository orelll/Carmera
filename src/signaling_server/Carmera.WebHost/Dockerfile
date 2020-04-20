FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Carmera.WebHost/Carmera.WebHost.csproj", "Carmera.WebHost/"]
RUN dotnet restore "Carmera.WebHost/Carmera.WebHost.csproj"
COPY . .
WORKDIR "/src/Carmera.WebHost"
RUN dotnet build "Carmera.WebHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Carmera.WebHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Carmera.WebHost.dll"]