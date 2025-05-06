using System;
using System.Data.SQLite;

class Program
{
    static void Main()
    {
        // Caminho para o banco de dados
        string dbPath = "Data Source=meu_banco.db";

        using (var connection = new SQLiteConnection(dbPath))
        {
            connection.Open();

            // Criação da tabela
            string createTable = @"CREATE TABLE IF NOT EXISTS usuarios (
                                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        nome TEXT,
                                        email TEXT
                                   );";
            using (var cmd = new SQLiteCommand(createTable, connection))
            {
                cmd.ExecuteNonQuery();
            }

            // Inserção de dados
            string insert = "INSERT INTO usuarios (nome, email) VALUES (@nome, @email)";
            using (var cmd = new SQLiteCommand(insert, connection))
            {
                cmd.Parameters.AddWithValue("@nome", "João da Silva");
                cmd.Parameters.AddWithValue("@email", "joao@example.com");
                cmd.ExecuteNonQuery();
            }

            // Leitura dos dados
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
