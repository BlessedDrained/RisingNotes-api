FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY . .
WORKDIR "/src/Api"
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS run
EXPOSE 80
EXPOSE 443
WORKDIR /app/publish
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]