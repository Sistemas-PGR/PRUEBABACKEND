using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Contexts;
namespace PruebaTecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EjemploController : Controller
    {
        private readonly DatabaseContext _dbcontext;
        public EjemploController(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            object respuesta = _dbcontext.EjemploModelo.ToList();
            return Ok(respuesta);
        }
    }
}
