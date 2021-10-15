FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["src/EmailingService/EmailingService.csproj", "src/EmailingService/"]
COPY ["src/EmailingService/EmailingService.csproj", "src/EmailingService.Contracts/"]
RUN dotnet restore "src/EmailingService/EmailingService.csproj"

COPY . .
WORKDIR "/src/src/EmailingService"

RUN dotnet build "EmailingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmailingService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmailingService.dll"]
