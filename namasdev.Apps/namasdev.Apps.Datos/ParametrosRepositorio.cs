using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.WindowsAzure.Storage;

using namasdev.Core.Validation;
using namasdev.Apps.Entidades;
using namasdev.Apps.Datos.Sql;

namespace namasdev.Apps.Datos
{
    public class ParametrosRepositorio : IParametrosRepositorio, IDisposable
    {
        private SqlContext _sqlContext;

        public ParametrosRepositorio(SqlContext sqlContext)
        {
            Validador.ValidarArgumentRequeridoYThrow(sqlContext, nameof(sqlContext));

            _sqlContext = sqlContext;
        }

        public string Obtener(string nombre)
        {
            return _sqlContext.Parametros
                .Where(e => e.Nombre == nombre)
                .Select(e => e.Valor)
                .FirstOrDefault();
        }

        public Dictionary<string, string> Obtener(params string[] nombres)
        {
            if (nombres == null || !nombres.Any())
            {
                return new Dictionary<string, string>();
            }

            return _sqlContext.Parametros
                .Where(e => nombres.Contains(e.Nombre))
                .ToDictionary(e => e.Nombre, e => e.Valor);
        }

        public void Guardar(string nombre, string valor)
        {
            Guardar(new Dictionary<string, string> { { nombre, valor } });
        }

        public void Guardar(Dictionary<string, string> parametros)
        {
            Validador.ValidarArgumentListaRequeridaYThrow(parametros, nameof(parametros));

            Parametro p = null;
            foreach (var parametro in parametros)
            {
                p = _sqlContext.Parametros.Find(parametro.Key);

                if (p == null)
                {
                    p = new Parametro { Nombre = parametro.Key };
                    _sqlContext.Parametros.Add(p);
                }

                p.Valor = parametro.Value;
            }

            _sqlContext.SaveChanges();
        }

        public CloudStorageAccount ObtenerCloudStorageAccount()
        {
            return CloudStorageAccount.Parse(Obtener(ParametroNombres.CLOUD_STORAGE_ACCOUNT_CONNECTION_STRING));
        }

        public void Dispose()
        {
            if (_sqlContext != null) 
            { 
                _sqlContext.Dispose();
            }
        }
    }
}