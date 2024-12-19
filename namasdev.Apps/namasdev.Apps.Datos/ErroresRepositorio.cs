using System;

using namasdev.Apps.Datos.Sql;
using namasdev.Apps.Entidades;
using namasdev.Data.Entity;

namespace namasdev.Apps.Datos
{
    public class ErroresRepositorio : Repositorio<SqlContext, Error, Guid>, IErroresRepositorio
    {
    }
}
