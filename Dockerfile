# === ETAPA 1: Construir Frontend (React/Vite) ===
FROM node:20-alpine AS build-frontend
WORKDIR /app

# Copiar configuración de dependencias primero para aprovechar caché
COPY frontend/package*.json ./frontend/
WORKDIR /app/frontend
# Evita problemas purgados de package-lock con dependencias exactas
RUN npm install

# Copiar todo el frontend y transpilamos
# Gracias a tu modificacion en package.json, Vite exportará esto a /app/Backend/wwwroot
COPY frontend/ ./
RUN npm run build

# === ETAPA 2: Construir Backend (.NET 8) ===
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-backend
WORKDIR /app

COPY Backend/*.csproj ./Backend/
RUN dotnet restore ./Backend/Backend.csproj

COPY Backend/ ./Backend/
WORKDIR /app/Backend
RUN dotnet publish Backend.csproj -c Release -o /app/publish /p:UseAppHost=false

# === ETAPA 3: Imagen final ligera (Producción en Render) ===
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copiamos binarios compilados de .NET
COPY --from=build-backend /app/publish .
# Copiamos la carpeta wwwroot inyectada desde Vite
COPY --from=build-frontend /app/Backend/wwwroot ./wwwroot

# Configuración de red para Render.com (usa puertos dinámicos o 10000/8080)
# Ajustamos ASP.NET 8 para priorizar el puerto 8080 bajo la variable contenedora
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Backend.dll"]
