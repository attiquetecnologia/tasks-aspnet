using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class DatabaseHelper
{

    /*
    Explicação do código C#:

using System.Data.SqlClient;: Importa o namespace necessário para interagir com o SQL Server.
DatabaseHelper Class:
_connectionString: Uma variável privada para armazenar a string de conexão com o banco de dados.
DatabaseHelper(string connectionString): O construtor da classe que recebe a string de conexão.
Create(string tabela, Dictionary<string, object> dados):
Recebe o nome da tabela e um dicionário dados onde as chaves são os nomes das colunas e os valores são os dados a serem inseridos.
Cria a string SQL de INSERT dinamicamente com base nas chaves do dicionário.
Utiliza SqlCommand com parâmetros para evitar SQL injection.
Executa a query e retorna true se a inserção for bem-sucedida.
Read(string tabela, int id):
Recebe o nome da tabela e o id do registro a ser lido.
Cria a string SQL de SELECT com uma cláusula WHERE.
Utiliza SqlCommand com um parâmetro para o id.
Executa a query e lê o resultado usando SqlDataReader.
Retorna um Dictionary<string, object> com os dados do registro ou null se não encontrado.
Update(string tabela, int id, Dictionary<string, object> dados):
Recebe o nome da tabela, o id do registro a ser atualizado e um dicionário dados com os novos valores.
Cria a string SQL de UPDATE dinamicamente com base nas chaves do dicionário.
Utiliza SqlCommand com parâmetros para evitar SQL injection.
Executa a query e retorna true se a atualização for bem-sucedida.
Delete(string tabela, int id):
Recebe o nome da tabela e o id do registro a ser deletado.
Cria a string SQL de DELETE com uma cláusula WHERE.
Utiliza SqlCommand com um parâmetro para o id.
Executa a query e retorna true se a exclusão for bem-sucedida.
ListAll(string tabela):
Recebe o nome da tabela.
Cria a string SQL de SELECT para buscar todos os registros.
Utiliza SqlCommand e SqlDataReader para ler todos os registros.
Retorna uma List<Dictionary<string, object>> contendo todos os registros da tabela.
Program Class:
Main(string[] args): O ponto de entrada da aplicação.
connectionString: Você precisa substituir esta string com a sua string de conexão real com o SQL Server. Ela geralmente contém informações sobre o servidor, banco de dados, usuário e senha.
Cria uma instância da classe DatabaseHelper.
Define o nome da tabela (Usuarios - substitua pelo nome da sua tabela).
Exemplos de uso de cada função CRUD: Create, Read, Update, ListAll e Delete. Os resultados das operações são exibidos no console.
Para usar este código:

Certifique-se de ter o SQL Server instalado e configurado.
Crie um banco de dados com o nome que você especificar na connectionString.
Crie uma tabela (por exemplo, Usuarios) com uma coluna Id (INT, PRIMARY KEY, IDENTITY) e outras colunas que você deseja manipular (por exemplo, Nome NVARCHAR(255), Email NVARCHAR(255)). A coluna Id geralmente é configurada como Identity para auto-incremento.
Substitua a string de conexão na variável connectionString dentro da função Main com as suas credenciais corretas do SQL Server.
Substitua o nome da tabela na variável tabela para corresponder ao nome da sua tabela no banco de dados.
Salve o código em um arquivo .cs (por exemplo, CrudSqlServer.cs).
Compile e execute o código utilizando um compilador C# (como o que vem com o .NET SDK ou Visual Studio).
Este exemplo fornece uma base sólida para realizar operações CRUD em um banco de dados SQL Server usando C#. Você pode expandir essa classe com tratamento de erros mais detalhado, métodos para consultas mais complexas, e integração com outras partes da sua aplicação.
    */
    private readonly string _connectionString;

    public DatabaseHelper(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool Create(string tabela, Dictionary<string, object> dados)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                string campos = string.Join(", ", dados.Keys);
                string parametros = string.Join(", @", dados.Keys.Select(k => k));
                string sql = $"INSERT INTO {tabela} ({campos}) VALUES (@{parametros})";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    foreach (var item in dados)
                    {
                        command.Parameters.AddWithValue($"@{item.Key}", item.Value ?? DBNull.Value);
                    }
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar registro: {ex.Message}");
                return false;
            }
        }
    }

    public Dictionary<string, object> Read(string tabela, int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                string sql = $"SELECT * FROM {tabela} WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Dictionary<string, object> registro = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                registro[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            }
                            return registro;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ler registro: {ex.Message}");
                return null;
            }
        }
    }

    public bool Update(string tabela, int id, Dictionary<string, object> dados)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                List<string> sets = new List<string>();
                foreach (var key in dados.Keys)
                {
                    sets.Add($"{key} = @{key}");
                }
                string setClause = string.Join(", ", sets);
                string sql = $"UPDATE {tabela} SET {setClause} WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    foreach (var item in dados)
                    {
                        command.Parameters.AddWithValue($"@{item.Key}", item.Value ?? DBNull.Value);
                    }
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar registro: {ex.Message}");
                return false;
            }
        }
    }

    public bool Delete(string tabela, int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                string sql = $"DELETE FROM {tabela} WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar registro: {ex.Message}");
                return false;
            }
        }
    }

    public List<Dictionary<string, object>> ListAll(string tabela)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                string sql = $"SELECT * FROM {tabela}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Dictionary<string, object>> lista = new List<Dictionary<string, object>>();
                    while (reader.Read())
                    {
                        Dictionary<string, object> registro = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            registro[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                        }
                        lista.Add(registro);
                    }
                    return lista;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar registros: {ex.Message}");
                return null;
            }
        }
    }
}