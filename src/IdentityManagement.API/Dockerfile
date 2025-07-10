FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/IdentityManagement.API/IdentityManagement.API.csproj", "src/IdentityManagement.API/"]
COPY ["src/IdentityManagement.Application/IdentityManagement.Application.csproj", "src/IdentityManagement.Application/"]
COPY ["src/IdentityManagement.Domain/IdentityManagement.Domain.csproj", "src/IdentityManagement.Domain/"]
COPY ["src/IdentityManagement.Infrastructure/IdentityManagement.Infrastructure.csproj", "src/IdentityManagement.Infrastructure/"]
RUN dotnet restore "src/IdentityManagement.API/IdentityManagement.API.csproj"
COPY . .
WORKDIR "/src/src/IdentityManagement.API"
RUN dotnet build "IdentityManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityManagement.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityManagement.API.dll"]