using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaTecnica.ViewModels.Requests;

namespace PruebaTecnica.Interfaces
{
    public interface IPersona
    {
        object Get();
        object GetById(int id);
        object Update(int id, UpdatePersona updateRequest);
        object Create(CreatePersona createRequest);
        object Delete(int id);
    }
}
