using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Interfaces;
using PruebaTecnica.ViewModels.Requests;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresa _empresa;

        public EmpresaController(IEmpresa empresa)
        {
            _empresa = empresa;
        }

        [HttpGet("IndexEmpresas")]
        public object ListEmpresas()
        {
            return _empresa.Get();
        }
        
        [HttpGet("GetEmpresabyId/{id}")]
        public object GetEmpresa(int id)
        {
            return _empresa.GetEmpresa(id);
        }

        [HttpPost("CreateEmpresa")]
        public object PostEmpresas(NuevaEmpresaRequest nuevaEmpresa)
        {
            return _empresa.Create(nuevaEmpresa);
        }


        [HttpPut("UpdateEmpresa/{id}")]
        public object ActualizarEmpresa(int id, [FromBody] UpdateEmpresa updateRequest)
        {
            return _empresa.Update(id, updateRequest);
        }
        
        [HttpDelete("DeleteEmpresa/{id}")]
        public object EliminarEmpresa(int id)
        {
            return _empresa.Delete(id);
        }

        [HttpGet("IndexEmpleados")]
        public object ListarEmpleados()
        {
            return _empresa.ListarEmpleados();
        }
        
        [HttpGet("GetEmpleadoById/{id}")]
        public object ListarEmpleados(int id)
        {
            return _empresa.ListarEmpleadosById(id);
        }

        [HttpPut("UpdateEmpleado/{idEmpleado}")]
        public object ActualizarEmpleado(int idEmpleado, [FromBody] UpdateEmpleado updateRequest)
        {
            return _empresa.UpdateEmpleado(idEmpleado, updateRequest);
        }

        [HttpPost("CreateEmpleado")]
        public object RegistrarEmpleado([FromBody] NuevoEmpleadoRequest nuevoEmpleado)
        {
            return _empresa.RegistrarEmpleado(nuevoEmpleado);
        }

        [HttpDelete("DeleteEmpleado/{id}")]
        public object DeleteEmpleado(int id)
        {
            return _empresa.DeleteEmpleado(id);
        }
    }
}
    