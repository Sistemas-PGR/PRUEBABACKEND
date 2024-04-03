using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.ViewModels.Requests
{
    public class NuevoEmpleadoRequest
    {
        public int IdPersona { get; set; }
        public int IdEmpresa { get; set; }
        public DateTime fechaContrato { get; set; }
        public DateTime fechaFinContrato { get; set; }
    }
}
