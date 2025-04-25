using AutoMapper;

using namasdev.Apps.Entidades;
using namasdev.Apps.Negocio.DTO.Entidades;
using namasdev.Apps.Web.Portal.Models.Entidades;
using namasdev.Apps.Web.Portal.ViewModels.Entidades;

namespace namasdev.Apps.Web.Portal.Automapper
{
    public class EntidadesProfile : Profile
    {
        public EntidadesProfile()
        {
            CreateMap<Entidad, EntidadViewModel>()
                .ReverseMap();

            CreateMap<Entidad, EntidadItemModel>();

            CreateMap<AgregarParametros, EntidadViewModel>()
                .ReverseMap();

            CreateMap<ActualizarParametros, EntidadViewModel>()
                .ReverseMap();
        }
    }
}