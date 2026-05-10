FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем все файлы проекта
COPY . ./

# Восстанавливаем зависимости
RUN dotnet restore DiceSimulator.csproj

# Публикуем приложение
RUN dotnet publish DiceSimulator.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Копируем опубликованные файлы
COPY --from=build /app/publish ./

# Настраиваем переменные окружения
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Открываем порт
EXPOSE 80

# Запускаем приложение
ENTRYPOINT ["dotnet", "DiceSimulator.dll"]