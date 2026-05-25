# cobros-automaticos-api

API para procesar cobros automáticos individuales y por lotes (batch).

## Instalación de dependencias.

Proveedor SQL Client.

```bash
dotnet add package Microsoft.Data.SqlClient
```

HASH para password (BCrypt).
```bash
dotnet add package BCrypt.Net-Next
```

Tokens JWT.
```bash
dotnet add package System.IdentityModel.Tokens.Jwt
```
Autenticación JWT Bearer.
```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

Restaurar paquetes.
```bash
dotnet restore
```

## Pasos para ejecutar la API

```python
# 1. Compilar el proyecto
dotnet build

# 2. Ejecutar la API
dotnet run

# 3. Validar la ejecución del proyecto
Ej: http://localhost:5148
```

## Cómo probar los EndPoints
**Nota:** Adicional a estos ejemplos se encuentra una colección completa de postman en la ubicación:

`cobros-automaticos-api/Presentation/Test/CobrosAutomaticosApi.collection.json`

### LogIn

*Obtener Token de acceso y generar sesión.*

* **Método:** `POST`
* **URL:** `http://localhost:5148/api/authentication/LogIn`

**Headers requeridos:**
* `Content-Type`: `application/json`


```json

{
    "UserName": "admin",
    "PassWord": "12345"
}

```
##
### Crear cobro

*Se crear un nuevo cobro vinculado a un cliente y esta listo para ser procesado.*

* **Método:** `POST`
* **URL:** `http://localhost:5148/api/cobro/CrearCobro`

**Headers requeridos:**
* `Authorization`: `Bearer <tokenJWT>`
* `Content-Type`: `application/json`

```json

{
    "ClienteId": 1,
    "Monto": 10.00,
    "Moneda": "QTZ",
    "ReferenciaExterna": "QTZ-2029"
}

```
##
### Procesar cobro individual

*Se procesa un único cobro y se obtiene el resultado del mismo.*

* **Método:** `POST`
* **URL:** `http://localhost:5148/api/cobro/individual/procesar`

**Headers requeridos:**
* `Authorization`: `Bearer <tokenJWT>`
* `Content-Type`: `application/json`

```json

{
    "UsuarioId": "1",
    "CobroId": "2"
}

```

##
### Procesar cobro por lote

*Se procesa un lote de cobros y se obtiene el numero de cobros que fueron procesados exitosamente (PROCESADO).*

* **Método:** `POST`
* **URL:** `http://localhost:5148/api/cobro/lote/procesar`

**Headers requeridos:**
* `Authorization`: `Bearer <tokenJWT>`
* `Content-Type`: `application/json`

```json

{
    "UsuarioId": "1",
    "CobroIds": [1,2,3,4]
}

```

##
### Crear cliente

*Se crea un cliente para vincularlo a los cobros.*

* **Método:** `POST`
* **URL:** `http://localhost:5148/api/cliente/CrearClient`

**Headers requeridos:**
* `Authorization`: `Bearer <tokenJWT>`
* `Content-Type`: `application/json`

```json

{
    "Dpi": "2541893450101",
    "Email": "email@email.com",
    "Nombre": "Nombre del cliente",
    "Telefono": "22222222"
}

```

##
### Listar cobros por cliente

*Se listan todos los cobros que están vinculados a el cliente.*

* **Método:** `GET`
* **URL:** `http://localhost:5148/api/cliente/{idCliente}/cobros`

**Headers requeridos:**
* `Authorization`: `Bearer <tokenJWT>`
* `Content-Type`: `application/json`

##
### Listar cobros por cliente y filtrar por rango de fechas

*Se listan todos los cobros que están vinculados a el cliente según las fechas filtradas.*

* **Método:** `GET`
* **URL:** `http://localhost:5148/api/cliente/idCliente}/cobros?FechaInicio={fechaInicio}&FechaFin={fechaFin}`

**Headers requeridos:**
* `Authorization`: `Bearer <tokenJWT>`
* `Content-Type`: `application/json`

---

## 🛠️ Decisiones Técnicas

* **Arquitectura Relacional:** Se optó por una base de datos relacional `(Microsoft SQL Server)`. A diferencia de las `NoSQL`, el esquema relacional está altamente optimizado para manejar de forma segura operaciones transaccionales a gran escala, como lo son los procesamiento de cobros individuales y por lotes `(idempotente)`.

* **Integridad Transaccional:** Se garantiza la integridad de datos mediante el uso de `COMMIT` y `ROLLBACK` en las transacciones.

* **Desarrollo sin Frameworks ORM:** No se implementó un Framework ORM. Ya que la idea es dar visibilidad de los procesos a nivel de `T-SQL`.

* **Seguridad de contraseñas:** Las contraseñas deben guardarse de manera cifrada a nivel de base de datos, para evitar una afectación al momento de obtenerse de manera no autorizada, por lo cual se decidió utilizar `BCrypt` el cual es altamente recomendado en la industria del desarrollo de software.

* **Autenticación basada en sesiones y token (JWT):** Para el manejo correcto de las sesiones se creo un servicio dedicado para comprobar el estado actual del usuario y de requerirlo generar una sesión que depende de un `tiempo de expiración` y `token de autenticación estándar para la web (JWT)`. Como nota importante se utilizó un `Filter` el cual validad cada petición que se hace a cualquier controlador (Excepto el controlador para el login), con el cual se puede rechazar la petición en caso `no exista una sesión`, `la sesión haya expirado` o bien `el token sea invalido`.

