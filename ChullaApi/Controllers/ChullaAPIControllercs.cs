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

        // Método GET para devolver todos los usuarios
        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var usuarios = _repository.GetAllUsuarios();
            return Ok(usuarios); // Devuelve un código HTTP 200 con el JSON
        }

        // Método GET para devolver un usuario por ID
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

        // Método POST para crear un usuario
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

            // Devuelve un código HTTP 201 con la información del usuario creado
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        // Método PUT para actualizar un usuario
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

            // Actualizar los datos del usuario existente
            usuarioExistente.Nombre = usuarioActualizado.Nombre;
            usuarioExistente.Email = usuarioActualizado.Email;
            usuarioExistente.EsAdmin = usuarioActualizado.EsAdmin;

            _repository.UpdateUsuario(usuarioExistente);

            return Ok("El usuario se actualizó correctamente.");
        }

        // Método PATCH para actualizar parcialmente un usuario
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

            // Actualizar solo los campos proporcionados
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

        // Método DELETE para eliminar un usuario
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
