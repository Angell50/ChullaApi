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
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Producto>();
        }

        public void AddNewProducto(Producto producto)
        {
            if (producto != null)
            {
                _connection.Insert(producto);
            }
        }

        public List<Producto> GetAllProductos()
        {
            return _connection.Table<Producto>().ToList();
        }

        public Producto GetProductoById(int id)
        {
            return _connection.Find<Producto>(id);
        }

        public void UpdateProducto(Producto producto)
        {
            if (producto != null)
            {
                _connection.Update(producto);
            }
        }

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