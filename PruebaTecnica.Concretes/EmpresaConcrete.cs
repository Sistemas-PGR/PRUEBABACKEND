using System;
using System.Net;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Concretes.Contexts;
using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;
using PruebaTecnica.ViewModels.Requests;
using PruebaTecnica.ViewModels.Responses;
using Microsoft.Extensions.Logging;


namespace PruebaTecnica.Concretes
{
    public class EmpresaConcrete : IEmpresa
    {
        private readonly PruebDbContext _context;

        public EmpresaConcrete(PruebDbContext context)
        {
            _context = context;
        }

        public object Create(NuevaEmpresaRequest nuevaEmpresa)
        {
            var empresa = new Empresa()
            {
                Nombre = nuevaEmpresa.Nombre,
                Direccion = nuevaEmpresa.Direccion,
                Telefono = nuevaEmpresa.Telefono
            };

            try
            {
                _context.Empresas.Add(empresa);

                var empresaGuardada = _context.SaveChanges() > 0;

                if (!empresaGuardada)
                {
                    return new HttpError(HttpStatusCode.InternalServerError,
                        "Ha ocurrido un error al guardar la empresa");
                }

                return new HttpResult(HttpStatusCode.Created, "Empresa Creada correctamente");
            }
            catch (Exception ex)
            {
                return new HttpError(HttpStatusCode.InternalServerError, "Ha ocurrido un error al guardar la empresa");
            }
        }

        public object Get()
        {
            var empresas = _context.Empresas.ToList();

            return new HttpResult(empresas, HttpStatusCode.OK);
        }

        public object GetEmpresa(int id)
        {
            var empresaEncontrada = _context.Empresas.Where(e => e.Id == id).FirstOrDefault();
            if (empresaEncontrada == null)
            {
                return new HttpError(HttpStatusCode.BadRequest, "El empleado no existe");
            }

            return new HttpResult(empresaEncontrada);
        }

        public object RegistrarEmpleado(NuevoEmpleadoRequest nuevoEmpleadoReq)
        {
            var empresaExiste = _context.Empresas.Any(e => e.Id == nuevoEmpleadoReq.IdEmpresa);

            if (!empresaExiste)
            {
                return new HttpError(HttpStatusCode.BadRequest, "La empresa no existe");
            }

            var personaExiste = _context.Personas.Any(p => p.Id == nuevoEmpleadoReq.IdPersona);

            if (!personaExiste)
            {
                return new HttpError(HttpStatusCode.BadRequest, "La persona no existe");
            }

            var yaRegistrado = _context.PersonaEmpresas.Any(pe =>
                (pe.IdEmpresa == nuevoEmpleadoReq.IdEmpresa && pe.IdPersona == nuevoEmpleadoReq.IdPersona));

            if (yaRegistrado)
            {
                return new HttpError(HttpStatusCode.BadRequest, "El empleado ya está registrado en esa empresa");
            }

            var nuevoEmpleado = new PersonaEmpresa
            {
                IdEmpresa = nuevoEmpleadoReq.IdEmpresa,
                IdPersona = nuevoEmpleadoReq.IdPersona,
                FechaContrato = nuevoEmpleadoReq.fechaContrato,
                FechaFinContrato = nuevoEmpleadoReq.fechaFinContrato
            };

            try
            {
                _context.PersonaEmpresas.Add(nuevoEmpleado);

                _context.SaveChanges();


                return new HttpResult(HttpStatusCode.Created, "Emplado registrado con éxito");
            }
            catch (Exception ex)
            {
                return new HttpError(HttpStatusCode.InternalServerError, $"Ha ocurrido un error inesperado {ex}");
            }
        }

        public object Update(int id, UpdateEmpresa updateRequest)
        {
            var empresaExistente = _context.Empresas.Where(e => e.Id == id).FirstOrDefault();

            if (empresaExistente is null)
            {
                return new HttpError(HttpStatusCode.BadRequest, "no se encontro la empresa");
            }

