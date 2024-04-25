FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MuafiyetProjesi2024.csproj", "MuafiyetProjesi2024/"]
RUN dotnet restore "MuafiyetProjesi2024.csproj"
COPY . .
WORKDIR "/src/MuafiyetProjesi2024"
RUN dotnet build "MuafiyetProjesi2024.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MuafiyetProjesi2024.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MuafiyetProjesi2024.dll"]
