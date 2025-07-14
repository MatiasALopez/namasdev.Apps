using AutoMapper;

using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio.DTO.EntidadesChecks;

namespace namasdev.Apps.Negocio.Automapper
{
    public class EntidadesChecksProfile : Profile
    {
        public EntidadesChecksProfile()
        {
            CreateMap<AgregarParametros,EntidadCheck>();
            CreateMap<ActualizarParametros,EntidadCheck>();
        }
    }
}
