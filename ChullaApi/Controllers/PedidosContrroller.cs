using ChullaApi.Models;
using ChullaApi.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ChullaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly PedidoRepository _repository;

        public PedidosController(PedidoRepository repository)
        {
            _repository = repository;
        }

        // Método GET para devolver todos los pedidos
        [HttpGet]
        public IActionResult GetPedidos()
        {
            var pedidos = _repository.GetAllPedidos();
            return Ok(pedidos); // Devuelve un código HTTP 200 con el JSON
        }

        // Método GET para devolver un pedido por ID
        [HttpGet("{id}")]
        public IActionResult GetPedido(int id)
        {
            var pedido = _repository.GetPedidoById(id);

            if (pedido == null)
            {
                return NotFound("Pedido no encontrado.");
            }

            return Ok(pedido);
        }

        // Método POST para crear un pedido
        [HttpPost]
        public IActionResult CreatePedido([FromBody] Pedido pedido)
        {
            if (pedido == null)
            {
                return BadRequest("El pedido no puede ser nulo.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Asignar la fecha actual a FechaPedido
            pedido.FechaPedido = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            _repository.AddNewPedido(pedido);

            // Devuelve un código HTTP 201 con la información del pedido creado
            return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido);
        }

        // Método PUT para actualizar un pedido
        [HttpPut("{id}")]
        public IActionResult UpdatePedido(int id, [FromBody] Pedido pedidoActualizado)
        {
            if (pedidoActualizado == null || pedidoActualizado.Id != id)
            {
                return BadRequest("Datos del pedido inválidos.");
            }

            var pedidoExistente = _repository.GetPedidoById(id);
            if (pedidoExistente == null)
            {
                return NotFound("Pedido no encontrado.");
            }

            // Actualizar los datos del pedido existente
            pedidoExistente.UsuarioId = pedidoActualizado.UsuarioId;
            pedidoExistente.FechaPedido = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Asignar la fecha actual
            pedidoExistente.Total = pedidoActualizado.Total;

            _repository.UpdatePedido(pedidoExistente);

            return Ok("El pedido se actualizó correctamente.");
        }

        // Método DELETE para eliminar un pedido
        [HttpDelete("{id}")]
        public IActionResult DeletePedido(int id)
        {
            var pedido = _repository.GetPedidoById(id);
            if (pedido == null)
            {
                return NotFound("Pedido no encontrado.");
            }

            _repository.DeletePedido(id);

            return Ok("El pedido se eliminó correctamente.");
        }
    }
}