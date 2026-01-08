# Use the official .NET 8 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["ProductCatalog.Api.csproj", "./"]
RUN dotnet restore "ProductCatalog.Api.csproj"

# Copy the rest of the application code
COPY . .

# Build the application
RUN dotnet build "ProductCatalog.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "ProductCatalog.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the official .NET 8 runtime image for running
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Expose port 8080 (standard for Container Apps)
EXPOSE 8080

# Copy the published app from the publish stage
COPY --from=publish /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "ProductCatalog.Api.dll"]