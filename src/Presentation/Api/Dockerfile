#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Presentation/Api/Api.csproj", "Presentation/Api/"]
COPY ["Presentation/TelegramBot/TelegramBot.csproj", "Presentation/TelegramBot/"]
COPY ["Core/Application/Application.csproj", "Core/Application/"]
COPY ["Core/DataAccess/DataAccess.csproj", "Core/DataAccess/"]
COPY ["Core/Domain/Domain.csproj", "Core/Domain/"]
COPY ["Common/Utilities/Utilities.csproj", "Common/Utilities/"]
COPY ["Infrastructure/Persistence/Persistence.csproj", "Infrastructure/Persistence/"]
RUN dotnet restore "Presentation/Api/Api.csproj"
COPY . .
WORKDIR "/src/Presentation/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]