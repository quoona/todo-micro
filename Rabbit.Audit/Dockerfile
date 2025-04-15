FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Rabbit.Audit/Rabbit.Audit.csproj", "Rabbit.Audit/"]
RUN dotnet restore "Rabbit.Audit/Rabbit.Audit.csproj"
COPY . .
WORKDIR "/src/Rabbit.Audit"
RUN dotnet build "Rabbit.Audit.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Rabbit.Audit.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rabbit.Audit.dll"]
