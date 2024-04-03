using PruebaTecnica.Concretes.Contexts;
using PruebaTecnica.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using PruebaTecnica.Models;
using PruebaTecnica.ViewModels.Requests;
using ServiceStack;
using ServiceStack.Script;


namespace PruebaTecnica.Concretes;

public class PersonaConcrete : IPersona
{
    private readonly PruebDbContext _context;

    public PersonaConcrete(PruebDbContext context)
    {
        _context = context;
    }

    public object Get()
    {
        return _context.Personas.ToList();
    }

    public object GetById(int id)
    {
        var empleado = _context.Personas.Where(p => p.Id == id).FirstOrDefault();

        if (empleado is null)
        {
            return new HttpError(HttpStatusCode.BadRequest, "No se encontro el empleado");
        }

        return empleado;
    }

    public object Create(CreatePersona createRequest)
    {
        var nuevoEmpleado = new Persona()
        {
            Nombre = createRequest.Nombre,
            Apellido = createRequest.Apellido,
            Ocupación = createRequest.Ocupacion,
            FechaNacimiento = createRequest.FechaNacimiento
        };

        _context.Personas.Add(nuevoEmpleado);

        _context.SaveChanges();

        return new HttpResult(HttpStatusCode.Created, "Empleado registrado exitosamente");
    }

    public object Delete(int id)
    {
        var usuario = _context.Personas.Where(p => p.Id == id).FirstOrDefault();

        if (usuario is null)
        {
            return new HttpError(HttpStatusCode.BadRequest, "No se encontro al usuario");
        }

        var relacionadoAEmpresa = _context.PersonaEmpresas.Where(pe => pe.IdPersona == id).Any();

        if (relacionadoAEmpresa)
        {
            return new HttpError(HttpStatusCode.BadRequest,
                "La persona está relacionada a una empresa, elimine primero la relacion");
        }

        _context.Personas.Remove(usuario);

        _context.SaveChanges();

        return new HttpResult(HttpStatusCode.OK);
    }

    public object Update(int id, UpdatePersona updateRequest)
    {
        var empleadoExistente = _context.Personas.Where(p => p.Id == id).FirstOrDefault();

        if (empleadoExistente is null)
        {
            return new HttpError(HttpStatusCode.BadRequest, "no se encontro al empleado");
        }

        try
        {
            empleadoExistente.Nombre = updateRequest.Nombre;
            empleadoExistente.Apellido = updateRequest.Apellido;
            empleadoExistente.FechaNacimiento = updateRequest.FechaNacimiento;
            empleadoExistente.Ocupación = updateRequest.Ocupacion;

            _context.SaveChanges();

            return new HttpResult(HttpStatusCode.InternalServerError, "Error al actualizar el empleado");
        }
        catch (Exception e)
        {
            return new HttpError(HttpStatusCode.InternalServerError,
                "Error inesperado al actualizar empleado" + e.Message);
        }
    }
}