# Proyecto WebAppAutos

Este proyecto es una aplicación basada en .NET y Docker. A continuación, se detallan las instrucciones para construir y ejecutar el entorno de desarrollo usando Docker Compose, y para aplicar las migraciones de base de datos manualmente.

## Prerrequisitos

1. Tener instalado Docker y Docker Compose.
2. Tener instalados los siguientes componentes en el sistema:
   - .NET SDK 8.0 o superior.
   - Entity Framework Core CLI (si no está instalado, puedes hacerlo ejecutando el siguiente comando):
     ```bash
     dotnet tool install --global dotnet-ef
     ```

## Instrucciones

### 1. Construir y levantar los contenedores

Ejecuta el siguiente comando desde la raíz del proyecto para construir las imágenes y levantar los servicios:

```bash
docker-compose up --build
```

Este comando:
- Construirá la imagen Docker para el proyecto.
- Levantará los servicios definidos en el archivo `docker-compose.yml`.

### 2. Aplicar las migraciones de base de datos

Una vez que los contenedores estén en funcionamiento, las migraciones de la base de datos deben ejecutarse manualmente. Sigue estos pasos:

1. Abre una terminal en la raíz del proyecto.
2. Ejecuta el siguiente comando para aplicar las migraciones:
   ```bash
   dotnet ef database update
   ```

Este comando aplicará todas las migraciones pendientes a la base de datos configurada en el proyecto.

### 3. Verificar la aplicación

Despúes de aplicar las migraciones, puedes acceder a la aplicación desde tu navegador en la siguiente URL:

```
http://localhost:8080/MarcasAutos
```

## Estructura del proyecto

- **WebAppAutos/**: Contiene el código fuente de la aplicación.
- **docker-compose.yml**: Archivo de configuración para Docker Compose.
- **Dockerfile**: Archivo para construir la imagen Docker de la aplicación.
