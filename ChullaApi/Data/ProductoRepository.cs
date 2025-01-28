using SQLite;
using ChullaApi.Models;
using System.Collections.Generic;

namespace ChullaApi.Data
{
    public class ProductoRepository
    {
        private readonly SQLiteConnection _connection;

        public ProductoRepository(string dbPath)
        {
            // Inicializa la conexión con SQLite
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Producto>();
        }

        // Método para agregar un nuevo producto
        public void AddNewProducto(Producto producto)
        {
            if (producto != null)
            {
                _connection.Insert(producto);
            }
        }

        // Método para obtener todos los productos
        public List<Producto> GetAllProductos()
        {
            return _connection.Table<Producto>().ToList();
        }

        // Método para obtener un producto por ID
        public Producto GetProductoById(int id)
        {
            return _connection.Find<Producto>(id);
        }

        // Método para actualizar un producto existente
        public void UpdateProducto(Producto producto)
        {
            if (producto != null)
            {
                _connection.Update(producto);
            }
        }

        // Método para eliminar un producto por ID
        public void DeleteProducto(int id)
        {
            var producto = GetProductoById(id);
            if (producto != null)
            {
                _connection.Delete(producto);
            }
        }
    }
}