using SQLite;
using ChullaApi.Models;
using System.Collections.Generic;

namespace ChullaApi.Data
{
    public class UsuarioRepository
    {
        private readonly SQLiteConnection _connection;

        public UsuarioRepository(string dbPath)
        {
            // Inicializa la conexión con SQLite
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Usuario>();
        }

        // Método para agregar un nuevo usuario
        public void AddNewUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                _connection.Insert(usuario);
            }
        }

        // Método para obtener todos los usuarios
        public List<Usuario> GetAllUsuarios()
        {
            return _connection.Table<Usuario>().ToList();
        }

        // Método para obtener un usuario por ID
        public Usuario GetUsuarioById(int id)
        {
            return _connection.Find<Usuario>(id);
        }

        // Método para actualizar un usuario existente
        public void UpdateUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                _connection.Update(usuario);
            }
        }

        // Método para eliminar un usuario por ID
        public void DeleteUsuario(int id)
        {
            var usuario = GetUsuarioById(id);
            if (usuario != null)
            {
                _connection.Delete(usuario);
            }
        }
    }
}
