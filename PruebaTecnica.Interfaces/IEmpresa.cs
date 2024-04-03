using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaTecnica.ViewModels.Requests;

namespace PruebaTecnica.Interfaces
{
    public interface IEmpresa
    {
        object Get();
        object GetEmpresa(int id);
        object Update(int id, UpdateEmpresa updateRequest);
        object Create(NuevaEmpresaRequest nuevaEmpresa);
        object Delete(int id);
        object ListarEmpleados();
        object ListarEmpleadosById(int id);
        object UpdateEmpleado(int id, UpdateEmpleado updateRequest);
        object RegistrarEmpleado(NuevoEmpleadoRequest nuevoEmpleado);
        object DeleteEmpleado(int id);
    }
}
