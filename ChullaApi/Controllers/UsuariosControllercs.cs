using ChullaApi.Models;
using ChullaApi.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ChullaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChullaAPIController : ControllerBase
    {
        private readonly UsuarioRepository _repository;

        public ChullaAPIController(UsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var usuarios = _repository.GetAllUsuarios();
            return Ok(usuarios); 
        }

        [HttpGet("{id}")]
        public IActionResult GetUsuario(int id)
        {
            var usuario = _repository.GetUsuarioById(id);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult CreateUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("El usuario no puede ser nulo.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.AddNewUsuario(usuario);

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, [FromBody] Usuario usuarioActualizado)
        {
            if (usuarioActualizado == null || usuarioActualizado.Id != id)
            {
                return BadRequest("Datos del usuario inválidos.");
            }

            var usuarioExistente = _repository.GetUsuarioById(id);
            if (usuarioExistente == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            usuarioExistente.Nombre = usuarioActualizado.Nombre;
            usuarioExistente.Email = usuarioActualizado.Email;
            usuarioExistente.EsAdmin = usuarioActualizado.EsAdmin;

            _repository.UpdateUsuario(usuarioExistente);

            return Ok("El usuario se actualizó correctamente.");
        }

        [HttpPatch("{id}")]
        public IActionResult PatchUsuario(int id, [FromBody] Usuario usuarioActualizado)
        {
            if (usuarioActualizado == null)
            {
                return BadRequest("Datos del usuario inválidos.");
            }

            var usuarioExistente = _repository.GetUsuarioById(id);
            if (usuarioExistente == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            if (!string.IsNullOrEmpty(usuarioActualizado.Nombre))
            {
                usuarioExistente.Nombre = usuarioActualizado.Nombre;
            }

            if (!string.IsNullOrEmpty(usuarioActualizado.Email))
            {
                usuarioExistente.Email = usuarioActualizado.Email;
            }

            usuarioExistente.EsAdmin = usuarioActualizado.EsAdmin;

            _repository.UpdateUsuario(usuarioExistente);

            return Ok("El usuario se actualizó parcialmente.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            var usuario = _repository.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            _repository.DeleteUsuario(id);

            return Ok("El usuario se eliminó correctamente.");
        }
    }
}
