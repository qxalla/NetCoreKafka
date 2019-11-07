Requiere tener docker instalado
1) ejecutar "docker-compose up" en carpeta docker
2) iniciar el proyecto LogApi
3) acceder mediante navegador https://localhost:44321/swagger/ para testing


Componentes utilizados:

FluentValidation.AspNetCore: validaciones del modelo
AutoMapper: mapeo de entidades con DTOS.
AutoWrapper: estandar de formato de respuestas.
Dapper (ORM): Acceso a datos (ejemplo de entidad persona).
SQL para crear tabla:
CREATE TABLE [dbo].[Person]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    	[FirstName] NVARCHAR(20) NOT NULL, 
    	[LastName] NVARCHAR(20) NOT NULL, 
    	[DateOfBirth] DATETIME NOT NULL
)
modificar appsettings.json para string de conexion a la BD.

Swashbuckle.AspNetCore: documentacion.
Serilog.AspNetCore: para generar log de ejecucion (queda en carpeta Logs)

en "Domain/DataLogManager.cs" esta implementado la escritura y lectura de kafka 
basado en la ultima versiones de netcore.
