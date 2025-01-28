using SQLite;
using ChullaApi.Models;
using System.Collections.Generic;

namespace ChullaApi.Data
{
    public class DetallePedidoRepository
    {
        private readonly SQLiteConnection _connection;

        public DetallePedidoRepository(string dbPath)
        {
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<DetallePedido>();
        }

        public void AddNewDetallePedido(DetallePedido detalle)
        {
            if (detalle != null)
            {
                _connection.Insert(detalle);
            }
        }

        public List<DetallePedido> GetAllDetallesPedido()
        {
            return _connection.Table<DetallePedido>().ToList();
        }

        public DetallePedido GetDetallePedidoById(int id)
        {
            return _connection.Find<DetallePedido>(id);
        }

        public void UpdateDetallePedido(DetallePedido detalle)
        {
            if (detalle != null)
            {
                _connection.Update(detalle);
            }
        }

        public void DeleteDetallePedido(int id)
        {
            var detalle = GetDetallePedidoById(id);
            if (detalle != null)
            {
                _connection.Delete(detalle);
            }
        }
    }
}