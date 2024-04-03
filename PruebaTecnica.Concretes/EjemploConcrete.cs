using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;

namespace PruebaTecnica.Concretes
{
    public class EjemploConcrete : IEjemplo
    {
        public object Ejemplo()
        {
            object respuesta = "Hola Mundo";
            return Enumerable.Range(1, 5).Select(index =>
            {
                return new EjemploModelo
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                };
            })
            .ToArray();
            return respuesta;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private class EjemploModelo
        {
            public DateTime Date { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }
        }
    }
}
