using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.ViewModels.Requests
{
    public class NuevaEmpresaRequest
    {
        [Required]
        public string Nombre { get; set; } = null!;
        [Phone]
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
    }
}
