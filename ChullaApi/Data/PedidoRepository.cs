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
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Pedido>();
        }

        public void AddNewPedido(Pedido pedido)
        {
            if (pedido != null)
            {
                _connection.Insert(pedido);
            }
        }

        public List<Pedido> GetAllPedidos()
        {
            return _connection.Table<Pedido>().ToList();
        }

        public Pedido GetPedidoById(int id)
        {
            return _connection.Find<Pedido>(id);
        }

        public void UpdatePedido(Pedido pedido)
        {
            if (pedido != null)
            {
                _connection.Update(pedido);
            }
        }

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