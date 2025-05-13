using System;
using System.Data.SQLite;

class Conexao
{
    static void Main()
    {
        string dbPath = "Data Source=meu_banco.db";

        using (var connection = new SQLiteConnection(dbPath))
        {
            connection.Open();

            string createTable = @"CREATE TABLE IF NOT EXISTS usuarios (
                                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        nome TEXT,
                                        email TEXT
                                   );";
            using (var cmd = new SQLiteCommand(createTable, connection))
            {
                cmd.ExecuteNonQuery();
            }

            string insert = "INSERT INTO usuarios (nome, email) VALUES (@nome, @email)";
            using (var cmd = new SQLiteCommand(insert, connection))
            {
                cmd.Parameters.AddWithValue("@nome", "João da Silva");
                cmd.Parameters.AddWithValue("@email", "joao@example.com");
                cmd.ExecuteNonQuery();
            }

            string select = "SELECT * FROM usuarios";
            using (var cmd = new SQLiteCommand(select, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["id"]} - Nome: {reader["nome"]} - Email: {reader["email"]}");
                }
            }
        }
    }
}
