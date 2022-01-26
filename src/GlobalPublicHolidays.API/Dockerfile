#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/GlobalPublicHolidays.API/GlobalPublicHolidays.API.csproj", "src/GlobalPublicHolidays.API/"]
COPY ["src/GlobalPublicHolidays.Infrastructure/GlobalPublicHolidays.Infrastructure.csproj", "src/GlobalPublicHolidays.Infrastructure/"]
COPY ["src/GlobalPublicHolidays.Application/GlobalPublicHolidays.Application.csproj", "src/GlobalPublicHolidays.Application/"]
COPY ["src/GlobalPublicHolidays.Core/GlobalPublicHolidays.Domain.csproj", "src/GlobalPublicHolidays.Core/"]
RUN dotnet restore "src/GlobalPublicHolidays.API/GlobalPublicHolidays.API.csproj"
COPY . .
WORKDIR "/src/src/GlobalPublicHolidays.API"
RUN dotnet build "GlobalPublicHolidays.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GlobalPublicHolidays.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GlobalPublicHolidays.API.dll"]