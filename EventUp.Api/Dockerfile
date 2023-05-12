FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EventUp.Api/EventUp.Api.csproj", "EventUp.Api/"]
RUN dotnet restore "EventUp.Api/EventUp.Api.csproj"
COPY . .
WORKDIR "/src/EventUp.Api"
RUN dotnet build "EventUp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventUp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventUp.Api.dll"]
