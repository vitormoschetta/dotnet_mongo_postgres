ARG DOTNET_VERSION=8.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build

COPY . /app/
RUN dotnet publish /app/dotnet_mongodb.csproj -c Release -o /public

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}
WORKDIR /public
COPY --from=build /public .

ENTRYPOINT ["/usr/bin/dotnet", "/public/dotnet_mongodb.dll"]
