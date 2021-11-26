FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build-env

WORKDIR /app

COPY WorkScheduleMaker.csproj ./

RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim

WORKDIR /app

COPY --from=build-env /app/out .

CMD ASPNETCORE\_URLS=http://\*:$PORT dotnet DemoMVCApp.dll