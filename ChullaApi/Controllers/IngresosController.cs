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

        // Método GET para devolver todos los ingresos
        [HttpGet]
        public IActionResult GetIngresos()
        {
            var ingresos = _repository.GetAllIngresos();
            return Ok(ingresos); // Devuelve un código HTTP 200 con el JSON
        }

        // Método GET para devolver un ingreso por ID
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

        // Método POST para crear un ingreso
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

            // Devuelve un código HTTP 201 con la información del ingreso creado
            return CreatedAtAction(nameof(GetIngreso), new { id = ingreso.Id }, ingreso);
        }

        // Método PUT para actualizar un ingreso
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

            // Actualizar los datos del ingreso existente
            ingresoExistente.UsuarioId = ingresoActualizado.UsuarioId;
            ingresoExistente.FechaIngreso = ingresoActualizado.FechaIngreso;

            _repository.UpdateIngreso(ingresoExistente);

            return Ok("El ingreso se actualizó correctamente.");
        }

        // Método DELETE para eliminar un ingreso
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