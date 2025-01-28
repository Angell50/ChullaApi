using ChullaApi.Models;
using ChullaApi.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ChullaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoRepository _repository;

        public ProductosController(ProductoRepository repository)
        {
            _repository = repository;
        }

        // Método GET para devolver todos los productos
        [HttpGet]
        public IActionResult GetProductos()
        {
            var productos = _repository.GetAllProductos();
            return Ok(productos); // Devuelve un código HTTP 200 con el JSON
        }

        // Método GET para devolver un producto por ID
        [HttpGet("{id}")]
        public IActionResult GetProducto(int id)
        {
            var producto = _repository.GetProductoById(id);

            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            return Ok(producto);
        }

        // Método POST para crear un producto
        [HttpPost]
        public IActionResult CreateProducto([FromBody] Producto producto)
        {
            if (producto == null)
            {
                return BadRequest("El producto no puede ser nulo.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.AddNewProducto(producto);

            // Devuelve un código HTTP 201 con la información del producto creado
            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        // Método PUT para actualizar un producto
        [HttpPut("{id}")]
        public IActionResult UpdateProducto(int id, [FromBody] Producto productoActualizado)
        {
            if (productoActualizado == null || productoActualizado.Id != id)
            {
                return BadRequest("Datos del producto inválidos.");
            }

            var productoExistente = _repository.GetProductoById(id);
            if (productoExistente == null)
            {
                return NotFound("Producto no encontrado.");
            }

            // Actualizar los datos del producto existente
            productoExistente.Nombre = productoActualizado.Nombre;
            productoExistente.Cantidad = productoActualizado.Cantidad;
            productoExistente.Categoria = productoActualizado.Categoria;
            productoExistente.Precio = productoActualizado.Precio;
            productoExistente.Descripcion = productoActualizado.Descripcion;

            _repository.UpdateProducto(productoExistente);

            return Ok("El producto se actualizó correctamente.");
        }

        // Método PATCH para actualizar parcialmente un producto
        [HttpPatch("{id}")]
        public IActionResult PatchProducto(int id, [FromBody] Producto productoActualizado)
        {
            if (productoActualizado == null)
            {
                return BadRequest("Datos del producto inválidos.");
            }

            var productoExistente = _repository.GetProductoById(id);
            if (productoExistente == null)
            {
                return NotFound("Producto no encontrado.");
            }

            // Actualizar solo los campos proporcionados
            if (!string.IsNullOrEmpty(productoActualizado.Nombre))
            {
                productoExistente.Nombre = productoActualizado.Nombre;
            }

            if (productoActualizado.Cantidad != 0)
            {
                productoExistente.Cantidad = productoActualizado.Cantidad;
            }

            if (!string.IsNullOrEmpty(productoActualizado.Categoria))
            {
                productoExistente.Categoria = productoActualizado.Categoria;
            }

            if (productoActualizado.Precio != 0)
            {
                productoExistente.Precio = productoActualizado.Precio;
            }

            if (!string.IsNullOrEmpty(productoActualizado.Descripcion))
            {
                productoExistente.Descripcion = productoActualizado.Descripcion;
            }

            _repository.UpdateProducto(productoExistente);

            return Ok("El producto se actualizó parcialmente.");
        }

        // Método DELETE para eliminar un producto
        [HttpDelete("{id}")]
        public IActionResult DeleteProducto(int id)
        {
            var producto = _repository.GetProductoById(id);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            _repository.DeleteProducto(id);

            return Ok("El producto se eliminó correctamente.");
        }
    }
}