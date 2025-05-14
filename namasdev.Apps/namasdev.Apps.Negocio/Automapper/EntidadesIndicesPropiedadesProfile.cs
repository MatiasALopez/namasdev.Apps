using AutoMapper;

using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio.DTO.EntidadesIndicesPropiedades;

namespace namasdev.Apps.Negocio.Automapper
{
    public class EntidadIndicePropiedadProfile : Profile
    {
        public EntidadIndicePropiedadProfile()
        {
            CreateMap<AgregarParametros,EntidadIndicePropiedad>();
            CreateMap<ActualizarParametros,EntidadIndicePropiedad>();
        }
    }
}
