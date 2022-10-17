using System.Data;
using Npgsql;

namespace ConsoleApp
{
    public class Database
    {
        protected NpgsqlConnection Connection { get; set; }
        protected NpgsqlCommand Command { get; set; }
        protected NpgsqlDataReader Reader { get; set; }

        public Database(string dbName)
        {
            Connection = new NpgsqlConnection(dbName);
            Reader = null;

            Connect();
        }

        protected void Connect()
        {
            try
            {
                Connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected void Disconnect()
        {
            if (Connection == null)
            {
                return;
            }

            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
        }
        protected void Execute(string query)
        {
            Command = new NpgsqlCommand(query, Connection);
            Reader = Command.ExecuteReader();
        }
    }

}