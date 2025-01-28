using ChullaApi.Models;
using ChullaApi.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ChullaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngresosController : ControllerBase
    {
        private readonly IngresoRepository _repository;

        public IngresosController(IngresoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetIngresos()
        {
            var ingresos = _repository.GetAllIngresos();
            return Ok(ingresos); 
        }

        [HttpGet("{id}")]
        public IActionResult GetIngreso(int id)
        {
            var ingreso = _repository.GetIngresoById(id);

            if (ingreso == null)
            {
                return NotFound("Ingreso no encontrado.");
            }

            return Ok(ingreso);
        }

        [HttpPost]
        public IActionResult CreateIngreso([FromBody] Ingreso ingreso)
        {
            if (ingreso == null)
            {
                return BadRequest("El ingreso no puede ser nulo.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.AddNewIngreso(ingreso);

            return CreatedAtAction(nameof(GetIngreso), new { id = ingreso.Id }, ingreso);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateIngreso(int id, [FromBody] Ingreso ingresoActualizado)
        {
            if (ingresoActualizado == null || ingresoActualizado.Id != id)
            {
                return BadRequest("Datos del ingreso inválidos.");
            }

            var ingresoExistente = _repository.GetIngresoById(id);
            if (ingresoExistente == null)
            {
                return NotFound("Ingreso no encontrado.");
            }

            ingresoExistente.UsuarioId = ingresoActualizado.UsuarioId;
            ingresoExistente.FechaIngreso = ingresoActualizado.FechaIngreso;

            _repository.UpdateIngreso(ingresoExistente);

            return Ok("El ingreso se actualizó correctamente.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteIngreso(int id)
        {
            var ingreso = _repository.GetIngresoById(id);
            if (ingreso == null)
            {
                return NotFound("Ingreso no encontrado.");
            }

            _repository.DeleteIngreso(id);

            return Ok("El ingreso se eliminó correctamente.");
        }
    }
}