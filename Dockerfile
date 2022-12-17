FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 13000

ENV ASPNETCORE_URLS=http://+:13000
ENV ASPNETCORE_ENVIRONMENT ASPNETCORE_ENVIRONMENT
ENV LITTERDB_HOST LITTERDB_HOST
ENV LITTERDB_DATABASE LITTERDB_DATABASE
ENV LITTERDB_USER LITTERDB_USER
ENV LITTERDB_PASSWORD LITTERDB_PASSWORD
ENV JWTISSUER JWTISSUER
ENV JWTAUDIENCE JWTAUDIENCE
ENV JWTPUBLICKEY JWTPUBLICKEY
ENV RUN_MIGRATIONS RUN_MIGRATIONS

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build

# copy all the layers' csproj files into respective folders
COPY ["src/Core/LitterService.Domain/LitterService.Domain.csproj", "Core/LitterService.Domain/"]
COPY ["src/Core/LitterService.Application/LitterService.Application.csproj", "Core/LitterService.Application/"]
COPY ["src/Infrastructure/LitterService.Infrastructure/LitterService.Infrastructure.csproj", "Infrastructure/LitterService.Infrastructure/"]
COPY ["src/Infrastructure/LitterService.Persistence/LitterService.Persistence.csproj", "Infrastructure/LitterService.Persistence/"]
COPY ["src/API/LitterService.API/LitterService.API.csproj", "API/LitterService.API/"]

RUN dotnet restore "API/LitterService.API/LitterService.API.csproj"
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LitterService.API.dll"]
