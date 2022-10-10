using Npgsql;

var cs = "Host=localhost;Username=postgres;Password=1111;Database=enjoffer";

using var connection = new NpgsqlConnection(cs);
connection.Open();

string sql = "SELECT * FROM users";
using var cmd = new NpgsqlCommand(sql, connection);

using NpgsqlDataReader rdr = cmd.ExecuteReader();

while (rdr.Read())
{
    Console.WriteLine("{0} {1} {2}, {3}", rdr.GetInt32(0), rdr.GetString(1),
            rdr.GetString(2), rdr.GetString(3));
}