            try
            {
                empresaExistente.Nombre = updateRequest.Nombre;
                empresaExistente.Telefono = updateRequest.Telefono;
                empresaExistente.Direccion = updateRequest.Direccion;

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new HttpError(HttpStatusCode.InternalServerError,
                    "Error inesperado al actualizar la empresa" + e.Message);
            }

            return new HttpResult(HttpStatusCode.OK, "Empresa actualizada con exito");
        }

        public object Delete(int id)
        {
            var empresaExistente = _context.Empresas.Where(e => e.Id == id).FirstOrDefault();

            if (empresaExistente is null)
            {
                return new HttpError(HttpStatusCode.BadRequest, "No se encontro la empresa");
            }

            var empresaRelacionada = _context.PersonaEmpresas.Any(e => e.IdEmpresa == id);

            if (empresaRelacionada)
            {
                return new HttpError(HttpStatusCode.BadRequest,
                    "La empresa se ha relacionado a uno o más empleados, elimine la relacion primero");
            }

            _context.Empresas.Remove(empresaExistente);

            _context.SaveChanges();

            return new HttpResult(HttpStatusCode.OK, "Empresa eliminada con exito");
        }

        public object ListarEmpleadosById(int id)
        {

            var empleadoExistente = (from pe in _context.PersonaEmpresas
                join e in _context.Empresas on pe.IdEmpresa equals e.Id
                join p in _context.Personas on pe.IdPersona equals p.Id
                where pe.Id == id
                select new EmpleadoResponse()
                {
                    Id = pe.Id,
                    IdPersona = pe.IdPersona,
                    IdEmpresa = pe.IdEmpresa,
                    NombreEmpleado = p.Nombre + " " + p.Apellido,
                    NombreEmpresa = e.Nombre,
                    FechaContrato = pe.FechaContrato,
                    FechaFinContrato = pe.FechaFinContrato
                }).FirstOrDefault();

            if (empleadoExistente is null)
            {
                return new HttpError(HttpStatusCode.BadRequest, "No se encontro al empleado");
                
            }

            return new HttpResult(empleadoExistente, HttpStatusCode.OK);
        }

        public object UpdateEmpleado(int id, UpdateEmpleado updateRequest)
        {
            // Se actualiza tomando el id del registro de empleado,
            // nos se puede cambiar la persona con la que está relacionado el empleado
            var empleadoExistente = _context.PersonaEmpresas
                .FirstOrDefault(pe => pe.Id == id);

            if (empleadoExistente is null)
            {
                return new HttpError(HttpStatusCode.BadRequest, "No se encontro el empleado");
            }

            empleadoExistente.IdEmpresa = updateRequest.IdEmpresa;
            empleadoExistente.FechaContrato = updateRequest.fechaContrato;
            empleadoExistente.FechaFinContrato = updateRequest.fechaFinContrato;

            _context.SaveChanges();

            return new HttpResult(HttpStatusCode.OK, "Empleado actualizado con exito");
        }

        public object ListarEmpleados()
        {
            var empleados = (from pe in _context.PersonaEmpresas
                join e in _context.Empresas on pe.IdEmpresa equals e.Id
                join p in _context.Personas on pe.IdPersona equals p.Id
                select new EmpleadoResponse()
                {
                    Id = pe.Id,
                    IdPersona = pe.IdPersona,
                    IdEmpresa = pe.IdEmpresa,
                    NombreEmpleado = p.Nombre + " " + p.Apellido,
                    NombreEmpresa = e.Nombre,
                    FechaContrato = pe.FechaContrato,
                    FechaFinContrato = pe.FechaFinContrato
                }).ToList();

            return new HttpResult(empleados, HttpStatusCode.OK);
        }

        public object DeleteEmpleado(int id)
        {
            // Se elimina por el id de registro de EmpleadoEmpresa, no por el de una persona
            var empleadoExistente = _context.PersonaEmpresas.Where(e => e.Id == id).FirstOrDefault();

            if (empleadoExistente is null)
            {
                return new HttpError(HttpStatusCode.BadRequest, "No se encontro el empleado");
            }

            _context.Remove(empleadoExistente);

            _context.SaveChanges();

            return new HttpResult(HttpStatusCode.OK, "Empleado eliminado correctamente");
        }
    }
}