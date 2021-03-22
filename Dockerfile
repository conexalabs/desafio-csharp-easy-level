#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ClimaAPI/ClimaAPI.csproj", "ClimaAPI/"]
COPY ["ClimaAPI.Application/ClimaAPI.Application.csproj", "ClimaAPI.Application/"]
COPY ["ClimaAPI.Domain/ClimaAPI.Domain.csproj", "ClimaAPI.Domain/"]
COPY ["ClimaAPI.Infrastructure/ClimaAPI.Infrastructure.csproj", "ClimaAPI.Infrastructure/"]
RUN dotnet restore "ClimaAPI/ClimaAPI.csproj"
COPY . .
WORKDIR "/src/ClimaAPI"
RUN dotnet build "ClimaAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClimaAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/ClimaAPI/ClimaAPI.xml .
ENTRYPOINT ["dotnet", "ClimaAPI.dll"]