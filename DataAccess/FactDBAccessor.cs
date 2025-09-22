using Microsoft.Data.Sqlite;
using Rebekah_As_A_Service.DataAccess.Interfaces;
using Rebekah_As_A_Service.Models;

namespace Rebekah_As_A_Service.DataAccess
{
    public class FactDBAccessor : IFactDBAccessor
    {
        private readonly ILogger<FactDBAccessor> _logger;
        private SqliteCommand _command;
        private SqliteConnection _connection;

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

        public virtual async Task<List<FactResponse>> GetFactsByCategory(string category)
        {
            var list = new List<FactResponse>();
            ConnectToDB();
            _command.CommandText = $"SELECT * FROM RebekahFacts where Category = {category}";

            SqliteDataReader reader = _command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    FactResponse factResponse = new FactResponse();
                    factResponse.Category = reader.GetString(0);
                    factResponse.Fact = reader.GetString(1);
                    factResponse.FactID = reader.GetInt32(2);
                    list.Add(factResponse);
                }
            }

            CloseDB();
            return list;

        }

        public async Task<FactResponse> GetFactByID(int factID)
        {
            var fact = new FactResponse();
            ConnectToDB();
            _command.CommandText = $"SELECT * FROM RebekahFacts where FactID = {factID}";

            SqliteDataReader reader = _command.ExecuteReader();
            if (reader.VisibleFieldCount > 1)
            {
                //need exception
                throw new Exception();
            }

            while (reader.Read())
            {
                while (reader.Read())
                {
                    fact.Category = reader.GetString(0);
                    fact.Fact = reader.GetString(1);
                    fact.FactID = reader.GetInt32(2);
                }
            }

            return fact;

        }
    }
}
