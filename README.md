# PruebaTecnica-SupBan
# Proyecto Web API con .NET 8  

Este proyecto es una Web API desarrollada utilizando el siguiente stack tecnológico y un conjunto de patrones y librerías para una arquitectura robusta y mantenible.  

---  

## Tecnologías y Herramientas  

- **.NET 8** - Plataforma principal del desarrollo Web API.  
- **Entity Framework Core** - ORM utilizado con la técnica Code First para la creación y gestión de la base de datos.  
- **Middleware personalizado**:  
  - Manejo centralizado de errores.  
  - Logging del comportamiento de las llamadas a los handlers.  
- **Arquitectura basada en**:  
  - Código limpio (Clean Architecture).  
  - Patrón CQRS (Command Query Responsibility Segregation).  
- **FluentValidation** - Librería para validar archivos subidos.  
- **JWT (JSON Web Token)** - Generación de tokens para la seguridad y autenticación.  
- **MediatR** - Implementación del patrón Mediator para desacoplar la lógica de negocio.  
- **Patrón Repositorio Genérico** - Para manejar operaciones de datos de forma genérica.  
- **Inyección de Dependencias** - Gestión flexible y desacoplada de dependencias.  
- **Swagger (Swashbuckle)** - Documentación automática de la API con UI y soporte XML.  
- **Pruebas Unitarias** - Implementadas con NUnit, para garantizar la calidad del código.  

---  

## Requisitos Previos  

Para poder correr el proyecto correctamente, es necesario tener instalado:  

- [.NET 8 SDK](https://dotnet.microsoft.com/download)  
- Visual Studio 2022 o superior (u otro IDE compatible)  
- Herramienta global de EF Core para migraciones y gestión de base de datos  

---  

## Instalación y Configuración  

1. **Instalar la herramienta de Entity Framework Core globalmente::**  

```powershell  
PM> dotnet tool install --global dotnet-ef  

```

2. **Actualizar y crear la base de datos local a partir de las migraciones ya existentes::**  

```powershell  
PM> dotnet ef database update

```

## Uso
Ejecuta la aplicación desde Visual Studio o mediante el comando dotnet run.
Accede a la documentación de la API vía Swagger UI en: https://localhost:####/swagger (#### validar el puerto configurado en su Visual Studio).

# Para Generar el token de acceso se debe de consumir el siguiente Endpoint con el siguiente objeto
## Request
```json
POST: /api/auth/login
{
  "username":"prueba",
  "password":"tecnica"
}
```
## Response 
```json

{
"success": true,
"data": {
"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicHJ1ZWJhIiwiZXhwIjoxNzQ1NDUyODM1LCJpc3MiOiJwcnVlYmFUZWNuaWNhU0kqMDFAIyQlNTE1MTUxNTE0NFdXNSJ9.CCoBby55PGJZJjXAtGT7OT4JTLN1mFfwgMpp_urrtQI",
"expiration": "2025-04-23T20:00:35.2101532-04:00"
},
"errorMessage": null
}
