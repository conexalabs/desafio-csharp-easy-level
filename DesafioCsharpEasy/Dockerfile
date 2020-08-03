FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY DesafioCsharpEasy.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
COPY --from=build /app .
ENTRYPOINT ["dotnet", "DesafioCsharpEasy.dll"]
