using AutoMapper;

using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio.DTO.EntidadesAsociaciones;

namespace namasdev.Apps.Negocio.Automapper
{
    public class EntidadAsociacionProfile : Profile
    {
        public EntidadAsociacionProfile()
        {
            CreateMap<AgregarParametros,EntidadAsociacion>();
            CreateMap<ActualizarParametros,EntidadAsociacion>();
        }
    }
}
