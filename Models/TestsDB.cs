class TestsDB
{
    static void Main(string[] args)
    {
        // Substitua pela sua string de conexão com o SQL Server
        string connectionString = "Server=localhost;Database=testes;User Id=root;Password=123;";

        DatabaseHelper dbHelper = new DatabaseHelper(connectionString);
        string tabela = "usuarios"; // Substitua pelo nome da sua tabela

        // Exemplo de Create
        Dictionary<string, object> novoUsuario = new Dictionary<string, object>()
        {
            {"nome", "Maria Souza"},
            {"email", "maria@email.com"}
        };
        if (dbHelper.Create(tabela, novoUsuario))
        {
            Console.WriteLine("Usuário criado com sucesso!");
        }
        else
        {
            Console.WriteLine("Erro ao criar usuário.");
        }

        // Exemplo de Read (assumindo que o ID do usuário criado é 1)
        Dictionary<string, object> usuario = dbHelper.Read(tabela, 1);
        if (usuario != null)
        {
            Console.WriteLine("Dados do usuário com ID 1:");
            foreach (var item in usuario)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
        else
        {
            Console.WriteLine("Usuário com ID 1 não encontrado.");
        }

        // Exemplo de Update (atualizando o usuário com ID 1)
        Dictionary<string, object> atualizarUsuario = new Dictionary<string, object>()
        {
            {"Nome", "Maria S. Souza"},
            {"Email", "maria.souza@email.com"}
        };
        if (dbHelper.Update(tabela, 1, atualizarUsuario))
        {
            Console.WriteLine("Usuário atualizado com sucesso!");
        }
        else
        {
            Console.WriteLine("Erro ao atualizar usuário.");
        }

        // Exemplo de ListAll
        List<Dictionary<string, object>> usuarios = dbHelper.ListAll(tabela);
        if (usuarios != null && usuarios.Count > 0)
        {
            Console.WriteLine("Lista de usuários:");
            foreach (var user in usuarios)
            {
                foreach (var item in user)
                {
                    Console.Write($"{item.Key}: {item.Value} | ");
                }
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Nenhum usuário encontrado.");
        }

        // Exemplo de Delete (deletando o usuário com ID 1)
        if (dbHelper.Delete(tabela, 1))
        {
            Console.WriteLine("Usuário deletado com sucesso!");
        }
        else
        {
            Console.WriteLine("Erro ao deletar usuário.");
        }
    }
}