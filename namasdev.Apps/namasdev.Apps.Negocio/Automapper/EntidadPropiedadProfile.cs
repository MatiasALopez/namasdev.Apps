
using AutoMapper;

using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio.DTO.EntidadesPropiedades;

namespace namasdev.Apps.Negocio.Automapper
{
    public class EntidadPropiedadProfile : Profile
    {
        public EntidadPropiedadProfile()
        {
            CreateMap<AgregarParametros, EntidadPropiedad>();
            CreateMap<ActualizarParametros, EntidadPropiedad>();
            CreateMap<EliminarParametros, EntidadPropiedad>();
        }
    }
}
