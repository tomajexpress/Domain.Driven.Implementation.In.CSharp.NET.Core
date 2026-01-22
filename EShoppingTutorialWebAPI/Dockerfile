# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EShoppingTutorialWebAPI/EShoppingTutorialWebAPI.csproj", "EShoppingTutorialWebAPI/"]
COPY ["EShoppingTutorial/EShoppingTutorial.Core.Application/EShoppingTutorial.Core.Application.csproj", "EShoppingTutorial/EShoppingTutorial.Core.Application/"]
COPY ["EShoppingTutorial/EShoppingTutorial.Core/EShoppingTutorial.Core.Domain.csproj", "EShoppingTutorial/EShoppingTutorial.Core/"]
COPY ["Framework.Core/GenericRepository/GenericRepository.Abstractions.csproj", "Framework.Core/GenericRepository/"]
COPY ["Framework.Core/SharedKernel/SharedKernel.csproj", "Framework.Core/SharedKernel/"]
COPY ["EShoppingTutorial/EShoppingTutorial.Core.Persistence/EShoppingTutorial.Core.Persistence.csproj", "EShoppingTutorial/EShoppingTutorial.Core.Persistence/"]
COPY ["Framework.Core/GenericRepositoryEntityFramework/GenericRepository.EntityFramework.csproj", "Framework.Core/GenericRepositoryEntityFramework/"]
COPY ["EShoppingTutorial/EShoppingTutorial.Infrastructure/EShoppingTutorial.Infrastructure.csproj", "EShoppingTutorial/EShoppingTutorial.Infrastructure/"]
RUN dotnet restore "./EShoppingTutorialWebAPI/EShoppingTutorialWebAPI.csproj"
COPY . .
WORKDIR "/src/EShoppingTutorialWebAPI"
RUN dotnet build "./EShoppingTutorialWebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EShoppingTutorialWebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShoppingTutorialWebAPI.dll"]