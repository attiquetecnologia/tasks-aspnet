using Microsoft.AspNetCore.Mvc;

public class TarefasController : Controller
{
    private readonly TarefaContext _context;

    public TarefasController(TarefaContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var tarefas = _context.Tarefas.ToList();
        return View(tarefas);
    }

    public IActionResult Criar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Criar(Tarefa tarefa)
    {
        if (ModelState.IsValid)
        {
            tarefa.DataCriacao = DateTime.Now;
            _context.Tarefas.Add(tarefa);
            // _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(tarefa);
    }

    public IActionResult Editar(int id)
    {
        var tarefa = _context.Tarefas.Find(id);
        if (tarefa == null)
        {
            return NotFound();
        }
        return View(tarefa);
    }

    [HttpPost]
    public IActionResult Editar(Tarefa tarefa)
    {
        if (ModelState.IsValid)
        {
            _context.Tarefas.Update(tarefa);
            // _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(tarefa);
    }

    public IActionResult Deletar(int id)
    {
        var tarefa = _context.Tarefas.Find(id);
        if (tarefa == null)
        {
            return NotFound();
        }
        return View(tarefa);
    }

    [HttpPost, ActionName("Deletar")]
    public IActionResult DeletarConfirmado(int id)
    {
        var tarefa = _context.Tarefas.Find(id);
        _context.Tarefas.Remove(tarefa);
        // _context.SaveChanges();
        return RedirectToAction("Index");
    }
}