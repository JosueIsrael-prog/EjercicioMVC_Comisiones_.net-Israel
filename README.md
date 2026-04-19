# 🚀 Ejercicio MVC .NET 8 + React 18: Cálculo de Comisiones

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)
![React](https://img.shields.io/badge/React-18.2.0-61DAFB?style=for-the-badge&logo=react&logoColor=black)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Supabase-336791?style=for-the-badge&logo=postgresql)
![Docker](https://img.shields.io/badge/Docker-Multietapa-2496ED?style=for-the-badge&logo=docker)

Bienvenido a la resolución completa del ejercicio técnico de cálculo de comisiones. El sistema consiste en un backend sólido en **.NET 8** y una Single Page Application (*SPA*) en **React**, completamente integrados bajo una única compilación a través de **Docker** y desplegados en *Render*.

🔗 **Producción (En vivo):** [https://ejercicio-mvc-comisiones-israel.onrender.com](https://ejercicio-mvc-comisiones-israel.onrender.com)

---

## 🏗️ Arquitectura y Tecnologías

1. **Backend (.NET 8):** Capa transaccional que utiliza *Entity Framework Core* para gestionar entidades tabulares y relaciones. Exposición de API REST con enrutamiento híbrido para alojar y servir dinámicamente los estáticos compilados de React en la carpeta \`/wwwroot\`.
2. **Base de Datos (Supabase):** Integración nativa con base de datos PostgreSQL utilizando \`Npgsql\` y configuración *Legacy Timezone* para evitar inconsistencias de fechas `UTC`.
3. **Frontend (React 18):** Interfaz mínima construida con `Vite`. Consume el endpoint transaccional empleando ruteo relativo para la interoperabilidad total (CORS universal resuelto y enrutamiento *fallback* activado).

---

## 🏃‍♂️ Ejecución en Entorno de Desarrollo (Local)

Para correr el proyecto en modo local de forma segregada, sigue estos pasos:

### 1. Variables de entorno (Backend)
Debes configurar tu cadena de conexión en el archivo `appsettings.json` o establecer explícitamente la variable de entorno `SupabaseConnection`.

### 2. Ejecutar la Base de Datos (.NET)
Sitúate en el directorio raiz y ejecuta:
```bash
cd Backend
dotnet run
```

### 3. Ejecutar la Visor (React)
Abre otra instancia de tu terminal y ejecuta:
```bash
cd frontend
npm install
npm run dev
```

---

## 🌍 Despliegue en Producción (Render)

Este repositorio contiene un bloque `Dockerfile` multietapa estandarizado para despliegue PaaS sin fricciones. 
La imagen primero resuelve las dependencias de Node, transpila la solución de React y luego inyecta todo el código *client-side* en los publicables generados de Kestrel.
El entorno ya está configurado para exponerse globalmente en los puertos TCP inyectados (por defecto `8080`).
