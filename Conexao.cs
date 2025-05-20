using System;
using System.Data.SQLite; //substitua pelo driver sqlserver
// https://learn.microsoft.com/pt-br/azure/azure-sql/database/connect-query-dotnet-core?view=azuresql
class Conexao
{
    static void Main()
    {
        string dbPath = "Data Source=meu_banco.db"; //substitua por uma conexão sqlservers

        using (var connection = new SQLiteConnection(dbPath))
        {
            connection.Open();

            // Atualize o código para um código sqlserver
            string createTable = @"CREATE TABLE IF NOT EXISTS usuarios (
                                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        nome TEXT,
                                        email TEXT
                                   );";
            using (var cmd = new SQLiteCommand(createTable, connection))
            {
                cmd.ExecuteNonQuery();
            }

            //1- Crie uma string para inserir usuários
            string insert = "???????????";
            //corrija os bugs
            using (var cmd = new SQLiteCommand(insert, connection))
            {
                cmd.Parameters.AddWithValue("@nome", "João da Silva");
                cmd.Parameters.AddWithValue("@email", "joao@example.com");
                cmd.ExecuteNonQuery();
            }

            //2- Crie uma string para consulta 
            string select = "SELECT * FROM ";
            using (var cmd = new SQLiteCommand(select, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["id"]} - Nome: {reader["nome"]} - Email: {reader["email"]}");
                }
            }

            //3- Implemente o código para apagar 

            //4- Implemente o código para atualizar um registro
        }
    }
}
