using Microsoft.AspNetCore.Mvc;

public class TarefasController : Controller
{
    public TarefasController()
    {
        
    }

    public IActionResult Index()
    {
        Console.WriteLine(View());
        return View();
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
            // _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(tarefa);
    }

}