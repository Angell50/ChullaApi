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
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Ingreso>();
        }

        public void AddNewIngreso(Ingreso ingreso)
        {
            if (ingreso != null)
            {
                ingreso.FechaIngreso = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _connection.Insert(ingreso);
            }
        }

        public List<Ingreso> GetAllIngresos()
        {
            return _connection.Table<Ingreso>().ToList();
        }

        public Ingreso GetIngresoById(int id)
        {
            return _connection.Find<Ingreso>(id);
        }

        public void UpdateIngreso(Ingreso ingreso)
        {
            if (ingreso != null)
            {
                ingreso.FechaIngreso = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _connection.Update(ingreso);
            }
        }

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