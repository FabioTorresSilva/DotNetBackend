# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App

# Copy the entire Radao folder to preserve structure
COPY Radao/ Radao/

# Restore dependencies
WORKDIR /App/Radao
RUN dotnet restore

# Build and publish **only the project**, NOT the solution
RUN dotnet publish -c Release -o /App/out --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

# Copy the published output from the build stage
COPY --from=build /App/out .

# Run the app
ENTRYPOINT ["dotnet", "Sprint3DotNet.dll"]
