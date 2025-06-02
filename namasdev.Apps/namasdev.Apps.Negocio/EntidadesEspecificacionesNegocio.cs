using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using namasdev.Core.Exceptions;
using namasdev.Core.Transactions;
using namasdev.Core.Types;
using namasdev.Core.Validation;

using namasdev.Apps.Datos;
using namasdev.Apps.Entidades;
using namasdev.Apps.Entidades.Metadata;
using namasdev.Apps.Entidades.Valores;
using namasdev.Apps.Negocio.DTO.EntidadesEspecificaciones;

namespace namasdev.Apps.Negocio
{
    public interface IEntidadesEspecificacionesNegocio
    {
        void Actualizar(ActualizarParametros parametros);
    }

    public class EntidadesEspecificacionesNegocio : NegocioBase<IEntidadesEspecificacionesRepositorio>, IEntidadesEspecificacionesNegocio
    {
        private readonly IEntidadesPropiedadesRepositorio _entidadesPropiedadesRepositorio;
        private readonly IEntidadesClavesRepositorio _entidadesClavesRepositorio;
        private readonly IEntidadesIndicesRepositorio _entidadesIndicesRepositorio;
        private readonly IEntidadesAsociacionesRepositorio _entidadesAsociacionesRepositorio;

        public EntidadesEspecificacionesNegocio(
            IEntidadesPropiedadesRepositorio entidadesPropiedadesRepositorio,
            IEntidadesClavesRepositorio entidadesClavesRepositorio,
            IEntidadesIndicesRepositorio entidadesIndicesRepositorio,
            IEntidadesAsociacionesRepositorio entidadesAsociacionesRepositorio,
            IEntidadesEspecificacionesRepositorio entidadesEspecificacionesRepositorio, IErroresNegocio erroresNegocio, IMapper mapper)
            : base(entidadesEspecificacionesRepositorio, erroresNegocio, mapper)
        {
            Validador.ValidarArgumentRequeridoYThrow(entidadesPropiedadesRepositorio, nameof(entidadesPropiedadesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesClavesRepositorio, nameof(entidadesClavesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesIndicesRepositorio, nameof(entidadesIndicesRepositorio));
            Validador.ValidarArgumentRequeridoYThrow(entidadesAsociacionesRepositorio, nameof(entidadesAsociacionesRepositorio));

            _entidadesPropiedadesRepositorio = entidadesPropiedadesRepositorio;
            _entidadesClavesRepositorio = entidadesClavesRepositorio;
            _entidadesIndicesRepositorio = entidadesIndicesRepositorio;
            _entidadesAsociacionesRepositorio = entidadesAsociacionesRepositorio;
        }

        // TODO (ML): mover eliminacion de propiedades a EntidadesPropiedadesNegocio
        public void Actualizar(ActualizarParametros parametros)
        {
            Validador.ValidarArgumentRequeridoYThrow(parametros, nameof(parametros));

            var entidad = Obtener(parametros.Id, cargarEntidadConDatos: true);
            
            var propiedadesAActualizar = ActualizarPropiedadesEnEntidadYObtenerLista(entidad, parametros);
            var propiedadesAEliminar = EliminarPropiedadesDeEntidadYObtenerLista(entidad, parametros);

            List<EntidadClave> clavesAEliminar;
            List<EntidadIndice> indicesAEliminar; 
            EliminarClavesYIndicesDePropiedadesAEliminarDeEntidad(entidad.Entidad, propiedadesAEliminar,
                out clavesAEliminar, 
                out indicesAEliminar);

            Mapper.Map(parametros, entidad);

            ValidarDatos(entidad);

            var propiedadesAAgregar = CrearPropiedadesParaEntidad(entidad);
            entidad.Entidad.Propiedades.AddRange(propiedadesAAgregar);

            var claves = CrearClavesParaEntidad(entidad);
            
            using (var ts = TransactionScopeFactory.Crear())
            {
                Repositorio.Actualizar(entidad);

                if (clavesAEliminar != null && clavesAEliminar.Any())
                {
                    _entidadesClavesRepositorio.Eliminar(clavesAEliminar);
                }
                if (indicesAEliminar != null && indicesAEliminar.Any())
                {
                    _entidadesIndicesRepositorio.Eliminar(indicesAEliminar);
                }
                if (propiedadesAActualizar != null && propiedadesAActualizar.Any())
                {
                    _entidadesPropiedadesRepositorio.Actualizar(propiedadesAActualizar);
                }
                if (propiedadesAEliminar != null && propiedadesAEliminar.Any())
                {
                    _entidadesPropiedadesRepositorio.Eliminar(propiedadesAEliminar);
                }
                if (propiedadesAAgregar != null && propiedadesAAgregar.Any())
                {
                    _entidadesPropiedadesRepositorio.Agregar(propiedadesAAgregar);
                }
                if (claves != null && claves.Any())
                {
                    _entidadesClavesRepositorio.Agregar(claves);
                }

                ts.Complete();
            }
        }

        private EntidadEspecificaciones Obtener(Guid id,
            bool cargarEntidadConDatos = false,
            bool validarExistencia = true)
        {
            var entidad = Repositorio.Obtener(id, cargarEntidadConPropiedadesYClavesYIndices: cargarEntidadConDatos);
            if (validarExistencia
                && entidad == null)
            {
                throw new Exception(Validador.MensajeEntidadInexistente(EntidadEspecificacionesMetadata.ETIQUETA, id));
            }

            return entidad;
        }

        private void ValidarDatos(EntidadEspecificaciones entidad)
        {
            var errores = new List<string>();

            if (!entidad.IDPropiedadTipoId.HasValue)
            {
                if (entidad.BajaTipoId != BajaTipos.NINGUNA)
                {
                    errores.Add($"Las entidades sin ID no pueden ser borradas.");
                }
                if (entidad.AuditoriaCreado || entidad.AuditoriaUltimaModificacion)
                {
                    errores.Add($"Las entidades sin ID no pueden guardar auditorías.");
                }
            }
            if (entidad.EsSoloLectura)
            {
                if (entidad.BajaTipoId != BajaTipos.NINGUNA)
                {
                    errores.Add($"Las entidades de solo lectura no pueden ser borradas.");
                }
                if (entidad.AuditoriaCreado || entidad.AuditoriaUltimaModificacion)
                {
                    errores.Add($"Las entidades de solo lectura no pueden guardar auditorías.");
                }
            }
            
            Validador.LanzarExcepcionMensajeAlUsuarioSiExistenErrores(errores);
        }

        private IEnumerable<EntidadPropiedad> EliminarPropiedadesDeEntidadYObtenerLista(EntidadEspecificaciones entidadEspecificaciones, ActualizarParametros actualizarParametros)
        {
            var entidadesAEliminar = new List<EntidadPropiedad>();

            var entidad = entidadEspecificaciones.Entidad;

            var nombres = new List<string>();
            if (entidadEspecificaciones.IDPropiedadTipoId.HasValue
                && !actualizarParametros.IDPropiedadTipoId.HasValue)
            {
                nombres.Add(EntidadPropiedad.IdNombre(entidad));
            }

            if (entidadEspecificaciones.AuditoriaCreado
                && !actualizarParametros.AuditoriaCreado)
            {
                nombres.Add(EntidadPropiedades.CreadoPor.NOMBRE);
                nombres.Add(EntidadPropiedades.CreadoFecha.NOMBRE);
            }

            if (entidadEspecificaciones.AuditoriaUltimaModificacion
                && !actualizarParametros.AuditoriaUltimaModificacion)
            {
                nombres.Add(EntidadPropiedades.UltimaModificacionPor.NOMBRE);
                nombres.Add(EntidadPropiedades.UltimaModificacionFecha.NOMBRE);
            }

            if (entidadEspecificaciones.BajaTipoId != actualizarParametros.BajaTipoId
                && actualizarParametros.BajaTipoId != BajaTipos.LOGICA)
            {
                nombres.Add(EntidadPropiedades.BorradoPor.NOMBRE);
                nombres.Add(EntidadPropiedades.BorradoFecha.NOMBRE);
                nombres.Add(EntidadPropiedades.Borrado.NOMBRE);
            }

            EntidadPropiedad p;
            for (int i = entidad.Propiedades.Count - 1; i >= 0; i--)
            {
                p = entidad.Propiedades[i];
                if (nombres.Contains(p.Nombre))
                {
                    entidad.Propiedades.RemoveAt(i);
                    entidadesAEliminar.Add(p);
                }
            }

            return entidadesAEliminar;
        }

        private IEnumerable<EntidadPropiedad> ActualizarPropiedadesEnEntidadYObtenerLista(EntidadEspecificaciones entidadEspecificaciones, ActualizarParametros actualizarParametros)
        {
            var entidadesAActualizar = new List<EntidadPropiedad>();

            var entidad = entidadEspecificaciones.Entidad;

            var nombresYTipos = new Dictionary<string,short>();
            if (entidadEspecificaciones.IDPropiedadTipoId.HasValue
                && actualizarParametros.IDPropiedadTipoId.HasValue
                && entidadEspecificaciones.IDPropiedadTipoId != actualizarParametros.IDPropiedadTipoId)
            {
                nombresYTipos.Add(EntidadPropiedad.IdNombre(entidad), actualizarParametros.IDPropiedadTipoId.Value);
            }

            EntidadPropiedad p;
            short nuevoTipoId;
            for (int i = entidad.Propiedades.Count - 1; i >= 0; i--)
            {
                p = entidad.Propiedades[i];
                if (nombresYTipos.TryGetValue(p.Nombre, out nuevoTipoId))
                {
                    p.PropiedadTipoId = nuevoTipoId;
                    p.PropiedadTipoEspecificaciones = CrearPropiedadEspecificacionesDefault(nuevoTipoId);
                    entidadesAActualizar.Add(p);
                }
            }

            return entidadesAActualizar;
        }

        private void EliminarClavesYIndicesDePropiedadesAEliminarDeEntidad(Entidad entidad, IEnumerable<EntidadPropiedad> propiedadesAEliminar,
            out List<EntidadClave> clavesAEliminar, out List<EntidadIndice> indicesAEliminar)
        {
            var propiedadesConAsociaciones = new List<string>();

            clavesAEliminar = new List<EntidadClave>();
            indicesAEliminar = new List<EntidadIndice>();

            EntidadClave ec;
            EntidadIndice ei;
            foreach (var p in propiedadesAEliminar)
            {
                if (_entidadesAsociacionesRepositorio.ExistenPorPropiedadOrigen(p.Id))
                {
                    propiedadesConAsociaciones.Add(p.Nombre);
                    continue;
                }

                for (int i = entidad.Claves.Count - 1; i >= 0; i--)
                {
                    ec = entidad.Claves[i];
                    if (ec.EntidadPropiedadId == p.Id)
                    {
                        entidad.Claves.RemoveAt(i);
                        clavesAEliminar.Add(ec);
                        
                        ec.Entidad = null;
                        ec.Propiedad = null;
                    }
                }

                for (int i = entidad.Indices.Count - 1; i >= 0; i--)
                {
                    ei = entidad.Indices[i];
                    if (ei.Propiedades.Any(ip => ip.EntidadPropiedadId == p.Id))
                    {
                        entidad.Indices.RemoveAt(i);
                        indicesAEliminar.Add(ei);

                        ei.Entidad = null;
                        ei.Propiedades = null;
                    }
                }
            }

            if (propiedadesConAsociaciones.Any())
            {
                throw new ExcepcionMensajeAlUsuario($"No se pueden eliminar las siguientes propiedades porque están siendo referenciadas por asociaciones: {Formateador.Lista(propiedadesConAsociaciones, ", ")}.");
            }
        }

        private IEnumerable<EntidadPropiedad> CrearPropiedadesParaEntidad(EntidadEspecificaciones especificaciones)
        {
            var propiedades = new List<EntidadPropiedad>();

            var entidad = especificaciones.Entidad;
            if (especificaciones.IDPropiedadTipoId.HasValue
                && !entidad.TienePropiedad(EntidadPropiedad.IdNombre(entidad)))
            {
                propiedades.Add(new EntidadPropiedad
                {
                    Id = Guid.NewGuid(),
                    EntidadId = entidad.Id,
                    Entidad = entidad,
                    Nombre = EntidadPropiedad.IdNombre(entidad),
                    Etiqueta = EntidadPropiedades.Id.ETIQUETA,
                    PropiedadTipoId = especificaciones.IDPropiedadTipoId.Value,
                    PropiedadTipoEspecificaciones = CrearPropiedadEspecificacionesDefault(especificaciones.IDPropiedadTipoId.Value),
                    PermiteNull = false,
                    Orden = 1,
                    GeneradaAlCrear = true,
                    Editable = false,
                });
            }

            if (especificaciones.AuditoriaCreado)
            {
                if (!entidad.TienePropiedad(EntidadPropiedades.CreadoPor.NOMBRE))
                {
                    propiedades.Add(new EntidadPropiedad
                    {
                        Id = Guid.NewGuid(),
                        EntidadId = entidad.Id,
                        Entidad = entidad,
                        Nombre = EntidadPropiedades.CreadoPor.NOMBRE,
                        Etiqueta = EntidadPropiedades.CreadoPor.ETIQUETA,
                        PropiedadTipoId = PropiedadTipos.TEXTO,
                        PropiedadTipoEspecificaciones = PropiedadTiposEspecificaciones.AUDITORIA_USUARIO,
                        PermiteNull = false,
                        Orden = EntidadPropiedades.CreadoPor.ORDEN,
                        GeneradaAlCrear = true,
                        Editable = false,
                    });
                }

                if (!entidad.TienePropiedad(EntidadPropiedades.CreadoFecha.NOMBRE))
                {
                    propiedades.Add(new EntidadPropiedad
                    {
                        Id = Guid.NewGuid(),
                        EntidadId = entidad.Id,
                        Entidad = entidad,
                        Nombre = EntidadPropiedades.CreadoFecha.NOMBRE,
                        Etiqueta = EntidadPropiedades.CreadoFecha.ETIQUETA,
                        PropiedadTipoId = PropiedadTipos.FECHA_HORA,
                        PermiteNull = false,
                        Orden = EntidadPropiedades.CreadoFecha.ORDEN,
                        GeneradaAlCrear = true,
                        Editable = false,
                    });
                }
            }

            if (especificaciones.AuditoriaUltimaModificacion)
            {
                if (!entidad.TienePropiedad(EntidadPropiedades.UltimaModificacionPor.NOMBRE))
                {
                    propiedades.Add(new EntidadPropiedad
                    {
                        Id = Guid.NewGuid(),
                        EntidadId = entidad.Id,
                        Entidad = entidad,
                        Nombre = EntidadPropiedades.UltimaModificacionPor.NOMBRE,
                        Etiqueta = EntidadPropiedades.UltimaModificacionPor.ETIQUETA,
                        PropiedadTipoId = PropiedadTipos.TEXTO,
                        PropiedadTipoEspecificaciones = PropiedadTiposEspecificaciones.AUDITORIA_USUARIO,
                        PermiteNull = false,
                        Orden = EntidadPropiedades.UltimaModificacionPor.ORDEN,
                        GeneradaAlCrear = true,
                        Editable = false,
                    });
                }

                if (!entidad.TienePropiedad(EntidadPropiedades.UltimaModificacionFecha.NOMBRE))
                {
                    propiedades.Add(new EntidadPropiedad
                    {
                        Id = Guid.NewGuid(),
                        EntidadId = entidad.Id,
                        Entidad = entidad,
                        Nombre = EntidadPropiedades.UltimaModificacionFecha.NOMBRE,
                        Etiqueta = EntidadPropiedades.UltimaModificacionFecha.ETIQUETA,
                        PropiedadTipoId = PropiedadTipos.FECHA_HORA,
                        PermiteNull = false,
                        Orden = EntidadPropiedades.UltimaModificacionFecha.ORDEN,
                        GeneradaAlCrear = true,
                        Editable = false,
                    });
                }
            }

            if (especificaciones.BajaTipoId == BajaTipos.LOGICA)
            {
                if (!entidad.TienePropiedad(EntidadPropiedades.BorradoPor.NOMBRE))
                {
                    propiedades.Add(new EntidadPropiedad
                    {
                        Id = Guid.NewGuid(),
                        EntidadId = entidad.Id,
                        Entidad = entidad,
                        Nombre = EntidadPropiedades.BorradoPor.NOMBRE,
                        Etiqueta = EntidadPropiedades.BorradoPor.ETIQUETA,
                        PropiedadTipoId = PropiedadTipos.TEXTO,
                        PropiedadTipoEspecificaciones = PropiedadTiposEspecificaciones.AUDITORIA_USUARIO,
                        PermiteNull = true,
                        Orden = EntidadPropiedades.BorradoPor.ORDEN,
                        Editable = false,
                    });
                }

                if (!entidad.TienePropiedad(EntidadPropiedades.BorradoFecha.NOMBRE))
                {
                    propiedades.Add(new EntidadPropiedad
                    {
                        Id = Guid.NewGuid(),
                        EntidadId = entidad.Id,
                        Entidad = entidad,
                        Nombre = EntidadPropiedades.BorradoFecha.NOMBRE,
                        Etiqueta = EntidadPropiedades.BorradoFecha.ETIQUETA,
                        PropiedadTipoId = PropiedadTipos.FECHA_HORA,
                        PermiteNull = true,
                        Orden = EntidadPropiedades.BorradoFecha.ORDEN,
                        Editable = false,
                    });
                }

                if (!entidad.TienePropiedad(EntidadPropiedades.Borrado.NOMBRE))
                {
                    propiedades.Add(new EntidadPropiedad
                    {
                        Id = Guid.NewGuid(),
                        EntidadId = entidad.Id,
                        Entidad = entidad,
                        Nombre = EntidadPropiedades.Borrado.NOMBRE,
                        Etiqueta = EntidadPropiedades.Borrado.ETIQUETA,
                        PropiedadTipoId = PropiedadTipos.BOOLEANO,
                        PermiteNull = false,
                        CalculadaFormula = EntidadPropiedades.Borrado.CALCULADA_FORMULA,
                        Orden = EntidadPropiedades.Borrado.ORDEN,
                        Editable = false,
                    });
                }
            }

            return propiedades;
        }

        private string CrearPropiedadEspecificacionesDefault(short propiedadTipoId)
        {
            switch (propiedadTipoId)
            {
                case PropiedadTipos.TEXTO:
                    return PropiedadTiposEspecificaciones.ID_TEXTO_DEFAULT;
                
                case PropiedadTipos.ENTERO:
                case PropiedadTipos.ENTERO_CORTO:
                case PropiedadTipos.ENTERO_LARGO:
                    return PropiedadTiposEspecificaciones.ID_ENTERO_DEFAULT;
                
                case PropiedadTipos.DECIMAL:
                case PropiedadTipos.DECIMAL_FLOTANTE:
                    return PropiedadTiposEspecificaciones.ID_DECIMAL_DEFAULT;
                
                default:
                    return null;
            }
        }

        private IEnumerable<EntidadClave> CrearClavesParaEntidad(EntidadEspecificaciones especificaciones)
        {
            if (especificaciones.IDPropiedadTipoId.HasValue)
            {
                var entidad = especificaciones.Entidad;
                var propiedadId =
                    entidad.Propiedades != null
                    ? entidad.Propiedades.FirstOrDefault(p => p.EsId)
                    : null;

                if (propiedadId != null
                    && !entidad.TieneClave(propiedadId.Id))
                {
                    return new[]
                    {
                        new EntidadClave
                        {
                            Id = Guid.NewGuid(),
                            EntidadId = propiedadId.EntidadId,
                            EntidadPropiedadId = propiedadId.Id
                        }
                    };
                }
            }

            return null;
        }
    }
}
