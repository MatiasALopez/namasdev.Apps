using AutoMapper;

using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio.DTO.EntidadesEspecificaciones;

namespace namasdev.Apps.Negocio.Automapper
{
    public class EntidadesEspecificacionesProfile : Profile
    {
        public EntidadesEspecificacionesProfile()
        {
            CreateMap<ActualizarParametros, EntidadEspecificaciones>();
        }
    }
}
