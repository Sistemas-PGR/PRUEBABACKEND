using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models
{
    public partial class PersonaEmpresa
    {
        [Key]
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public int IdEmpresa { get; set; }
        public DateTime? FechaContrato { get; set; }
        public DateTime? FechaFinContrato { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; } = null!;
        public virtual Persona IdPersonaNavigation { get; set; } = null!;
    }
}
