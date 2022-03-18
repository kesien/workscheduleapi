FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app 

ARG ASPNETCORE_ENVIRONMENT
ARG DATABASE_URL
ENV ASPNETCORE_ENVIRONMENT $ASPNETCORE_ENVIRONMENT
ENV DATABASE_URL $DATABASE_URL
#
# copy csproj and restore as distinct layers
COPY *.sln .
COPY WorkSchedule.Api/*.csproj ./WorkSchedule.Api/
COPY WorkSchedule.Application/*.csproj ./WorkSchedule.Application/
COPY WorkSchedule.UnitTests/*.csproj ./WorkSchedule.UnitTests/
COPY WorkSchedule.Web/*.csproj ./WorkSchedule.Web/
#
RUN dotnet restore 
#
# copy everything else and build app
COPY WorkSchedule.Api/. ./WorkSchedule.Api/
COPY WorkSchedule.Application/. ./WorkSchedule.Application/
COPY WorkSchedule.UnitTests/. ./WorkSchedule.UnitTests/ 
COPY WorkSchedule.Web/. ./WorkSchedule.Web/ 
#
WORKDIR /app/WorkSchedule.Web
RUN dotnet tool restore
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app 
#
COPY --from=build /app/WorkSchedule.Web/out ./
ENTRYPOINT ["dotnet", "WorkSchedule.Web.dll"]