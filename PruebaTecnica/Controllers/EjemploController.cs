using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Interfaces;
using PruebaTecnica.Concretes;

namespace PruebaTecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EjemploController : Controller
    {
        private readonly IEjemplo _IEjemplo;
        public EjemploController(IEjemplo iejemplo)
        {
            _IEjemplo = iejemplo;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            object respuesta = _IEjemplo.Ejemplo();
            return Ok(respuesta);
        }
    }
}
