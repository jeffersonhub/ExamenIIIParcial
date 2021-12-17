using SQLite;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ExamenIIIParcial.Models;

namespace ExamenIIIParcial.Config
{
    public class DataBase
    {
        readonly SQLiteAsyncConnection dataBase;
        private static DataBase instance { get; set; }
        private DataBase(string _dbPath)
        {
            dataBase = new SQLiteAsyncConnection(_dbPath);
            dataBase.CreateTableAsync<Pagos>().Wait();
        }

        //Se crea un metodo para poder crear una sola instancia de la base de datos 
        public static DataBase CurrentDB
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dbmvvm.db3"));
                }
                return instance;
            }
        }

        public Task<List<Pagos>> GetAllEmployees()
        {
             return dataBase.Table<Pagos>().OrderByDescending(c => c.Fecha).ToListAsync();

        }
        public Task<int> GetEmpleyeeCount()
        {
            return dataBase.Table<Pagos>().CountAsync();
        }

        public Task<Pagos> GetEmployeById(int id)
        {
           
            return dataBase.Table<Pagos>().Where(i => i.IdPago == id).FirstOrDefaultAsync();
        }
        public Task<int> Save(Pagos employe)
        {
            if (employe.IdPago != 0)
            {
                return dataBase.UpdateAsync(employe);
            }
            else
            {
                return dataBase.InsertAsync(employe);
            }
        }

        public Task<int> DeleteEmployee(Pagos employee)
        {
            return dataBase.DeleteAsync(employee);
        }
    }
       
}
