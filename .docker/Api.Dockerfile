FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/api/YTodo.Api/YTodo.Api.csproj", "src/api/YTodo.Api/"]
COPY ["src/api/YTodo.Application/YTodo.Application.csproj", "src/api/YTodo.Application/"]
COPY ["src/api/YTodo.Persistence/YTodo.Persistence.csproj", "src/api/YTodo.Persistence/"]
RUN dotnet restore "src/api/YTodo.Api/YTodo.Api.csproj"
COPY . .
WORKDIR "/src/src/api/YTodo.Api"
RUN dotnet build "YTodo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "YTodo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "YTodo.Api.dll"]
