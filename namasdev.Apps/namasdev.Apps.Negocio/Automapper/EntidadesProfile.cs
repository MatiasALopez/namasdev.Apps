using AutoMapper;
using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio.DTO.Entidades;

namespace namasdev.Apps.Negocio.Automapper
{
    public class EntidadesProfile : Profile
    {
        public EntidadesProfile()
        {
            CreateMap<AgregarParametros, Entidad>();
            CreateMap<ActualizarParametros, Entidad>();
            CreateMap<MarcarComoBorradoParametros, Entidad>();
            CreateMap<DesmarcarComoBorradoParametros, Entidad>();
        }
    }
}
