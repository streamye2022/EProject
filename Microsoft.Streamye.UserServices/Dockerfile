FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Microsoft.Streamye.UserServices/Microsoft.Streamye.UserServices.csproj", "Microsoft.Streamye.UserServices/"]
RUN dotnet restore "Microsoft.Streamye.UserServices/Microsoft.Streamye.UserServices.csproj"
COPY . .
WORKDIR "/src/Microsoft.Streamye.UserServices"
RUN dotnet build "Microsoft.Streamye.UserServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microsoft.Streamye.UserServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microsoft.Streamye.UserServices.dll"]
