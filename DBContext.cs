using System;
using System.Collections.Generic;
using System.Linq;

public class TarefaContext
{
    private List<Tarefa> tarefas = new List<Tarefa>();

    public List<Tarefa> Tarefas => tarefas; // Propriedade para acessar a lista

    public void AdicionarTarefa(Tarefa tarefa)
    {
        tarefa.Id = tarefas.Count + 1; // Simula a geração de um ID
        tarefas.Add(tarefa);
    }

    public Tarefa ObterTarefa(int id)
    {
        return tarefas.FirstOrDefault(t => t.Id == id);
    }

    public void Update(Tarefa tarefa)
    {
        var tarefaExistente = tarefas.FirstOrDefault(t => t.Id == tarefa.Id);
        if (tarefaExistente != null)
        {
            tarefaExistente.Titulo = tarefa.Titulo;
            tarefaExistente.Descricao = tarefa.Descricao;
            tarefaExistente.Concluida = tarefa.Concluida;
        }
    }

    public void RemoverTarefa(int id)
    {
        tarefas.RemoveAll(t => t.Id == id);
    }
}
