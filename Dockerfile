# stage one

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /SimpleMailSender

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o out

# stage two

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /SimpleMailSender
COPY --from=build-env /SimpleMailSender/out .
ENTRYPOINT ["dotnet", "SimpleMailSender.dll"]