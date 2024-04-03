
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Interfaces;
using PruebaTecnica.ViewModels.Requests;
using ServiceStack;

namespace PruebaTecnica.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersona _persona;

        public PersonaController(IPersona persona)
        {
            _persona = persona;
        }

        [HttpGet("IndexPersonas")]
        public object GetPersonas()
        {
            return _persona.Get();
        }
        
        [HttpGet("GetPersonaById/{id}")]
        public object GetPorId(int id)
        {
            return _persona.GetById(id);
        }

        [HttpDelete("DeletePersona/{id}")]
        public object DeletePersona(int id)
        {
            return _persona.Delete(id);
        }
        
        [HttpPut("UpdatePersona/{id}")]
        public object UpdatePersona(int id,UpdatePersona updateRequest)
        {
            return _persona.Update(id, updateRequest);
        }

        [HttpPost("CreatePersona")]
        public object CreatePersona([FromBody] CreatePersona createRequest)
        {
            return _persona.Create(createRequest);
        }
    }
}
