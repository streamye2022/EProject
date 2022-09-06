FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Microsoft.Streamye.OrderServices/Microsoft.Streamye.OrderServices.csproj", "Microsoft.Streamye.OrderServices/"]
RUN dotnet restore "Microsoft.Streamye.OrderServices/Microsoft.Streamye.OrderServices.csproj"
COPY . .
WORKDIR "/src/Microsoft.Streamye.OrderServices"
RUN dotnet build "Microsoft.Streamye.OrderServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microsoft.Streamye.OrderServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microsoft.Streamye.OrderServices.dll"]
