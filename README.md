# c-sharp-api-practice
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/bf5ea9b857f5431384831ef1f06b93e7)](https://app.codacy.com/app/Bayke96/c-sharp-api-practice?utm_source=github.com&utm_medium=referral&utm_content=Bayke96/c-sharp-api-practice&utm_campaign=Badge_Grade_Dashboard)

API de practica creada bajo la arquitectura REST

- Herramientas utilizadas:
	- ASP.NET MVC para aplicar la arquitectura MVC.
	- C# para la codificacion del backend.
	- Entity Framework 6 ( Code First ) para el modelado de datos y servicios.
	- JSON.NET para manejar objetos JSON.
	- Microsoft SQL Server para la base de datos.

- API Endpoints:
	- Clientes:

		GET:	/clientes - Retorna los datos de todos los clientes.

		GET:	/clientes/{id} - Retorna los datos de un cliente.

		POST:	/clientes - Crea un nuevo cliente.

		PUT:	/clientes/{id} - Modifica un cliente.

		DELETE:	/clientes/{id} - Eliminar un cliente
		
	- Productos

		GET:	/productos - Retorna los datos de todos los productos.

		GET:	/productos/{id} - Retorna los datos de un producto.

		POST:	/productos- Crea un nuevo producto.

		PUT:	/productos/{id} - Modifica un producto.

		DELETE:	/productos/{id} - Eliminar un producto.
		
	- Facturas


		GET:	/facturas/{id} - Retorna los datos de una factura.

		POST:	/facturas - Crea una nueva factura.

		DELETE:	/facturas/{id} - Elimina una factura.
