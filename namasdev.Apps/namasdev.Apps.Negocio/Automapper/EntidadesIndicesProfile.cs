using AutoMapper;

using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio.DTO.EntidadesIndices;

namespace namasdev.Apps.Negocio.Automapper
{
    public class EntidadIndiceProfile : Profile
    {
        public EntidadIndiceProfile()
        {
            CreateMap<AgregarParametros,EntidadIndice>();
            CreateMap<ActualizarParametros,EntidadIndice>();
        }
    }
}
