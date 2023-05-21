FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY EventUp.Api/*.csproj ./EventUp.Api/
COPY EventUp.Application/*.csproj ./EventUp.Application/
COPY EventUp.Infrastructure/*.csproj ./EventUp.Infrastructure/
COPY EventUp.Domain/*.csproj ./EventUp.Domain/
RUN dotnet restore ./EventUp.Api/EventUp.Api.csproj

COPY . ./
RUN dotnet publish -c Release -o out EventUp.Api/EventUp.Api.csproj

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "EventUp.Api.dll"]