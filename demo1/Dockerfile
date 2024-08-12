
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .
COPY ["aspnetapp/*.csproj", "aspnetapp/"]
RUN dotnet restore *.sln
COPY ["aspnetapp/.", "aspnetapp/"]
WORKDIR "/src/aspnetapp"
RUN dotnet build "aspnetapp.csproj" -c release -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "aspnetapp.csproj" -c release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "aspnetapp.dll"]
