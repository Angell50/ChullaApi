using SQLite;
using ChullaApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChullaApi.Data
{
    public class IngresoRepository
    {
        private readonly SQLiteConnection _connection;

        public IngresoRepository(string dbPath)
        {
            // Inicializa la conexión con SQLite
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Ingreso>();
        }

        // Método para agregar un nuevo ingreso
        public void AddNewIngreso(Ingreso ingreso)
        {
            if (ingreso != null)
            {
                ingreso.FechaIngreso = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _connection.Insert(ingreso);
            }
        }

        // Método para obtener todos los ingresos
        public List<Ingreso> GetAllIngresos()
        {
            return _connection.Table<Ingreso>().ToList();
        }

        // Método para obtener un ingreso por ID
        public Ingreso GetIngresoById(int id)
        {
            return _connection.Find<Ingreso>(id);
        }

        // Método para actualizar un ingreso existente
        public void UpdateIngreso(Ingreso ingreso)
        {
            if (ingreso != null)
            {
                ingreso.FechaIngreso = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _connection.Update(ingreso);
            }
        }

        // Método para eliminar un ingreso por ID
        public void DeleteIngreso(int id)
        {
            var ingreso = GetIngresoById(id);
            if (ingreso != null)
            {
                _connection.Delete(ingreso);
            }
        }
    }
}