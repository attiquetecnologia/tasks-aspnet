using Microsoft.AspNetCore.Mvc;

public class Home : Controller
{
    public Home()
    {
        
    }

    public IActionResult Index()
    {
        Console.WriteLine(View());
        return View();
    }
}
