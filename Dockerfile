# Etapa 1: Compilación de Frontend React
FROM node:20-alpine AS build-frontend
WORKDIR /app

# Almacenamiento en caché de dependencias npm
COPY frontend/package*.json ./frontend/
WORKDIR /app/frontend

RUN npm install

# Transpilación de aplicación estática a directorio empaquetado
COPY frontend/ ./
RUN npm run build

# Etapa 2: Compilación y publicación del Backend ASP.NET Core
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-backend
WORKDIR /app

COPY Backend/*.csproj ./Backend/
RUN dotnet restore ./Backend/Backend.csproj

COPY Backend/ ./Backend/
WORKDIR /app/Backend
RUN dotnet publish Backend.csproj -c Release -o /app/publish /p:UseAppHost=false

# Etapa 3: Ensamblaje de imagen optimizada
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Inserción de binarios compilados de .NET
COPY --from=build-backend /app/publish .

# Integración del artefacto frontend SPA compilado hacia /wwwroot
COPY --from=build-frontend /app/Backend/wwwroot ./wwwroot

# Mapeo de puertos para entorno de ejecución en Render
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Backend.dll"]
