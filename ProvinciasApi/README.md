
## Aclaraciones

Proyecto NET 5 que expone dos endpoints, uno de logeo y otro para retornar informacion de las provincias.

### Se utilizó

* Inyección de dependencias.
* Test unitarios.
* Logs en archivos con NLOG.
* Swagger.
* Entity Framework Core

## Primeros pasos

Para poder correrlo local primero se debe actualizar los paquetes de nuget ya que tiene dependencias necesarias, ademas de tener que correr el comando `update-database` en la consola para poder generar la base de datos.

### Configuración

* La conexión a la base de datos puede modificarse en el archivo `appsettings.json`.
* La ubicacion donde se guardan los archivos de logeo se encuentran en el archivo `nlog.config` (por defecto se crean en la carpeta bin/net5/logs/)
* Por defecto se crea un usuario de prueba, con username: test y password: 123456

## Uso

Una vez levantada la api, se puede usar postman o el mismo swagger para testear los endpoints
* /User/Login : metodo post el cual se le debe enviar en el body la siguiente estructura
`
{
  "username": "string",
  "password": "string"
} 
` y de ser correcto, retornara una estructura basica de usuario junto con un token

* /Province : metodo get que devuelve la latitud y longitud de una provincia que se le envia por query (string). Si o si necesita ser enviado el bearer token generado por el login anteriormente, sino devolvera el error 401. Para poder testearlo en swagger, utilize el boton "Authorize" y ponga el token generado iniciando con la palabra Bearer.


