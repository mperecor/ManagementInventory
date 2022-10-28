- Instrucciones para poner en marcha la App

Hacer doble click sobre el fichero con nombre Managementinventory.sln para que se abra el Visual Studio pinchas sobre el bot�n que pone ManagementInventory para que se ejecute, 
esto compilar� el proyecto y abrir� un navegador con el swagger para ejecutar los endpoint.

El dise�o de la aplicaci�n est� realizado con el standard DDD tal y c�mo se pide en el requerimiento, est� separado por capas tal y c�mo indica el standard, siendo la capa de 
presentaci�n la del API donde se ofrecen las distintas operaciones que se pueden realizar, despu�s est� la capa CORE que es donde est�n la parte de aplicaci�n y de dominio.
La parte de aplicaci�n es donde se realizan las diferentes operaciones que se invocan desde la capa del API, en esta capa es donde est� los contratos (interfaces) de la capa
de infraestructura (se explicar� a continuaci�n), los comportamientos que tendr� la App, las caracter�sticas donde se hacen las operaciones y todo lo referente a las necesidades
de esta capa c�mo los mapeos (realizados con la dll de AutoMapper) y su utilidades. Esta capa tiene como dependencia del proyecto al de Dominio que es donde est�n las 
entidades base, es decir, las tablas de la base de datoscon la misma estructura. Este �ltimo proyecto no tiene ninguna dependencia de ning�n otro.
Despu�s de esto est� la carpeta de infrastructure que es donde est� el proyecto de infraestructura que es desde donde se accede a base de datos, donde se implementan los contratos
de la capa de Application. En esta �ltima capa es donde se crea el modelo de datos.

Razones de uso de paquetes nugets de terceros.
- Proyecto de API
	- Swashbuckle.AspNetCore, dll para poder montar el swagger del API

- Proyecto de Application
	- AutoMapper, dll para realizar el mapeo de una clase origen a una destino
	- AutoMapper.Extensions.Microsoft.DependencyInjection, dll para poder hacer la inyecci�n de dependencias de la dll de AutoMapper
	- FluentValidation, dll para realizar la validaci�n de los objetos de entrada a los distintos handler antes de la llegada al mismo
	- FluentValidation.DependencyInjectionExtensions, dll para poder hacer la inyecci�n de dependencias de la dll de FluentValidation
	- MediatR.Extensions.Micrososft.DependencyInjection, dll para poder hacer la inyecci�n de dependecia de la dll de MediatR
	- Microsoft.AspNetCore.Mvc.Abstractions, invoaci�n de los filtros de acci�n
	- Microsoft.AspNetCore.Mvc.Core, se usa para los tipos de resultados de las acciones comunes de toda la aplicaci�n, excepciones HttpResponseException
	- Microsoft.Extensions.Http,
	- Microsoft.Extensions.Options.ConigurationExtensions, se usa para poder obtener la configuraci�n del fichero de configuraci�n c�mo appsettings.json

- Proyecto de Infrastructure
	- Microsoft.EntityFrameworkCore, se usa c�mo mapeo de la base de datos
	- Microsoft.EntityFramewrokCore.InMemeory, se usa para tener una base de datos en memoria
	- Microsoft.EntityFrameworkCore.Tools, se usa para que pinte en la consola las instrucciones que se lanzan sobre la base de datos
	- Microsoft.Extensions.Options.ConfigurationExtensions, se usa para pasar la configuraci�n del contexto a DbContext

Puntos no realizados:

 - En este caso son todos opcionales:
	- Securizaci�n de API, no lo he montado porque considero que hay poco tiempo para montar toda esta estructura
	- Implementar FrontEnd, porque no tengo experiencia en estos Frameworks de typescript tampoco en Angular, lo he usado en casa pero no a modo profesional.