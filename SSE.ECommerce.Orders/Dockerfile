#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SSE.ECommerce.Orders/SSE.ECommerce.Orders.csproj", "SSE.ECommerce.Orders/"]
RUN dotnet restore "SSE.ECommerce.Orders/SSE.ECommerce.Orders.csproj"
COPY . .
WORKDIR "/src/SSE.ECommerce.Orders"
RUN dotnet build "SSE.ECommerce.Orders.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SSE.ECommerce.Orders.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SSE.ECommerce.Orders.dll"]