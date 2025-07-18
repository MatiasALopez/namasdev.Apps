using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using namasdev.Data;

using namasdev.Apps.Entidades;

namespace namasdev.Apps.Datos.Entity
{
    public class AplicacionVersionCargaPropiedades : ICargaPropiedades<AplicacionVersion>
    {
        public bool Aplicacion { get; set; }
        public bool Entidades { get; set; }

        public static AplicacionVersionCargaPropiedades CrearTodas()
        {
            return new AplicacionVersionCargaPropiedades
            {
                Aplicacion = true,
                Entidades = true
            };
        }

        public static AplicacionVersionCargaPropiedades CrearAplicacion()
        {
            return new AplicacionVersionCargaPropiedades
            {
                Aplicacion = true,
            };
        }

        public IEnumerable<Expression<Func<AplicacionVersion, object>>> CrearPaths()
        {
            var paths = new List<Expression<Func<AplicacionVersion, object>>>();

            if (Aplicacion)
            {
                paths.Add(av => av.Aplicacion);
            }
            if (Entidades)
            {
                paths.Add(av => av.Entidades);
            }

            return paths;
        }
    }
}
