FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DesafioCsharpEasy.csproj", ""]
RUN dotnet restore "./DesafioCsharpEasy.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DesafioCsharpEasy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DesafioCsharpEasy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesafioCsharpEasy.dll"]