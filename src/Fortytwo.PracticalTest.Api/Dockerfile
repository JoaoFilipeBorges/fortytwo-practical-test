FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Fortytwo.PracticalTest.Api/Fortytwo.PracticalTest.Api.csproj", "src/Fortytwo.PracticalTest.Api/"]
RUN dotnet restore "src/Fortytwo.PracticalTest.Api/Fortytwo.PracticalTest.Api.csproj"
COPY . .
WORKDIR "/src/src/Fortytwo.PracticalTest.Api"
RUN dotnet build "Fortytwo.PracticalTest.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Fortytwo.PracticalTest.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fortytwo.PracticalTest.Api.dll"]
