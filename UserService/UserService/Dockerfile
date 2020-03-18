FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-users
WORKDIR /source

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out/users

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-users /source/out/users .
ENTRYPOINT ["dotnet", "UserService.dll"]