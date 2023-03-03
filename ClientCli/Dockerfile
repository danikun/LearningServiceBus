FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ClientCli/ClientCli.csproj", "ClientCli/"]
RUN dotnet restore "ClientCli/ClientCli.csproj"
COPY . .
WORKDIR "/src/ClientCli"
RUN dotnet build "ClientCli.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClientCli.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientCli.dll"]
