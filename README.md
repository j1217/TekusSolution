# Web API para Gestión de Proveedores y Servicios – TEKUS

## Introducción
El proyecto **Web API para Gestión de Proveedores y Servicios – TEKUS** fue desarrollado en **Visual Studio 2022** utilizando el framework **.NET 8.0**.  
Está diseñado como solución a una prueba técnica de TEKUS S.A.S. con el objetivo de construir una API RESTful profesional que permita administrar proveedores, servicios y su relación con países.  
Incluye autenticación, carga automática de datos, reportes resumidos, validaciones y una estructura basada en **Clean Architecture** y **DDD (Domain-Driven Design)**. La base de datos utilizada es **SQL Server**.

## Objetivos del Proyecto
Permitir la administración de proveedores y los servicios que ofrecen en diferentes países, con soporte para autenticación JWT, campos personalizados, reportes resumidos y consumo de APIs externas.

## Tecnologías principales

| Tecnología        | Uso                                                       |
|------------------|------------------------------------------------------------|
| .NET 8           | Backend y WebAPI principal                                 |
| EF Core 8        | ORM Code First, persistencia, migraciones                  |
| JWT              | Autenticación segura                                       |
| FluentValidation | Validación de DTOs y entrada de datos                      |
| AutoMapper       | Mapeo limpio entre entidades y DTOs                        |
| SQL Server       | Base de datos relacional                                   |
| Swagger          | Documentación y pruebas del API                            |

## Arquitectura y Buenas Prácticas

### Clean Architecture
El código está organizado en capas bien definidas, con separación de responsabilidades y sin dependencias cruzadas:

- `/Tekus.API` → Controladores (entrada del sistema)
- `/Tekus.Application` → Servicios de aplicación, DTOs, validaciones, lógica de orquestación
- `/Tekus.Domain` → Entidades del dominio, interfaces, lógica de negocio pura
- `/Tekus.Infrastructure` → EF Core, persistencia, autenticación, consumo de APIs externas

### Domain-Driven Design (DDD)
- Entidades: `Provider`, `Service`, `Country`, `ProviderCustomField`
- Value Object: `Email`
- Interfaces: para repositorios y servicios
- Servicios de aplicación que median entre controladores y lógica de dominio

### Inyección de dependencias
Se utilizó `Microsoft.Extensions.DependencyInjection`.  
Servicios, repositorios y validadores están registrados mediante extensiones limpias:

```csharp
builder.Services.AddProjectServices(configuration);
builder.Services.AddApplicationServices();
```

### Bajo acoplamiento / Alta cohesión
- Cada clase cumple una única responsabilidad (**SRP - SOLID**)
- Dependencias inyectadas por constructor
- Separación clara entre lógica de dominio, aplicación y presentación

## Funcionalidades implementadas

### Proveedores
- Crear, editar, eliminar, listar con búsqueda y paginación
- Campos: `Id`, `NIT`, `Nombre`, `Email`
- Soporte para campos personalizados dinámicos (ej: "contacto en Marte")

### Servicios
- Asociados a proveedores y países
- Campos: `Id`, `Nombre`, `Valor por hora (USD)`, `ProviderId`, `CountryId`

### Países
- Se consumen en tiempo real desde `https://restcountries.com/v3.1/all`
- Guardados en base de datos automáticamente al iniciar la app

### Reportes
- Endpoint: `GET /api/report/summary`
- Devuelve:
  - Cantidad de servicios por país
  - Cantidad de proveedores por país (según los servicios registrados)

### Autenticación JWT
- Basado en tokens JWT
- No requiere sistema de usuarios (se parte de un usuario simulado)
- Se debe usar el token en el header: `Authorization: Bearer <token>`

## Datos semilla automáticos

El proyecto ejecuta automáticamente `SeedData.cs` al iniciar:

- ✔ Inserta 5 países desde la API externa
- ✔ 10 proveedores con NIT, email y campos personalizados
- ✔ 10 servicios distribuidos entre proveedores y países

> No se necesita ejecutar scripts SQL manualmente.

## Instalación y ejecución

Clonar el repositorio:
```bash
git clone https://github.com/usuario/tekus-prueba.git
cd tekus-prueba
```

Configurar cadena de conexión en `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TekusDb;Trusted_Connection=True;"
}
```

Crear la base de datos y aplicar migraciones:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Ejecutar la aplicación:
```bash
dotnet run
```

Navegar a Swagger para probar el API:
```
https://localhost:5001/swagger
```

## Diagrama de base de datos

Generado automáticamente desde el modelo Code First utilizando **EF Core Power Tools**.  
Incluye:

- Relaciones 1:N entre `Provider ↔ Service`, `Provider ↔ CustomFields`, `Service ↔ Country`
- Claves primarias y foráneas correctamente definidas  
📎 Diagrama incluido como imagen PNG en el repositorio.

## Testing (Bonus)

Si se dispone de tiempo adicional:

- Se recomienda pruebas unitarias en `ProvidersService` usando `xUnit` y `Moq`
- Pruebas de integración sobre `ApplicationDbContext` con `InMemoryDb`
- Pruebas de carga sobre `/api/report/summary`

## Commits y versionamiento

- Proyecto versionado desde el inicio con Git
- Commits organizados por hitos:
  - `feat: implementación de ProviderCustomField`
  - `refactor: separación de servicios`
  - `docs: documentación de DTOs`
  - `fix: validaciones NIT`

## Entrega

- 📧 Enviado a: `jaime.marin@tekus.co`
- 📁 Contenido:
  - Código fuente ejecutable
  - Migraciones
  - Diagrama de base de datos
  - Documentación
  - Seed data
- 🔐 Repositorio privado o público según preferencia

