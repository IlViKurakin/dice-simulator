FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем все файлы
COPY . ./

# Находим csproj файл и восстанавливаем зависимости
RUN csproj_file=$(find . -name "*.csproj" -type f | head -n 1) && \
    dotnet restore "$csproj_file"

# Публикуем приложение
RUN csproj_file=$(find . -name "*.csproj" -type f | head -n 1) && \
    dotnet publish "$csproj_file" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

# Настройка для Render
ENV ASPNETCORE_URLS=http://+:${PORT}
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 80

# Находим имя DLL и запускаем
CMD find . -name "*.dll" -not -name "System*.dll" -not -name "Microsoft*.dll" | head -n 1 | xargs -I {} dotnet {}
