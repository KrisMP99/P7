#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV DOTNET_URLS=http://+:5000
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ENV DOTNET_URLS=http://+:5000
WORKDIR /src
COPY ["P7WebApp.API/P7WebApp.API.csproj", "P7WebApp.API/"]
COPY ["P7WebApp.Infrastructure/P7WebApp.Infrastructure.csproj", "P7WebApp.Infrastructure/"]
COPY ["P7WebApp.Application/P7WebApp.Application.csproj", "P7WebApp.Application/"]
COPY ["P7WebApp.SharedKernel/P7WebApp.SharedKernel.csproj", "P7WebApp.SharedKernel/"]
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