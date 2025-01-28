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

        [HttpGet]
        public IActionResult GetPedidos()
        {
            var pedidos = _repository.GetAllPedidos();
            return Ok(pedidos);
        }

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

            pedido.FechaPedido = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            _repository.AddNewPedido(pedido);

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido);
        }

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

            pedidoExistente.UsuarioId = pedidoActualizado.UsuarioId;
            pedidoExistente.FechaPedido = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); 
            pedidoExistente.Total = pedidoActualizado.Total;

            _repository.UpdatePedido(pedidoExistente);

            return Ok("El pedido se actualizó correctamente.");
        }

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