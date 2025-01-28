using SQLite;
using ChullaApi.Models;
using System.Collections.Generic;

namespace ChullaApi.Data
{
    public class PedidoRepository
    {
        private readonly SQLiteConnection _connection;

        public PedidoRepository(string dbPath)
        {
            // Inicializa la conexión con SQLite
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Pedido>();
        }

        // Método para agregar un nuevo pedido
        public void AddNewPedido(Pedido pedido)
        {
            if (pedido != null)
            {
                _connection.Insert(pedido);
            }
        }

        // Método para obtener todos los pedidos
        public List<Pedido> GetAllPedidos()
        {
            return _connection.Table<Pedido>().ToList();
        }

        // Método para obtener un pedido por ID
        public Pedido GetPedidoById(int id)
        {
            return _connection.Find<Pedido>(id);
        }

        // Método para actualizar un pedido existente
        public void UpdatePedido(Pedido pedido)
        {
            if (pedido != null)
            {
                _connection.Update(pedido);
            }
        }

        // Método para eliminar un pedido por ID
        public void DeletePedido(int id)
        {
            var pedido = GetPedidoById(id);
            if (pedido != null)
            {
                _connection.Delete(pedido);
            }
        }
    }
}