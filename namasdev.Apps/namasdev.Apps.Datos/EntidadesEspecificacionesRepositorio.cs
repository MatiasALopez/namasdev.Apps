using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using namasdev.Data;
using namasdev.Data.Entity;

using namasdev.Apps.Datos.Sql;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos
{
    public interface IEntidadesEspecificacionesRepositorio : IRepositorio<EntidadEspecificaciones, Guid>
    {
        EntidadEspecificaciones Obtener(Guid id, bool cargarEntidadConPropiedadesYClavesYIndices = false);
    }

    public class EntidadesEspecificacionesRepositorio : Repositorio<SqlContext, EntidadEspecificaciones, Guid>, IEntidadesEspecificacionesRepositorio
    {
        public EntidadEspecificaciones Obtener(Guid id,
            bool cargarEntidadConPropiedadesYClavesYIndices = false)
        {
            using (var ctx = CrearContext())
            {
                return ctx.EntidadesEspecificaciones
                    .IncludeMultipleIf(CrearPathsEntidadYPropiedadesYClaves(), cargarEntidadConPropiedadesYClavesYIndices)
                    .FirstOrDefault(e => e.Id == id);
            }
        }

        private IEnumerable<Expression<Func<EntidadEspecificaciones, object>>> CrearPathsEntidadYPropiedadesYClaves()
        {
            return new Expression<Func<EntidadEspecificaciones, object>>[]
            {
                ee => ee.Entidad.Propiedades,
                ee => ee.Entidad.Claves,
                ee => ee.Entidad.Indices.Select(ei => ei.Propiedades),
                ee => ee.Entidad.AplicacionVersion.Aplicacion.Idioma.Textos,
            };
        }
    }
}
