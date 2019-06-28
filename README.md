# c-sharp-api-practice
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/bf5ea9b857f5431384831ef1f06b93e7)](https://app.codacy.com/app/Bayke96/c-sharp-api-practice?utm_source=github.com&utm_medium=referral&utm_content=Bayke96/c-sharp-api-practice&utm_campaign=Badge_Grade_Dashboard)

APi de practica creada bajo la arquitectura REST

- Herramientas utilizadas:
	ASP.NET MVC
	C# como backend, 
	Entity Framework 6 ( Code First )
	JSON.NET
	Microsoft SQL Server

- API Endpoints:

GET:	/clientes - Retorna los datos de todos los clientes.
GET:	/clientes/{id} - Retorna los datos de un cliente.
POST:	/clientes - Crea un nuevo cliente.
PUT:	/clientes/{id} - Modifica un cliente.
DELETE:	/clientes/{id} - Eliminar un cliente

GET:	/productos - Retorna los datos de todos los productos.
GET:	/productos/{id} - Retorna los datos de un productos.
POST:	/productos- Crea un nuevo productos.
PUT:	/productos/{id} - Modifica un productos.
DELETE:	/productos/{id} - Eliminar un productos.

GET:	/facturas/{id} - Retorna los datos de una factura.
POST:	/facturas - Crea una nueva factura.
DELETE:	/facturas/{id} - Elimina una factura.