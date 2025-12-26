using System;
using System.Linq;

using namasdev.Data;
using namasdev.Data.Entity;
using namasdev.Apps.Datos.Sql;
using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos
{
    public interface IEntidadesClavesRepositorio : IRepositorio<EntidadClave, Guid>
    {
        void EliminarPorEntidad(Guid entidadId);
    }

    public class EntidadesClavesRepositorio : Repositorio<SqlContext, EntidadClave, Guid>, IEntidadesClavesRepositorio
    {
        public void EliminarPorEntidad(Guid entidadId)
        {
            using (var ctx = CrearContext())
            {
                var claves = ctx.EntidadesClaves
                    .Where(x => x.EntidadId == entidadId);
                foreach (var clave in claves)
                {
                    ctx.EntidadesClaves.Remove(clave);
                }
                ctx.SaveChanges();
            }
        }
    }
}
