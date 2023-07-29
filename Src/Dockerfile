FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SnapShop.csproj", "./"]
RUN dotnet restore "./SnapShop.csproj"
RUN dotnet tool install --global dotnet-watch
COPY . .
WORKDIR "/src/."
RUN dotnet build "SnapShop.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SnapShop.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SnapShop.dll"]
