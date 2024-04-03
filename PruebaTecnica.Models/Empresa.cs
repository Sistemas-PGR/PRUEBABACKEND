using System;
using System.Collections.Generic;

namespace PruebaTecnica.Models
{
    public partial class Empresa
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
    }
}
