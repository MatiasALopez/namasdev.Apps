using System;

using namasdev.Data;
using namasdev.Data.Entity;
using namasdev.Apps.Datos.Sql;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos
{
    public interface IErroresRepositorio : IRepositorio<Error, Guid>
    {
    }

    public class ErroresRepositorio : Repositorio<SqlContext, Error, Guid>, IErroresRepositorio
    {
    }
}
