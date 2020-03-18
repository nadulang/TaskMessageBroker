FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-notifications
WORKDIR /source

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out/notifications

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-notifications source/out/notifications .
ENTRYPOINT ["dotnet", "NotificationService.dll"]
