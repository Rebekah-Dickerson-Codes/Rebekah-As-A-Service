using Microsoft.Data.Sqlite;
using Rebekah_As_A_Service.Models;
using System.Data;

namespace Rebekah_As_A_Service.DataAccess
{
    public class DatabaseAccessor
    {
        private readonly ILogger<DatabaseAccessor> _logger;
        private SqliteCommand _command;
        private SqliteConnection _connection;

        private void ConnectToDB()
        {
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            _connection = new SqliteConnection("data source=database/rebekahDB.db");
            _command = _connection.CreateCommand();
            _connection.Open();
        }

        private void CloseDB()
        {
            _connection.Close();
        }

        public async Task<FactResponse> GetRebekahFactByCategory(string category)
        {
            var list = new FactResponse();
            ConnectToDB();
            _command.CommandText = $"SELECT * FROM RebekahFacts";
            //do this better this is dumb
            string factCategory = "";
            string fact = "";

            SqliteDataReader reader = _command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    factCategory = reader.GetString(0);
                    fact = reader.GetString(1);
                }
            }

            CloseDB();
            return new FactResponse
            {
                Category = factCategory,
                Fact = fact
            };

        }
    }
}
