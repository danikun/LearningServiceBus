FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mailing/Mailing.csproj", "Mailing/"]
RUN dotnet restore "Mailing/Mailing.csproj"
COPY . .
WORKDIR "/src/Mailing"
RUN dotnet build "Mailing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mailing.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mailing.dll"]
