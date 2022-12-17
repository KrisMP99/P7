#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS=http://+:5000 DOTNET_RUNNING_IN_CONTAINER=true
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ENV ASPNETCORE_URLS=http://+:5000 DOTNET_RUNNING_IN_CONTAINER=true
WORKDIR /src
EXPOSE 5000
COPY ["P7WebApp.API/P7WebApp.API.csproj", "P7WebApp.API/"]
COPY ["P7WebApp.Infrastructure/P7WebApp.Infrastructure.csproj", "P7WebApp.Infrastructure/"]
COPY ["P7WebApp.Application/P7WebApp.Application.csproj", "P7WebApp.Application/"]
RUN dotnet restore "P7WebApp.API/P7WebApp.API.csproj"
COPY . .
WORKDIR "/src/P7WebApp.API"
RUN dotnet build "P7WebApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "P7WebApp.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "P7WebApp.API.dll"]

FROM build AS development
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "P7WebApp.API.dll", "--environment=Development"]

FROM build AS production
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "P7WebApp.API.dll"]