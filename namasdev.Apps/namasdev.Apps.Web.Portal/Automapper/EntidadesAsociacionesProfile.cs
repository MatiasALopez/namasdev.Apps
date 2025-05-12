using AutoMapper;

using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio.DTO.EntidadesAsociaciones;
using namasdev.Apps.Web.Portal.Models.EntidadesAsociaciones;
using namasdev.Apps.Web.Portal.ViewModels.EntidadesAsociaciones;

namespace namasdev.Apps.Web.Portal.Automapper
{
    public class EntidadAsociacionProfile : Profile
    {
        public EntidadAsociacionProfile()
        {
            CreateMap<EntidadAsociacion, EntidadAsociacionViewModel>()
                .ReverseMap();

            CreateMap<EntidadAsociacion, EntidadAsociacionItemModel>();

            CreateMap<AgregarParametros, EntidadAsociacionViewModel>()
                .ReverseMap();

            CreateMap<ActualizarParametros, EntidadAsociacionViewModel>()
                .ReverseMap();
        }
    }
}