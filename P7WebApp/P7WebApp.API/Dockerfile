#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["P7WebApp.API/P7WebApp.API.csproj", "P7WebApp.API/"]
RUN dotnet restore "P7WebApp.API/P7WebApp.API.csproj"
COPY . .
WORKDIR "/src/P7WebApp.API"
RUN dotnet build "P7WebApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "P7WebApp.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "P7WebApp.API.dll"]