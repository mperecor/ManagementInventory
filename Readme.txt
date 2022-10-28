- Instrucciones para poner en marcha la App

Hacer doble click sobre el fichero con nombre Managementinventory.sln para que se abra el Visual Studio pinchas sobre el botón que pone ManagementInventory para que se ejecute, 
esto compilará el proyecto y abrirá un navegador con el swagger para ejecutar los endpoint.

El diseño de la aplicación está realizado con el standard DDD tal y cómo se pide en el requerimiento, está separado por capas tal y cómo indica el standard, siendo la capa de 
presentación la del API donde se ofrecen las distintas operaciones que se pueden realizar, después está la capa CORE que es donde están la parte de aplicación y de dominio.
La parte de aplicación es donde se realizan las diferentes operaciones que se invocan desde la capa del API, en esta capa es donde está los contratos (interfaces) de la capa
de infraestructura (se explicará a continuación), los comportamientos que tendrá la App, las características donde se hacen las operaciones y todo lo referente a las necesidades
de esta capa cómo los mapeos (realizados con la dll de AutoMapper) y su utilidades. Esta capa tiene como dependencia del proyecto al de Dominio que es donde están las 
entidades base, es decir, las tablas de la base de datoscon la misma estructura. Este último proyecto no tiene ninguna dependencia de ningún otro.
Después de esto está la carpeta de infrastructure que es donde está el proyecto de infraestructura que es desde donde se accede a base de datos, donde se implementan los contratos
de la capa de Application. En esta última capa es donde se crea el modelo de datos.

Razones de uso de paquetes nugets de terceros.
- Proyecto de API
	- Swashbuckle.AspNetCore, dll para poder montar el swagger del API

- Proyecto de Application
	- AutoMapper, dll para realizar el mapeo de una clase origen a una destino
	- AutoMapper.Extensions.Microsoft.DependencyInjection, dll para poder hacer la inyección de dependencias de la dll de AutoMapper
	- FluentValidation, dll para realizar la validación de los objetos de entrada a los distintos handler antes de la llegada al mismo
	- FluentValidation.DependencyInjectionExtensions, dll para poder hacer la inyección de dependencias de la dll de FluentValidation
	- MediatR.Extensions.Micrososft.DependencyInjection, dll para poder hacer la inyección de dependecia de la dll de MediatR
	- Microsoft.AspNetCore.Mvc.Abstractions, invoación de los filtros de acción
	- Microsoft.AspNetCore.Mvc.Core, se usa para los tipos de resultados de las acciones comunes de toda la aplicación, excepciones HttpResponseException
	- Microsoft.Extensions.Http,
	- Microsoft.Extensions.Options.ConigurationExtensions, se usa para poder obtener la configuración del fichero de configuración cómo appsettings.json

- Proyecto de Infrastructure
	- Microsoft.EntityFrameworkCore, se usa cómo mapeo de la base de datos
	- Microsoft.EntityFramewrokCore.InMemeory, se usa para tener una base de datos en memoria
	- Microsoft.EntityFrameworkCore.Tools, se usa para que pinte en la consola las instrucciones que se lanzan sobre la base de datos
	- Microsoft.Extensions.Options.ConfigurationExtensions, se usa para pasar la configuración del contexto a DbContext

Puntos no realizados:

 - En este caso son todos opcionales:
	- Securización de API, no lo he montado porque considero que hay poco tiempo para montar toda esta estructura
	- Implementar FrontEnd, porque no tengo experiencia en estos Frameworks de typescript tampoco en Angular, lo he usado en casa pero no a modo profesional.