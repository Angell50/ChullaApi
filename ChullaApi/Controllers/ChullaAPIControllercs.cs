using ChullaApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChullaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChullaAPIController : ControllerBase
    {
        // Lista en memoria para almacenar los usuarios
        private static List<Usuario> _usuarios = new List<Usuario>
        {
            new Usuario { Id = 1, Nombre = "Juan Perez", Email = "juan.perez@example.com", EsAdmin = false },
            new Usuario { Id = 2, Nombre = "Ana Gomez", Email = "ana.gomez@example.com", EsAdmin = true },
            new Usuario { Id = 3, Nombre = "Carlos Lopez", Email = "carlos.lopez@example.com", EsAdmin = false }
        };

        // Método GET para devolver datos quemados
        [HttpGet]
        public IActionResult GetUsuarios()
        {
            return Ok(_usuarios); // Devuelve un código HTTP 200 con el JSON
        }

        // Método GET para devolver un usuario por ID
        [HttpGet("{id}")]
        public IActionResult GetUsuario(int id)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
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

            // Asigna un nuevo ID al usuario
            usuario.Id = _usuarios.Max(u => u.Id) + 1;
            _usuarios.Add(usuario); // Agrega el usuario a la lista en memoria

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

            var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Nombre = usuarioActualizado.Nombre;
            usuario.Email = usuarioActualizado.Email;
            usuario.EsAdmin = usuarioActualizado.EsAdmin;

            return Ok("El cambio se realizó correctamente."); // Devuelve un código HTTP 200 con un mensaje de éxito
        }

        // Método PATCH para actualizar parcialmente un usuario
        // Método PATCH para actualizar parcialmente un usuario
        [HttpPatch("{id}")]
        public IActionResult PatchUsuario(int id, [FromBody] Usuario usuarioActualizado)
        {
            if (usuarioActualizado == null)
            {
                return BadRequest("Datos del usuario inválidos.");
            }

            var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            if (usuarioActualizado.Nombre != null)
            {
                usuario.Nombre = usuarioActualizado.Nombre;
            }
            if (usuarioActualizado.Email != null)
            {
                usuario.Email = usuarioActualizado.Email;
            }
            usuario.EsAdmin = usuarioActualizado.EsAdmin;

            return Ok("El cambio se realizó correctamente."); // Devuelve un código HTTP 200 con un mensaje de éxito
        }

        // Método DELETE para eliminar un usuario
        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            _usuarios.Remove(usuario);

            return Ok("El usuario se eliminó correctamente."); // Devuelve un código HTTP 200 con un mensaje de éxito
        }
    }
}