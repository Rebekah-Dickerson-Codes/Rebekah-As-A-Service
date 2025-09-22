using Microsoft.Data.Sqlite;
using Rebekah_As_A_Service.DataAccess.Interfaces;
using Rebekah_As_A_Service.Models;

namespace Rebekah_As_A_Service.DataAccess
{
    public class CategoryDBAccessor : ICategoryDBAccessor
    {
        private readonly ILogger<CategoryDBAccessor> _logger;
        private SqliteCommand _command;
        private SqliteConnection _connection;


        //DO NOT FUCKING FORGET TO SANITIZE SHIT BEFORE IT GETS HERE
        private void ConnectToDB()
        {
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            //pull the connection string out to a setting file somewhere
            _connection = new SqliteConnection("data source=database/rebekahDB.db");
            _command = _connection.CreateCommand();
            _connection.Open();
        }

        private void CloseDB()
        {
            _connection.Close();
        }

        public async Task CreateNewCategory(string categoryName)
        {
            ConnectToDB();
            _command.CommandText = $"INSERT into Category (name) VALUES ('{categoryName}')";

            var response = _command.ExecuteScalar();

            CloseDB();
        }

        public virtual async Task<List<CategoryResponse>> GetCategories()
        {
            var list = new List<CategoryResponse>();
            ConnectToDB();
            _command.CommandText = $"SELECT * FROM Category";

            SqliteDataReader reader = _command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CategoryResponse catResponse = new CategoryResponse();
                    catResponse.Name = reader.GetString(0);
                    catResponse.CategoryID = reader.GetInt32(1);
                    list.Add(catResponse);
                }
            }
            CloseDB();
            return list;

        }
    }
}
