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
            // Inicializa la conexión con SQLite
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<DetallePedido>();
        }

        // Método para agregar un nuevo detalle de pedido
        public void AddNewDetallePedido(DetallePedido detalle)
        {
            if (detalle != null)
            {
                _connection.Insert(detalle);
            }
        }

        // Método para obtener todos los detalles de pedidos
        public List<DetallePedido> GetAllDetallesPedido()
        {
            return _connection.Table<DetallePedido>().ToList();
        }

        // Método para obtener un detalle de pedido por ID
        public DetallePedido GetDetallePedidoById(int id)
        {
            return _connection.Find<DetallePedido>(id);
        }

        // Método para actualizar un detalle de pedido existente
        public void UpdateDetallePedido(DetallePedido detalle)
        {
            if (detalle != null)
            {
                _connection.Update(detalle);
            }
        }

        // Método para eliminar un detalle de pedido por ID
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