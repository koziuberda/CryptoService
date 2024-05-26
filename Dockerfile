# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project files
COPY ["CryptoService.API/CryptoService.API.csproj", "CryptoService.API/"]
COPY ["CryptoService.Core.Domain/CryptoService.Core.Domain.csproj", "CryptoService.Core.Domain/"]
COPY ["CryptoService.Data/CryptoService.Data.csproj", "CryptoService.Data/"]
COPY ["CryptoService.Integrations.CoinApi/CryptoService.Integrations.CoinApi.csproj", "CryptoService.Integrations.CoinApi/"]
COPY ["CryptoService.Logic/CryptoService.Logic.csproj", "CryptoService.Logic/"]

# Restore dependencies
RUN dotnet restore "CryptoService.API/CryptoService.API.csproj"

# Copy the rest of the application code
COPY . .

# Build the application
WORKDIR "/src/CryptoService.API"
RUN dotnet build "CryptoService.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "CryptoService.API.csproj" -c Release -o /app/publish

# Create the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CryptoService.API.dll"]
