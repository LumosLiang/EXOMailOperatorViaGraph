# stage one

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /EXOMailOperatorViaGraph

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o out

# stage two

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /EXOMailOperatorViaGraph
COPY --from=build-env /EXOMailOperatorViaGraph/out .
ENTRYPOINT ["dotnet", "EXOMailOperatorViaGraph.dll"]