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
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Usuario>();
        }

        public void AddNewUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                _connection.Insert(usuario);
            }
        }

        public List<Usuario> GetAllUsuarios()
        {
            return _connection.Table<Usuario>().ToList();
        }

        public Usuario GetUsuarioById(int id)
        {
            return _connection.Find<Usuario>(id);
        }

        public void UpdateUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                _connection.Update(usuario);
            }
        }

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
