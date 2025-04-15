FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Rabbit.API/Rabbit.API.csproj", "Rabbit.API/"]
COPY ["Rabbit.Infrastructure/Rabbit.Infrastructure.csproj", "Rabbit.Infrastructure/"]
COPY ["Rabbit.Application/Rabbit.Application.csproj", "Rabbit.Application/"]
COPY ["Rabbit.Domain/Rabbit.Domain.csproj", "Rabbit.Domain/"]
RUN dotnet restore "Rabbit.API/Rabbit.API.csproj"
COPY . .
WORKDIR "/src/Rabbit.API"
RUN dotnet build "Rabbit.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Rabbit.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rabbit.API.dll"]
