using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using namasdev.Data;

using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos.Entity
{
    public class EntidadCargaPropiedades : ICargaPropiedades<Entidad>
    {
        public bool AplicacionVersion { get; set; }
        public bool Especificaciones { get; set; }
        public bool Propiedades { get; set; }
        public bool Claves { get; set; }
        public bool AsociacionesOrigen { get; set; }
        public bool AsociacionesDestino { get; set; }
        public bool Indices { get; set; }
        public bool Checks { get; set; }

        public static EntidadCargaPropiedades CrearTodas()
        {
            return new EntidadCargaPropiedades
            {
                AplicacionVersion = true,
                Especificaciones = true,
                Propiedades = true,
                Claves = true,
                AsociacionesOrigen = true,
                AsociacionesDestino = true,
                Indices = true,
                Checks = true
            };
        }

        public IEnumerable<Expression<Func<Entidad, object>>> CrearPaths()
        {
            var paths = new List<Expression<Func<Entidad, object>>>();

            if (AplicacionVersion)
            {
                paths.Add(e => e.AplicacionVersion.Aplicacion);
            }
            if (Especificaciones)
            {
                paths.AddRange(new Expression<Func<Entidad, object>>[]
                {
                    e => e.Especificaciones.IDTipo,
                    e => e.Especificaciones.Articulo,
                    e => e.Especificaciones.BajaTipo 
                });
            }
            if (Propiedades)
            {
                paths.Add(e => e.Propiedades.Select(p => p.Tipo));
            }
            if (Claves)
            {
                paths.Add(e => e.Claves.Select(c => c.Propiedad));
            }
            if (AsociacionesOrigen)
            {
                paths.AddRange(new Expression<Func<Entidad, object>>[]
                {
                     e => e.AsociacionesOrigen.Select(a => a.OrigenEntidad),
                    e => e.AsociacionesOrigen.Select(a => a.OrigenPropiedad),
                    e => e.AsociacionesOrigen.Select(a => a.OrigenMultiplicidad),
                    e => e.AsociacionesOrigen.Select(a => a.DestinoPropiedad.Entidad),
                    e => e.AsociacionesOrigen.Select(a => a.DestinoMultiplicidad),
                    e => e.AsociacionesOrigen.Select(a => a.DeleteRegla),
                    e => e.AsociacionesOrigen.Select(a => a.UpdateRegla),
                });
            }
            if (AsociacionesDestino)
            {
                paths.AddRange(new Expression<Func<Entidad, object>>[]
                {
                    e => e.AsociacionesDestino.Select(a => a.OrigenEntidad),
                    e => e.AsociacionesDestino.Select(a => a.OrigenPropiedad),
                    e => e.AsociacionesDestino.Select(a => a.OrigenMultiplicidad),
                    e => e.AsociacionesDestino.Select(a => a.DestinoPropiedad.Entidad),
                    e => e.AsociacionesDestino.Select(a => a.DestinoMultiplicidad),
                    e => e.AsociacionesDestino.Select(a => a.DeleteRegla),
                    e => e.AsociacionesDestino.Select(a => a.UpdateRegla),
                });
            }
            if (Indices)
            {
                paths.Add(e => e.Indices.Select(i => i.Propiedades.Select(p => p.Propiedad)));
            }
            if (Checks)
            {
                paths.Add(e => e.Checks);
            }

            return paths;
        }
    }
}
