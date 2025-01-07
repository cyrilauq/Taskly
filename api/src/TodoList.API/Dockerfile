# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["api/src/TodoList.API/TodoList.API.csproj", "api/src/TodoList.API/"]
COPY ["api/src/TodoList.Infrastructure/TodoList.Infrastructure.csproj", "api/src/TodoList.Infrastructure/"]
COPY ["api/src/TodoList.Application/TodoList.Application.csproj", "api/src/TodoList.Application/"]
COPY ["api/src/TodoList.Domain/TodoList.Domain.csproj", "api/src/TodoList.Domain/"]
RUN dotnet restore "./api/src/TodoList.API/TodoList.API.csproj"
COPY . .
WORKDIR "/src/api/src/TodoList.API"
RUN dotnet build "./TodoList.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TodoList.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoList.API.dll"]