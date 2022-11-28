FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 13000

ENV ASPNETCORE_URLS=http://+:13000
ENV ASPNETCORE_ENVIRONMENT ASPNETCORE_ENVIRONMENT
ENV LITTERDB_CONNECTIONSTRING LITTERDB_CONNECTIONSTRING
ENV RUN_MIGRATIONS RUN_MIGRATIONS

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build

# copy all the layers' csproj files into respective folders
COPY ["src/Core/LitterBackend.Domain/LitterBackend.Domain.csproj", "Core/LitterBackend.Domain/"]
COPY ["src/Core/LitterBackend.Application/LitterBackend.Application.csproj", "Core/LitterBackend.Application/"]
COPY ["src/Infrastructure/LitterBackend.Infrastructure/LitterBackend.Infrastructure.csproj", "Infrastructure/LitterBackend.Infrastructure/"]
COPY ["src/Infrastructure/LitterBackend.Persistence/LitterBackend.Persistence.csproj", "Infrastructure/LitterBackend.Persistence/"]
COPY ["src/API/LitterBackend.API/LitterBackend.API.csproj", "API/LitterBackend.API/"]

RUN dotnet restore "API/LitterBackend.API/LitterBackend.API.csproj"
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LitterBackend.API.dll"]
