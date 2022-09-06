FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Microsoft.Streamye.SeckillAggregateServices/Microsoft.Streamye.SeckillAggregateServices.csproj", "Microsoft.Streamye.SeckillAggregateServices/"]
RUN dotnet restore "Microsoft.Streamye.SeckillAggregateServices/Microsoft.Streamye.SeckillAggregateServices.csproj"
COPY . .
WORKDIR "/src/Microsoft.Streamye.SeckillAggregateServices"
RUN dotnet build "Microsoft.Streamye.SeckillAggregateServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microsoft.Streamye.SeckillAggregateServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microsoft.Streamye.SeckillAggregateServices.dll"]
