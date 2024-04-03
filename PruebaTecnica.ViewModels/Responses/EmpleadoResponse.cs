namespace PruebaTecnica.ViewModels.Responses;

public class EmpleadoResponse
{
        public int Id { get; set; }
        // Por si a caso
        public int IdPersona { get; set; }
        public string NombreEmpleado { get; set; }
        // Por si a caso
        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public DateTime? FechaContrato { get; set; }
        public DateTime? FechaFinContrato { get; set; }
}
