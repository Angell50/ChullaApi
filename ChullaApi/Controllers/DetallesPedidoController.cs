using ChullaApi.Models;
using ChullaApi.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ChullaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetallesPedidoController : ControllerBase
    {
        private readonly DetallePedidoRepository _repository;

        public DetallesPedidoController(DetallePedidoRepository repository)
        {
            _repository = repository;
        }

        // Método GET para devolver todos los detalles de pedidos
        [HttpGet]
        public IActionResult GetDetallesPedido()
        {
            var detalles = _repository.GetAllDetallesPedido();
            return Ok(detalles); // Devuelve un código HTTP 200 con el JSON
        }

        // Método GET para devolver un detalle de pedido por ID
        [HttpGet("{id}")]
        public IActionResult GetDetallePedido(int id)
        {
            var detalle = _repository.GetDetallePedidoById(id);

            if (detalle == null)
            {
                return NotFound("Detalle de pedido no encontrado.");
            }

            return Ok(detalle);
        }

        // Método POST para crear un detalle de pedido
        [HttpPost]
        public IActionResult CreateDetallePedido([FromBody] DetallePedido detalle)
        {
            if (detalle == null)
            {
                return BadRequest("El detalle de pedido no puede ser nulo.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.AddNewDetallePedido(detalle);

            // Devuelve un código HTTP 201 con la información del detalle de pedido creado
            return CreatedAtAction(nameof(GetDetallePedido), new { id = detalle.Id }, detalle);
        }

        // Método PUT para actualizar un detalle de pedido
        [HttpPut("{id}")]
        public IActionResult UpdateDetallePedido(int id, [FromBody] DetallePedido detalleActualizado)
        {
            if (detalleActualizado == null || detalleActualizado.Id != id)
            {
                return BadRequest("Datos del detalle de pedido inválidos.");
            }

            var detalleExistente = _repository.GetDetallePedidoById(id);
            if (detalleExistente == null)
            {
                return NotFound("Detalle de pedido no encontrado.");
            }

            // Actualizar los datos del detalle de pedido existente
            detalleExistente.PedidoId = detalleActualizado.PedidoId;
            detalleExistente.ProductoId = detalleActualizado.ProductoId;
            detalleExistente.Cantidad = detalleActualizado.Cantidad;
            detalleExistente.Precio = detalleActualizado.Precio;

            _repository.UpdateDetallePedido(detalleExistente);

            return Ok("El detalle de pedido se actualizó correctamente.");
        }

        // Método DELETE para eliminar un detalle de pedido
        [HttpDelete("{id}")]
        public IActionResult DeleteDetallePedido(int id)
        {
            var detalle = _repository.GetDetallePedidoById(id);
            if (detalle == null)
            {
                return NotFound("Detalle de pedido no encontrado.");
            }

            _repository.DeleteDetallePedido(id);

            return Ok("El detalle de pedido se eliminó correctamente.");
        }
    }
}