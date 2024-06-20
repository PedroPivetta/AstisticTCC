using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    public class Games : Controller
    {
        public IActionResult Game()
        {
            return View();
        }

        public IActionResult CacaPalavras()
        {
            return View();
        }

        public IActionResult JogoDaMemoria()
        {
            return View();
        }

        public IActionResult Peca()
        {
            return View();
        }
    }
}
