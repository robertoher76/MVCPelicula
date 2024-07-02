using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MVCPelicula.Controllers
{
    public class HelloWorldController : Controller
    {
        //GET: /HelloWorld/
        public ActionResult Index()
        {
            return View();
        }

        //GET: /HelloWorld/Welcome/
        public ActionResult Welcome(string nombre, int numVeces = 1)
        {
            ViewData["nombre"] = "Hola " + nombre;
            ViewData["numVeces"] = numVeces;

            return View();
        }
    }
}
